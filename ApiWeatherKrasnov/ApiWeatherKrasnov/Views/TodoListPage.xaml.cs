using ApiWeatherKrasnov.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ApiWeatherKrasnov.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoListPage : ContentPage
    {
        CancellationTokenSource stc;
        Pin krasnovichPin = new Pin();
        public TodoListPage()
        {
            InitializeComponent();
            var res = GetLocation();
            krasnovichPin.Label = "Техникум Связи";
            krasnovichPin.Address = "KTITS";
            krasnovichPin.Position = new Position(55.8016314,49.175043);
            MyMap.Pins.Add(krasnovichPin);
        }
        async Task<Location> GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(15));
            stc = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync(request,stc.Token);
            if (location != null)
            {
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), new Distance(100d)));
            }
            return location;

        }
        protected override void OnAppearing()
        {
            if(stc != null && !stc.IsCancellationRequested)
            {
                stc.Cancel();
            }
            base.OnAppearing();
        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            try
            {
                TodoItemModel a = await App.TodoManager.GetTodoItemModels(searchbarcity.Text);
                BindingContext = a.main;
                Temp.Text += "°C - градусов цельсия";
                Fells.Text += "°C - ощущение";
                Wind.Text += $"{a.wind.speed} - м.с ветер!";
                Hum.Text += " - г/м³Влажность";
                Pressure.Text += " - атм Давление";

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Введите название города\n{ex.Message}", "Ok");
            }
        }
    }
}