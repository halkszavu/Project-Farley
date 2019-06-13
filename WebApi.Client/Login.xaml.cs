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
using WebApi.Bll.Dtos;

namespace WebApi.Client
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string tie;

        public Login()
        {
            InitializeComponent();
            tie = ((MainWindow)Application.Current.MainWindow).Tie;
        }

        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(tie + "User/register", new StringContent(JsonConvert.SerializeObject(new SignInDto { Password = SignUpPassword.Password, Email = SignInEmail.Text, UserName = SignInUsername.Text }), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    ((MainWindow)Application.Current.MainWindow).Token = json;
                }
                else
                {
#if DEBUG
                    MessageBox.Show($"The response from the server :\n{response.StatusCode.ToString()}", "Http Response");
#endif
                    return;
                }
            }

            this.Close();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(tie + "User/login", new StringContent(JsonConvert.SerializeObject(new LoginDto { Password = LoginPassword.Password, Username = LoginUsername.Text }), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    ((MainWindow)Application.Current.MainWindow).Token = json;
                }
                else
                {
#if DEBUG
                    MessageBox.Show($"The response from the server :\n{response.StatusCode.ToString()}", "Http Response");
#endif
                    return;
                }
            }

            this.Close();
        }
    }
}
