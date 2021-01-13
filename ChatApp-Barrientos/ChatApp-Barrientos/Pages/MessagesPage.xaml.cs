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
public partial class MessagesPage : ContentPage
{
        DataClass dataClass = DataClass.GetInstance;
        public ICommand PopModalCommand => new Command(async () => await Navigation.PopModalAsync(true));
        public ICommand SendCommand => new Command(Send);
        
        ContactModel ConversationPartner;
        public MessagesPage(ContactModel input)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ConversationsList = new ObservableCollection<ConversationModel>();
            //ContactModel ConversationPartner = new ContactModel();
            ConversationPartner = input;
            CrossCloudFirestore.Current
                .Instance.GetCollection("contacts")
                .GetDocument(ConversationPartner.id)
                .GetCollection("conversations")
                .OrderBy("created_at", false)
                .AddSnapshotListener((snapshot, error) =>
                {
                    if (snapshot != null)
                    {
                        
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                            var obj = JsonConvert.DeserializeObject<ConversationModel>(json);
                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    ConversationsList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (ConversationsList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = ConversationsList.Where(c => c.id == obj.id).FirstOrDefault();
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (ConversationsList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = ConversationsList.Where(c => c.id == obj.id).FirstOrDefault();
                                        ConversationsList.Remove(item);
                                    }
                                    break;
                            }
                        }
                        
                        if (ConversationsList.Count != 0)
                        {
                            MessagingCenter.Send<object, object>(this, "LastMessage", ConversationsList.Last());
                            MessagesIsEmpty = false;
                        }
                        else
                        {
                            MessagesIsEmpty = true;
                        }
                    }
                });
        }

        private ObservableCollection<ConversationModel> conversationsList;
        public ObservableCollection<ConversationModel> ConversationsList
        {
            get { 
                return conversationsList; 
            }
            set {
                conversationsList = value; 
                OnPropertyChanged(nameof(ConversationsList));
            }
        }

        

        private string message;
        public string Message
        {
            get { 
                return message; 
            }
            set {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private bool messagesIsEmpty;
        public bool MessagesIsEmpty
        {
            get { 
                return messagesIsEmpty; 
            }
            set {
                messagesIsEmpty = value;
                OnPropertyChanged(nameof(MessagesIsEmpty));
            }
        }
        

        private async void Send()
        {
            Guid guid = Guid.NewGuid();
            string ID = guid.ToString();
            ConversationModel conversationObject = new ConversationModel()
            {
                id = ID,
                converseeID = dataClass.loggedInUser.uid,
                message = Message,
                created_at = DateTime.UtcNow.ToString()
            };
            await CrossCloudFirestore.Current
                    .Instance
                    .GetCollection("contacts")
                    .GetDocument(ConversationPartner.id)
                    .GetCollection("conversations")
                    .GetDocument(ID)
                    .SetDataAsync(conversationObject);
            Message = string.Empty;
        }
    }
}