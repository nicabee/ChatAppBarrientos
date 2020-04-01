using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace ChatApp_Barrientos
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }
        public string em,em1;
        public string na,na1;
        public string ps;
        public MainPage(string email, string name ,string pass)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            em = email;
            na = name;
            ps = pass;
            em1 = email;
            na1 = name;
        }

        //public MainPage(string email, string name)
        //{
        //    InitializeComponent();
        //    NavigationPage.SetHasNavigationBar(this, false);
        //    em1 = email;
        //    na1 = name;

        //}



        private async void Button_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(PassInput.Text) && string.IsNullOrEmpty(EmailInput.Text))
            {
                passframe.BorderColor = Color.Red;
                emailframe.BorderColor = Color.Red;
                await DisplayAlert("Error", "Missing Fields", "Okay");
            }else if (string.IsNullOrEmpty(PassInput.Text))
            {
                passframe.BorderColor = Color.Red;
                await DisplayAlert("Error", "Missing Fields", "Okay");
            }
            else if (string.IsNullOrEmpty(EmailInput.Text))
            {
                emailframe.BorderColor = Color.Red;
                await DisplayAlert("Error", "Missing Fields", "Okay");
            }
            else if (!EmailInput.Text.Contains("@"))
            {
                emailframe.BorderColor = Color.Red;
                await DisplayAlert("Error", "Invalid Email", "Okay");
            }else if (!EmailInput.Text.Equals(em1) || !PassInput.Text.Equals(ps))
            {
                ai.IsRunning = true;
                aiLayout.IsVisible = true;
                await Task.Delay(1000);
                aiLayout.IsVisible = false;
                ai.IsRunning = false;
                await DisplayAlert("Error", "Email/Password is wrong", "Okay");
            }
            else
            {
                ai.IsRunning = true;
                aiLayout.IsVisible = true;
                await Task.Delay(1000);
                aiLayout.IsVisible = false;
                ai.IsRunning = false;
                await Navigation.PushAsync(new ChatTabbedPage(na1, em1,ps), true);
          
            }
        }
        private async void OnLogin(object sender, EventArgs args)
        {
            try
            {
                ai.IsRunning = true;
                aiLayout.IsVisible = true;
                await Task.Delay(1000);
                aiLayout.IsVisible = false;
                ai.IsRunning = false;
                await Navigation.PushAsync(new ChatTabbedPage(na1,em1,ps), true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async void OnLoginFB(object sender, EventArgs args)
        {
            try
            {
                ai.IsRunning = true;
                aiLayout.IsVisible = true;
                await Task.Delay(1000);
                aiLayout.IsVisible = false;
                ai.IsRunning = false;
                await Navigation.PushAsync(new ChatTabbedPage(na1, em1,ps), true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void StartCall1(object sender, EventArgs args)
        {
            emailframe.BorderColor = Color.Black;
        }
        void StartCall2(object sender, EventArgs args)
        {
            passframe.BorderColor = Color.Black;
            
        }
        void hidesh(object sender, EventArgs e)
        {
            Button button = sender as Button;
            
            if (button.Text == "Show")
              {
               button.Text = "Hide";
               PassInput.IsPassword = false;
                string pss = PassInput.ToString();
                int ln = pss.Length;
                PassInput.CursorPosition = ln;
              }
            else
              {
                button.Text = "Show";
                PassInput.IsPassword = true;
                string pss = PassInput.ToString();
                int ln = pss.Length;
                PassInput.CursorPosition = ln;
            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            EmailInput.Text = string.Empty;
            PassInput.Text = string.Empty;
            
        }
        
        private async void signup_Clicked(object sender, EventArgs e)
        {
            if(na1 != "" && em1 != "")
            {
                await Navigation.PushAsync(new SignUp(na1,em1,ps), true);
            }
            else
            {
                await Navigation.PushAsync(new SignUp(), true);
            }
            
        }
    }
}
