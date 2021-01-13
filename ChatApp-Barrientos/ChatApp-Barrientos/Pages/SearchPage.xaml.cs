using ChatApp_Barrientos.Helpers;
using ChatApp_Barrientos.Models;
using Newtonsoft.Json;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_Barrientos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SearchPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        public SearchPage(string txt)
         {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //ObservableCollection<UserModel> Users = new ObservableCollection<UserModel>();
            Users = new ObservableCollection<UserModel>();
            GetResults(txt);
        }
        private bool isBusy;
        public bool IsBusy
        {
            get { return 
                    isBusy; 
            }
            set {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                
            }
        }
        private UserModel selectedItem;
        public UserModel SelectedItem
        {
            get { 
                return selectedItem; 
            }
            set
            {
                selectedItem = value;
                AddDialog();
                OnPropertyChanged("SelectedItem");
                //RaisePropertyChanged();
            }
        }
        public ICommand BackCommand => new Command(async () => await Navigation.PopModalAsync(true));

        private ObservableCollection<UserModel> _users;
        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged(nameof(Users)); }
        }

        private async void AddDialog()
        {
            if (SelectedItem == null) { return; }
            bool choice = await DisplayAlert("Add contact", "Would you like to add " + SelectedItem.name, "Yes", "No");
            if (choice)
            {
                if (dataClass.loggedInUser.uid.Equals(SelectedItem.uid))
                {
                    await DisplayAlert("Error", "You are not allowed to add yourself.", "Okay");
                }
                else
                {
                    bool exists = false;
                    if (dataClass.loggedInUser.contacts != null && dataClass.loggedInUser.contacts.Count != 0)
                    {
                        //Checks if it is in his/her contacts.
                        string y = dataClass.loggedInUser.contacts.FirstOrDefault(x => x.Equals(SelectedItem.uid));
                        exists = !(String.IsNullOrEmpty(y));
                    }
                    if (exists)
                    {
                        await DisplayAlert("Error", "You already both have a connection.", "Okay");
                    }
                    else
                    {
                        IsBusy = true;
                        Guid guid = Guid.NewGuid();
                        ContactModel contact = new ContactModel()
                        {
                            id = Guid.NewGuid().ToString(),
                            contactID = new string[] { dataClass.loggedInUser.uid, SelectedItem.uid },
                            contactEmail = new string[] { dataClass.loggedInUser.email, SelectedItem.email },
                            contactName = new string[] { dataClass.loggedInUser.name, SelectedItem.name },
                            created_at = DateTime.UtcNow.ToString()
                            //created_at = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                        };
                        await CrossCloudFirestore.Current
                                .Instance
                                .GetCollection("contacts")
                                .GetDocument(contact.id)
                                .SetDataAsync(contact);
                        if (dataClass.loggedInUser.contacts == null)
                            dataClass.loggedInUser.contacts = new List<string>();
                        dataClass.loggedInUser.contacts.Add(SelectedItem.uid);
                        await CrossCloudFirestore.Current
                                .Instance
                                .GetCollection("users")
                                .GetDocument(dataClass.loggedInUser.uid)
                                .UpdateDataAsync(new { contacts = dataClass.loggedInUser.contacts });
                        if (SelectedItem.contacts == null)
                            selectedItem.contacts = new List<string>();
                        SelectedItem.contacts.Add(dataClass.loggedInUser.uid);
                        await CrossCloudFirestore.Current
                                .Instance
                                .GetCollection("users")
                                .GetDocument(SelectedItem.uid)
                                .UpdateDataAsync(new { contacts = SelectedItem.contacts });
                        IsBusy = false;
                        await DisplayAlert("Success", "Contact Added", "Okay");
                    }
                }
            }
            SelectedItem = null;
        }


        private async void GetResults(string email)
        {
            var documents = await CrossCloudFirestore.Current
                                .Instance
                                .GetCollection("users")
                                .WhereEqualsTo("email", email)
                                .GetDocumentsAsync();
            int x = 0;
            foreach (var documentChange in documents.DocumentChanges)
            {
                var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                UserModel user = JsonConvert.DeserializeObject<UserModel>(json);
                Users.Add(user);
                x++;
            }
             if (x == 0)
            {
                await DisplayAlert("", "User not found.", "Okay");
                await Navigation.PopModalAsync(true);
            }
        }

        
    }

    
}