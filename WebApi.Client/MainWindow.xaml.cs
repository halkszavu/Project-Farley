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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using WebApi.Entities;

namespace WebApi.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string Tie = @"http://localhost:58637/api/";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewPersonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void SearchPersonButton_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(new Uri(Tie + $"People/1"));

                if(response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    Person person = JsonConvert.DeserializeObject<Person>(json);

                    PersonNameTextBox.Text = person.Name;
                }
            }
        }

        private void PersonSaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NoteSaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
