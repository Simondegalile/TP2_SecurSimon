using System;
using TP2_SecurSimon.Services;
using TP2_SecurSimon.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TP2_SecurSimon
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
