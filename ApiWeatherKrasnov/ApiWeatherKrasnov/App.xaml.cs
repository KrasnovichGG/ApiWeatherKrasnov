using ApiWeatherKrasnov.Services;
using ApiWeatherKrasnov.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApiWeatherKrasnov
{
    public partial class App : Application
    {
        public static TodoManager TodoManager { get; set; }
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
            TodoManager = new TodoManager(new RestService());
            MainPage = new NavigationPage(new MainPage());
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
