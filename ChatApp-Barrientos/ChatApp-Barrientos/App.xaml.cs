using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_Barrientos
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            base.OnStart();
            Console.WriteLine("OnStart");

        }

        protected override void OnSleep()
        {
            base.OnSleep();
            Console.WriteLine("OnSleep");
        }

        protected override void OnResume()
        {
            base.OnResume();
            Console.WriteLine("OnResume");
        }

    }
}
