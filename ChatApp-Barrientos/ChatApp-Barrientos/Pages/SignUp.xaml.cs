using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_Barrientos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
   
    public partial class SignUp : ContentPage
{
        public SignUp()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
            hide1.Clicked += delegate {

                ButtonStyle1(hide1,hide2);
            };
            hide2.Clicked += delegate {

                ButtonStyle2(hide2,hide1);
            };
        }
        String name2;
        String email2;
        String pass2;
        public SignUp(string name, string email, string pass)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            hide1.Clicked += delegate {

                ButtonStyle1(hide1, hide2);
            };
            hide2.Clicked += delegate {

                ButtonStyle2(hide2, hide1);
            };
            name2 = name;
            email2 = email;
            pass2 = pass;
        }


        void ButtonStyle1(Button button,Button button2)
        {
            if (button.Text == "Show")
            {
                button.Text = "Hide";
                button2.Text = "Hide";
                PassInput.IsPassword = false;
                PassInput2.IsPassword = false;
                string pss = PassInput.ToString();
                int ln = pss.Length;
                PassInput.CursorPosition = ln;
                

            }
            else
            {
                button.Text = "Show";
                button2.Text = "Show";
                PassInput.IsPassword = true;
                PassInput2.IsPassword = true;
                string pss = PassInput.ToString();
                int ln = pss.Length;
                PassInput.CursorPosition = ln;
               
            }

        }
        void ButtonStyle2(Button button,Button button2)
        {
            if (button.Text == "Show")
            {
                button.Text = "Hide";
                button2.Text = "Hide";
                PassInput.IsPassword = false;
                PassInput2.IsPassword = false;
                string pss = PassInput.ToString();
                int ln = pss.Length;
                PassInput2.CursorPosition = ln;

            }
            else
            {
                button.Text = "Show";
                button2.Text = "Show";
                PassInput.IsPassword = true;
                PassInput2.IsPassword = true;
                string pss = PassInput.ToString();
                int ln = pss.Length;
                PassInput2.CursorPosition = ln;
            }

        }
        

        public async void Button_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(NameInput.Text) && string.IsNullOrEmpty(EmailInput.Text) && string.IsNullOrEmpty(PassInput.Text) && string.IsNullOrEmpty(PassInput2.Text)) //working
            {
                nameframe.BorderColor = Color.Red;
                emailframe.BorderColor = Color.Red;
                passframe.BorderColor = Color.Red;
                confpassframe.BorderColor = Color.Red;
                await DisplayAlert("Error", "Missing Fields", "Okay");
            }
            else if (string.IsNullOrEmpty(NameInput.Text) && string.IsNullOrEmpty(PassInput.Text) && string.IsNullOrEmpty(PassInput2.Text))
            {
                nameframe.BorderColor = Color.Red;
                passframe.BorderColor = Color.Red;
                confpassframe.BorderColor = Color.Red;
                await DisplayAlert("Error", "Missing Fields", "Okay");
            }
            else if (string.IsNullOrEmpty(EmailInput.Text) && string.IsNullOrEmpty(PassInput.Text) && string.IsNullOrEmpty(PassInput2.Text))
            {
                emailframe.BorderColor = Color.Red;
                passframe.BorderColor = Color.Red;
                confpassframe.BorderColor = Color.Red;
                await DisplayAlert("Error", "Missing Fields", "Okay");
            }
            else if (string.IsNullOrEmpty(EmailInput.Text) && string.IsNullOrEmpty(NameInput.Text) && !string.IsNullOrEmpty(PassInput.Text) && !string.IsNullOrEmpty(PassInput2.Text))
            {
                
                nameframe.BorderColor = Color.Red;
                emailframe.BorderColor = Color.Red;
                confpassframe.BorderColor = Color.Black;
                await DisplayAlert("Error", "Missing Fields", "Okay");
            }
            else if (!string.IsNullOrEmpty(EmailInput.Text) && !string.IsNullOrEmpty(NameInput.Text) && string.IsNullOrEmpty(PassInput.Text) && string.IsNullOrEmpty(PassInput2.Text))
            {
                passframe.BorderColor = Color.Red;
                confpassframe.BorderColor = Color.Red;
                await DisplayAlert("Error", "Missing Fields", "Okay");
            }
            else if (string.IsNullOrEmpty(EmailInput.Text) && !string.IsNullOrEmpty(PassInput.Text) && !string.IsNullOrEmpty(PassInput2.Text))
            {
                emailframe.BorderColor = Color.Red;
                await DisplayAlert("Error", "Missing Fields", "Okay");
            }
            else if (string.IsNullOrEmpty(NameInput.Text) && !string.IsNullOrEmpty(PassInput.Text) && !string.IsNullOrEmpty(PassInput2.Text))
            {
                nameframe.BorderColor = Color.Red;
                await DisplayAlert("Error", "Missing Fields", "Okay");
            }
            else if (!EmailInput.Text.Contains("@"))
            {
                emailframe.BorderColor = Color.Red;
                await DisplayAlert("Error", "Invalid Email", "Okay");
            }else if (!PassInput.Text.Equals(PassInput2.Text))
            {
                await DisplayAlert("Error", "Passwords don't match.", "Okay");
            }
            else
            {
                ai.IsRunning = true;
                aiLayout.IsVisible = true;
                await Task.Delay(3000);
                aiLayout.IsVisible = false;
                ai.IsRunning = false;
                await DisplayAlert("Success", "Sign up successful. Verification email sent.", "Okay");
                await Navigation.PushAsync(new MainPage(EmailInput.Text, NameInput.Text, PassInput.Text),true);
            }
        }
        private async void OnSignUp(object sender, EventArgs args)
        {
            try
            {
                await DisplayAlert("Success", "Sign up successful. Verification email sent.", "Okay");
                await Navigation.PushAsync(new MainPage(email2, name2,pass2), true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async void Button_Clicked2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(email2, name2,pass2), true);
        }

        void StartCall1(object sender, EventArgs args)
        {
            nameframe.BorderColor = Color.Black;
            
        }
        void StartCall2(object sender, EventArgs args)
        {
            passframe.BorderColor = Color.Black;

        }
        void StartCall3(object sender, EventArgs args)
        {
            emailframe.BorderColor = Color.Black;

        }
        void StartCall4(object sender, EventArgs args)
        {
            confpassframe.BorderColor = Color.Black;

        }

        
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            EmailInput.Text = string.Empty;
            NameInput.Text = string.Empty;
            PassInput.Text = string.Empty;
            PassInput2.Text = string.Empty;

        }

    }
}