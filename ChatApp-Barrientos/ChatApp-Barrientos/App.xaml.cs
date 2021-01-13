using ChatApp_Barrientos.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChatApp_Barrientos.Helpers;

namespace ChatApp_Barrientos
{
    public partial class App : Application
    {
        public static float screenWidth { get; set; }
        public static float screenHeight { get; set; }
        public static float appScale { get; set; }

        DataClass dataClass = DataClass.GetInstance;
        public App()
        {
            InitializeComponent();
            //auth = DependencyService.Get<firebasebarrientos>();
            //if (auth.IsSignIn())
            //{
            //    MainPage = new NavigationPage(new ChatTabbedPage());
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new MainPage());
            //}
            //  MainPage = new NavigationPage(new MainPage());
            if (dataClass.isSignedIn)
            {
                Application.Current.MainPage = new ChatTabbedPage();
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
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
