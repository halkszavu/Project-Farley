using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using WebApi.Bll.Dtos;
using WebApi.Entities;

namespace WebApi.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly string Tie = @"http://localhost:58637/api/";

        //public readonly string Tie = @"http://localhost:5000/api/";
        
        //henkilöllisyys
        private int henkilo;
        private byte[] rowVersion;
        //tapaaminen
        //private int tapa;
        List<Person> people;

        public MainWindow()
        {
            InitializeComponent();
            people = new List<Person>();
            PersonListBox.ItemsSource = people;
        }

        private async void NewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO:
            //onko henkilö jo olemassa tietokannassa
            //jos kyllä: näyttä virheilmoituksen

            rowVersion = new byte[] { };

            using (var client =  new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(PrimePerson()),Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Tie + "Person", content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    LoadPerson(JsonConvert.DeserializeObject<Person>(json));
                }
#if DEBUG
                else
                    MessageBox.Show($"The response from the server :\n{response.StatusCode.ToString()}", "Http Response");
#endif
            }
        }

        private async void SearchPersonButton_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(new Uri(Tie + $"Person/byName{PersonNameTextBox.Text}"));

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    LoadPerson(JsonConvert.DeserializeObject<Person>(json));
                }
            }
        }

        private async void PersonSaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(PrimePerson()), Encoding.UTF8, "application/json");
                var response = await client.PutAsync(Tie + "Person", content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    LoadPerson(JsonConvert.DeserializeObject<Person>(json));
                }
            }
        }

        private async void NoteSaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                Note Note = PrimeNote();
                Note.PersonID = henkilo;
                var content = new StringContent(JsonConvert.SerializeObject(Note), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Tie + $"Note", content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    LoadNote(JsonConvert.DeserializeObject<Note>(json));
                }
            }
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Készítette: Perényi Botond L.\nFarley-akta kezelőfelület\n2019.05", "Info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

        /// <summary>
        /// Loads in the Person to the UI
        /// </summary>
        /// <param name="Person">The <see cref="Person"/> to load</param>
        private async void LoadPerson(Person Person)
        {
            PersonNameTextBox.Text = Person.Name;
            henkilo = Person.ID;

            switch (Person.MartialState)
            {
                case MartialState.Single:
                    MartialStateSingleRadioButton.IsChecked = true;
                    break;
                case MartialState.Married:
                    MartialStateMarriedRadioButton.IsChecked = true;
                    break;
                case MartialState.Other:
                    MartialStateOtherRadioButton.IsChecked = true;
                    break;
                case MartialState.NA:
                default:
                    MartialStateNARadioButton.IsChecked = true;
                    break;
            }

            switch (Person.SiblingState)
            {
                case SiblingState.Eldest:
                    SiblingStateEldestRadioButton.IsChecked = true;
                    break;
                case SiblingState.Younges:
                    SiblingStateYoungestRadioButton.IsChecked = true;
                    break;
                case SiblingState.Middle:
                    SiblingStateMiddleRadioButton.IsChecked = true;
                    break;
                case SiblingState.Only_Child:
                    SiblingStateOnlyChildRadioButton.IsChecked = true;
                    break;
                case SiblingState.Other:
                    SiblingStateOtherRadioButton.IsChecked = true;
                    break;
                case SiblingState.NA:
                default:
                    SiblingStateNARadioButton.IsChecked = true;
                    break;
            }

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(new Uri(Tie + $"Note/{Person.ID}"));

                if(response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    LoadNote(JsonConvert.DeserializeObject<Note>(json));
                }
            }

            rowVersion = Person.RowVersion;
        }

        /// <summary>
        /// Loads in the Meeting to the UI
        /// </summary>
        /// <param name="Meeting">The <see cref="Meeting"/> to load</param>
        private void LoadMeeting(Meeting Meeting)
        {

        }

        /// <summary>
        /// Loads in the Note to the UI
        /// </summary>
        /// <param name="Note">The <see cref="Note"/> to load</param>
        private void LoadNote(Note Note) => NotesTextBox.Text = Note.Notes;


        /// <summary>
        /// Sets up the Person from the UI
        /// </summary>
        /// <returns>The <see cref="Person"/> on the UI</returns>
        private Person PrimePerson()
        {
            return new Person
            {
                Name = PersonNameTextBox.Text,
                MartialState = GetMartialState(),
                SiblingState = GetSiblingState(),
                DateOfBirth = PersonCalendar.SelectedDate ?? DateTime.Now,
                RowVersion = rowVersion
            };
        }

        private SiblingState GetSiblingState()
        {
            if (SiblingStateMiddleRadioButton.IsChecked ?? false)
                return SiblingState.Middle;
            else if (SiblingStateEldestRadioButton.IsChecked ?? false)
                return SiblingState.Eldest;
            else if (SiblingStateOnlyChildRadioButton.IsChecked ?? false)
                return SiblingState.Only_Child;
            else if (SiblingStateYoungestRadioButton.IsChecked ?? false)
                return SiblingState.Younges;
            else if (SiblingStateOtherRadioButton.IsChecked ?? false)
                return SiblingState.Other;
            else
                return SiblingState.NA;
        }

        private MartialState GetMartialState()
        {
            if (MartialStateMarriedRadioButton.IsChecked ?? false)
                return MartialState.Married;
            else if (MartialStateSingleRadioButton.IsChecked ?? false)
                return MartialState.Single;
            else if (MartialStateOtherRadioButton.IsChecked ?? false)
                return MartialState.Other;
            else
                return MartialState.NA;
        }

        /// <summary>
        /// Sets up the Note from the UI
        /// </summary>
        /// <returns>The <see cref="Note"/> in the UI</returns>
        private Note PrimeNote() => new Note { Notes = NotesTextBox.Text, Time = DateTime.Now };

        private void FarleyTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl && MeetingTabItem.IsSelected)
            {
                UpdateMeetingTabItem();
            }
            else
            {

            }
        }

        private async void UpdateMeetingTabItem()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(Tie + "People");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var populii = JsonConvert.DeserializeObject<List<Person>>(json);
                    people.Clear();

                    foreach (var item in populii)
                    {
                        people.Add(item);
                    }
                }
            }
        }

        private void NewMeetingButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextMeetingButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PreviousMeetingButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            //Megváltozott a "TextBox" tartalma (valamilyen módon)
        }
    }
}
