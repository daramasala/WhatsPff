using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;
using WhatsappDB;

namespace WhatsappClient
{
    /// <summary>
    /// Interaction logic for WhatsappWindow.xaml
    /// </summary>
    public partial class WhatsappWindow : Window
    {
        public List<MainView> mainViewItems = new List<MainView>();
        public WhatsappWindow()
        {
            InitializeComponent();

            //Initialize the mainListView ItemsSource
            mainListView.ItemsSource = mainViewItems;

            // Set window to the middle of the screen
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Perform logout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = Properties.Settings.Default["Username"].ToString();
            using (var c = new HttpClient())
            {
                // Perform Logout from OnlineUsers
                var result =
                         c.GetStringAsync(String.Format("http://localhost:56584//api/User?username={0}", username)).Result;

                // Update the Username to null
                Properties.Settings.Default["Username"] =  "null";
                Properties.Settings.Default.Save();
                this.Close();
                
                //Go back to the Login window
                Window w = new MainWindow();
                w.Show();
                return;

            }
        }

        // Close the application
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }


        /// <summary>
        /// Gets all the friend list and the latest message with them
        /// </summary>
        private void GetFriendsWithLastMessage()
        {
            List<Contact> contactList =null;
            string currentUser = Properties.Settings.Default["Username"].ToString();

            try
            {
                using (var c = new HttpClient())
                {

                    // Get all Friends
                    var query =
                             c.GetStringAsync(String.Format("http://localhost:56584//api/User?friendOfUsername={0}", currentUser)).Result;
                    List<Contact> requestContact = JsonConvert.DeserializeObject<List<Contact>>(query);
                    contactList = requestContact;
                    
                    // The user have no friends yet
                    if (contactList == null || contactList.Count == 0)
                        return;

                    //Clear the lastMessagesDictionary
                    mainViewItems.Clear();

                    foreach (Contact contact in contactList)
                    {
                        string lastMessage = "";
                        using (var client = new HttpClient())
                        {
                            // Get the latest message between the current user and his friend
                            lastMessage =
                                client.GetStringAsync(String.Format("http://localhost:56584//api/Message?clientUser={0}&friendUser={1}", currentUser, contact.Username)).Result;
                        }

                        // No messages has been sent yet
                        if (lastMessage == "null")
                        {
                            var d1 = DateTime.Now.AddDays(-1);                            
                            mainViewItems.Add(new MainView(contact.FirstName + " " + contact.LastName, "No Messages Yet",d1,contact.Username));
                        }
                        else
                        {
                            Message msg = JsonConvert.DeserializeObject<Message>(lastMessage);
                            mainViewItems.Add(new MainView(contact.FirstName + " " + contact.LastName, msg.Content, msg.Date,contact.Username));
                        }
                            
                    }

                    // Order the list descending by Date
                    mainViewItems = mainViewItems.OrderByDescending(x => x.Date).ToList();

                    // Update the binding so the UI updates
                    mainListView.ItemsSource = null;
                    mainListView.ItemsSource = mainViewItems;
                }
            }
              catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetFriendsWithLastMessage();
            mainListView.ItemsSource = mainViewItems;

            // Create timer that execute UpdateGUI every 10 seconds
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateGUI);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        private void UpdateGUI(object sender, EventArgs e)
        {
            // Get the updates messages with the friend
            GetFriendsWithLastMessage();
        }

        /// <summary>
        /// Creates a new ChatWindow and show it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewItem_Click(object sender, RoutedEventArgs e)
        {
            var item = mainListView.SelectedItem;
            Window chatWindow = new ChatWindow(((MainView)item).FullName, ((MainView)item).Username);
            chatWindow.Show();         
        }

        /// <summary>
        /// Enable / Disable the Add New Contact Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(addNewUserTxt.Text) || (addNewUserTxt.Text == "[Enter Username Here]"))
                addNewContactBtn.IsEnabled = false;
            else
                addNewContactBtn.IsEnabled = true;
        }

        /// <summary>
        /// Perform add new contact
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewContactBtn_Click(object sender, RoutedEventArgs e)
        {
            string friendUsername = addNewUserTxt.Text;
            string currentUsername = Properties.Settings.Default["Username"].ToString();

            // Trying to add myself as a friend
            if(friendUsername == currentUsername)
            {
                MessageBox.Show("Nice try ;)");
                return;
            }

            using (var client = new HttpClient())
            {
                // Perform AddNewFriend
                var res =
                    client.GetStringAsync(String.Format("http://localhost:56584//api/User?currentUsername={0}&newFriendUsername={1}", currentUsername, friendUsername)).Result;

                // Successfully added
                if (res == "1")
                {
                    MessageBox.Show("Friend added successfully.", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                    GetFriendsWithLastMessage();
                    return;
                }

                //Username doesn't exists
                if (res == "-1")
                {
                    MessageBox.Show("Username doesn't exists, please verify its the right username and try again", "Username not found", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //Friend already exist in friend list
                if (res == "-2")
                {
                    MessageBox.Show("Friend already exist in your friend list.", "Already Exists", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                //Server-side error
                if (res == "-3")
                {
                    MessageBox.Show("Error occuried in server side", "Undefined Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

        }
    }
}
