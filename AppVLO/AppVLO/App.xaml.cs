using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppVLO.Data;
using AppVLO.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppVLO
{
    public partial class App : Application
    {
        private static FriendDatabase database;

        public static FriendDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database =
                         new FriendDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("VLO.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage()) { BarBackgroundColor = Color.FromHex("#0B5894"), BarTextColor = Color.White };
            //MainPage = new NavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
