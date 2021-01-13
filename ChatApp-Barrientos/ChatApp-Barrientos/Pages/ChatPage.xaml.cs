using ChatApp_Barrientos.Helpers;
using ChatApp_Barrientos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.CloudFirestore;
using Newtonsoft.Json;
using System.Windows.Input;

namespace ChatApp_Barrientos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        
        DataClass dataClass = DataClass.GetInstance;
        
        public ChatPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            ContactList = new ObservableCollection<ContactModel>();
          
            CrossCloudFirestore.Current
                .Instance
                .GetCollection("contacts")
                .WhereArrayContains("contactID", dataClass.loggedInUser.uid)
                .AddSnapshotListener((snapshot, error) =>
                {
                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                            var obj = JsonConvert.DeserializeObject<ContactModel>(json);
                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    ContactList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (ContactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = ContactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (ContactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = ContactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        string idOfRemoved = item.contactID[0] == dataClass.loggedInUser.uid ? item.contactID[1] : item.contactID[0];
                                        bool test = dataClass.loggedInUser.contacts.Remove(idOfRemoved);
                                        ContactList.Remove(item);
                                    }
                                    break;

                            }
                        }

                     
                        IsEmpty = ContactList.Count == 0;
                        
                    }
                });
        }

        private bool _isEmpty;

        public bool IsEmpty
        {
            get
            {
                return _isEmpty;
            }
            set
            {
                _isEmpty = value;
                OnPropertyChanged("IsEmpty");
            }
        }

       

        private string _text;
        public string Text
        {
            get { 
                
                return _text;
               
            }
            set { 
                _text = value; 
                OnPropertyChanged(Text); 
            }
        }

        private ContactModel selecteditem;
        public ContactModel SelectedItem {
            get { return selecteditem; }
            set { 
                selecteditem = value; 
                if(value != null)
                {
                    NavigateToMessages();
                }
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private ObservableCollection<ContactModel> contactlist;
        public ObservableCollection<ContactModel> ContactList {
            get { return contactlist; }
            set { contactlist = value; OnPropertyChanged(nameof(ContactList)); }
        }

        public ICommand SearchCommand => new Command(Search);
       
        private async void Search()
        {
            await Navigation.PushModalAsync(new SearchPage(Text),true);
            Text = String.Empty;
        }

        private async void NavigateToMessages()
        {
            await Navigation.PushModalAsync(new MessagesPage(SelectedItem), true);
            SelectedItem = null; 
        }
    }
}