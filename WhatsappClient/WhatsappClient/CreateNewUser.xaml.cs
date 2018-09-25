using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WhatsappDB;

namespace WhatsappClient
{
    /// <summary>
    /// Interaction logic for CreateNewUser.xaml
    /// </summary>
    public partial class CreateNewUser : Window
    {
        public CreateNewUser()
        {
            InitializeComponent();
        }

        private void createUserBtn_Click(object sender, RoutedEventArgs e)
        {
            using(var c = new HttpClient())
            {
                try
                {
                    var serialized = JsonConvert.SerializeObject(
                        new Contact(usernameTxt.Text,passwordTxt.Text
                        ,firstnameTxt.Text,lastnameTxt.Text), Formatting.Indented);
                    MessageBox.Show(serialized);
                    var content =
                    new StringContent(serialized, Encoding.UTF8, "application/json");
                    var result =

                    c.PostAsync("http://localhost:56584//api/users/new", content).Result;
                    MessageBox.Show(result.Headers.ToString());
                    this.Close();
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
