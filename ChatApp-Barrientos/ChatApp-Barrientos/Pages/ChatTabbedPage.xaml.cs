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
        //public string name1;
        //public string email1;
        //public string pass1;
        //public ChatTabbedPage(string name, string email, string pass)
        //{
        //    InitializeComponent();
        //    NavigationPage.SetHasNavigationBar(this, false);
        //    //profilePage.signout.Clicked += LogoutButton_Clicked;

        //    //profilePage.Name = name;
        //    //profilePage.Email = email;
        //    //name1 = name;
        //    //email1 = email;
        //    //pass1 = pass;

    
        

        //private async void LogoutButton_Clicked(object sender, EventArgs e)
        //{
        //    //await Navigation.PopToRootAsync(true);
        //    firebasebarrientos auth = DependencyService.Get<firebasebarrientos>();
        //    var signedOut = auth.SignOut();
        //    if (signedOut)
        //    {
        //        await Navigation.PushAsync(new MainPage(), true);
        //    }
        //    else
        //    {
        //        await DisplayAlert("Error", "Retry?","Okay");
        //    }

        //    // await Navigation.PushAsync(new MainPage(email1, name1,pass1), true);

        //}
    }
}