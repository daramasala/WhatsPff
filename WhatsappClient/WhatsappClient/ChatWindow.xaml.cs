using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using WhatsappDB;
using Newtonsoft.Json;
using System.Linq;
using System.Windows.Input;
using System.Windows.Documents;

namespace WhatsappClient
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        public DateTime lastChecked;
        
        public List<Message> messages = new List<Message>();
        public List<ChatView> chatViewItems = new List<ChatView>();
        private string _friendUsername;
        public ChatWindow()
        {
            InitializeComponent();
        }

        public ChatWindow(string title,  string friendUsername)
        {
            InitializeComponent();

            //Set window in the middle of the screen
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this._friendUsername = friendUsername;

            //Set the title of the window as Friend's name
            Title = title;

            //Initialize the chat list
            InitializeChatList();
        }

        /// <summary>
        /// Intializes the chat view
        /// </summary>
        private void InitializeChatList()
        {
            string messagesList = "";
            string currentUser = Properties.Settings.Default["Username"].ToString();
            using (var client = new HttpClient())
            {
                // Gets all the messages between current user and friend
                messagesList =
                    client.GetStringAsync(String.Format("http://localhost:56584//api/Message?firstUser={0}&secondUser={1}", currentUser, _friendUsername)).Result;
            }
            if (messagesList == "null")
            {
                MessageBox.Show("No messages yet, feel free to wirte to your friend here.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Deserialize the json object
                messages = JsonConvert.DeserializeObject<List<Message>>(messagesList);
                // No new messages
                if (messages.Count == chatViewItems.Count)
                    return;

                //Order messages by Date
                messages = messages.OrderBy(x => x.Date).ToList();

                //Create list for the chat view
                List<ChatView> list = new List<ChatView>();
                foreach (Message m in messages)
                {
                    // The message was sent by me
                    if (m.SenderUsername == currentUser)
                    {
                        DateTime time = m.Date;
                        list.Add(new ChatView(m.Content + " " +time.Hour + ":" + time.Minute, "", time));
                    }
                    else
                    {
                        DateTime time = m.Date;
                        list.Add(new ChatView("", time.Hour + ":" + time.Minute + " " +  m.Content, time));
                    }
                }
                if(chatViewItems != null && list.Count != 0)
                {
                    // Update the chatView Items
                    chatViewItems = list;

                    //Update the binding so the UI updates
                    chatListView.ItemsSource = null;
                    chatListView.ItemsSource = chatViewItems;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Create timer that execute PerformCheckForNewMessages every 10 seconds
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(PerformCheckForNewMessages);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Perfom check for new messages every 10 seconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformCheckForNewMessages(object sender, EventArgs e)
        {
            InitializeChatList();

            //Riase the RequerySuggested
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Enables / Disables the Send button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newMessageTxt_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextRange range = new TextRange(newMessageTxt.Document.ContentStart, newMessageTxt.Document.ContentEnd);

            string allText = range.Text;
            if (allText == "")
                sendBtn.IsEnabled = false;
            else
                sendBtn.IsEnabled = true;
        }

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            // Get the message content
            TextRange range = new TextRange(newMessageTxt.Document.ContentStart, newMessageTxt.Document.ContentEnd);
            string msgText = range.Text;
            string currentUser = Properties.Settings.Default["Username"].ToString();

            // Add new message to the database
            using (var client = new HttpClient())
            {
                var messagesList =
                    client.GetStringAsync(String.Format("http://localhost:56584//api/Message?sender={0}&receiver={1}&date={2}&content={3}", currentUser, _friendUsername,DateTime.Now,msgText)).Result;
            }

            //Clear the input
            newMessageTxt.Document.Blocks.Clear();
        }
    }
}
