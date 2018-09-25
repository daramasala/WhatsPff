using System;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using WhatsappDB;

namespace WhatsappClient
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            // Get the current window
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            // Check if any of the TextBoxes are empty or null or whitespaces
            foreach (TextBox textbox in Helper.FindVisualChildren<TextBox>(window))
            {
                if (String.IsNullOrEmpty(textbox.Text) || String.IsNullOrWhiteSpace(textbox.Text))
                {
                    MessageBox.Show("Some of the fields are empty, please refill and try again.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }                    
            }

            try
            {
                // Create new Contact object
                Contact newContact = new Contact(usernameTxt.Text, passwordTxt.Text, firstnameTxt.Text, lastnameTxt.Text);
                using (var c = new HttpClient())
                {
                    // Perform AddNewUser
                    var result =
                         c.GetStringAsync(String.Format("http://localhost:56584//api/User?username={0}&password={1}&firstname={2}&lastname={3}",usernameTxt.Text,passwordTxt.Text
                         ,firstnameTxt.Text,lastnameTxt.Text)).Result;
                    MessageBox.Show(result);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
