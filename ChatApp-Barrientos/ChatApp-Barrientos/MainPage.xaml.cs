using ChatApp_Barrientos.Interfaces;
using ChatApp_Barrientos.Models;
using ChatApp_Barrientos.Helpers;
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
        DataClass dataClass = DataClass.GetInstance;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            EmailInput.Text = dataClass.loggedInUser != null ? dataClass.loggedInUser.email : "";

        }
        public string em,em1;
        public string na,na1;
        public string ps;
        



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
            }
           
            else
            {
                ai.IsRunning = true;
                aiLayout.IsVisible = true;
                await Task.Delay(1000);
                aiLayout.IsVisible = false;
                ai.IsRunning = false;
                //var log = DependencyService.Get<firebasebarrientos>();
                //string token = await log.doLogin(EmailInput.Text, PassInput.Text);
                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<firebasebarrientos>().LoginWithEmailPassword(EmailInput.Text, PassInput.Text);

                if (res.Status == true)
                {
                    Application.Current.MainPage = new ChatTabbedPage();
                  
                }
                else
                {
                    bool retryBool = await DisplayAlert("Error", res.Response + " Retry?", "Yes", "No");
                    if (retryBool)
                    {
                        EmailInput.Text = string.Empty;
                        PassInput.Text = string.Empty;
                        EmailInput.Focus();
                    }
                }
                

            }
        }
       
        private async void onForgotPassword(object sender, EventArgs args)
        {
            try
            {
                ai.IsRunning = true;
                aiLayout.IsVisible = true;
                await Task.Delay(500);
                aiLayout.IsVisible = false;
                ai.IsRunning = false;
                await Navigation.PushModalAsync(new forgotPassword(), true);
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
            //if(na1 != "" && em1 != "")
            //{
            //    await Navigation.PushAsync(new SignUp(na1,em1,ps), true);
            //}
            //else
            {
                await Navigation.PushAsync(new SignUp(), true);
            }
            
        }
    }
}
