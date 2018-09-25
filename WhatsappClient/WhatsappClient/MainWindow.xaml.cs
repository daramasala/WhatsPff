using System;
using System.Net.Http;
using System.Windows;
namespace WhatsappClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Set window location in center of the screen
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }

        /// <summary>
        /// Open new Register window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window w = new Register();
            w.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckIfValidInput())
                    return;

                using (var c = new HttpClient())
                {
                    // Perform login
                    var result =
                             c.GetStringAsync(String.Format("http://localhost:56584//api/User?username={0}&password={1}", usernameTxt.Text, passwordTxt.Text)).Result;
                    // Loggin succeeded
                    if (result.Equals("\"1\""))
                    {
                        MessageBox.Show("Logged In Successfully", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                        Properties.Settings.Default["Username"] = usernameTxt.Text;
                        Properties.Settings.Default.Save();
                        string user = Properties.Settings.Default["Username"].ToString();
                        this.Hide();
                        Window w = new WhatsappWindow();
                        w.Show();

                    }
                    if (result.Equals("\"-2\""))
                        MessageBox.Show("Incorrect username or password please try again", "Failed To Login", MessageBoxButton.OK, MessageBoxImage.Error);

                    if (result.Equals("\"-3\""))
                        MessageBox.Show("You are already signed in !", "Failed To Login", MessageBoxButton.OK, MessageBoxImage.Error);

                    if (result.Equals("\"-4\""))
                        MessageBox.Show("Something wrong happened in server side", "Failed To Login", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Check if TextBoxes are not null or empty or whitespaces
        /// </summary>
        /// <returns></returns>
        private bool CheckIfValidInput()
        {
            if (String.IsNullOrWhiteSpace(usernameTxt.Text) ||
               String.IsNullOrEmpty(usernameTxt.Text))
            {
                MessageBox.Show("Username is requierd", "Invalid Input", MessageBoxButton.OK
                    , MessageBoxImage.Error);
                return false;
            }

            if (String.IsNullOrWhiteSpace(passwordTxt.Text) ||
                String.IsNullOrEmpty(passwordTxt.Text))
            {
                MessageBox.Show("Password is requierd", "Invalid Input", MessageBoxButton.OK
                    , MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// On load get Username from Properties default settings
        /// Username = "" - No one has logged in yet, keep login window
        /// Username = "Some User Name" - Someone already signed in, go to the WhatsappWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string user = Properties.Settings.Default["Username"].ToString();
                if ( user != "null")
                {
                    this.Hide();
                    Window w = new WhatsappWindow();
                    w.Show();
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
