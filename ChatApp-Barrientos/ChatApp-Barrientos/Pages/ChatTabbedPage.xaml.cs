using ChatApp_Barrientos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp_Barrientos.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_Barrientos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ChatTabbedPage : TabbedPage
{
        DataClass dataClass = DataClass.GetInstance;
        public ChatTabbedPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        profilePage.Name = dataClass.loggedInUser.name;
        profilePage.Email = dataClass.loggedInUser.email;
        }
        
    }
}