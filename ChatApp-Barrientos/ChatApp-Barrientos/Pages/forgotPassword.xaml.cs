using ChatApp_Barrientos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp_Barrientos.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_Barrientos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class forgotPassword : ContentPage
    {
        public forgotPassword()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void send_email(object sender, EventArgs e)
        {

            if (sendemail.Text.Length == 0)
            {
                await DisplayAlert("Error", "Missing fields", "Okay");
            }
            else
            {
                ai.IsRunning = true;
                aiLayout.IsVisible = true;
                await Task.Delay(500);
                aiLayout.IsVisible = false;
                ai.IsRunning = false;
                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<firebasebarrientos>().ResetPassword(ForgotPasswordInput.Text);
                //   await DisplayAlert("asd", ForgotPasswordInput.Text, "ok");
                if (res.Status == true)
                {
                    await DisplayAlert("Success", res.Response, "Okay");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", res.Response, "Okay");
                }
            }

        }
    }
}