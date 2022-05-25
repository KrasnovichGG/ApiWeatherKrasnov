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
        TodoItemModel a;
        CancellationTokenSource stc;
        Pin krasnovichPin = new Pin();
        public TodoListPage()
        {
            InitializeComponent();
            //var res = GetLocation();
            krasnovichPin.Label = "Техникум Связи";
            krasnovichPin.Address = "Город";
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
                krasnovichPin.Label = a.name;
                krasnovichPin.Position = new Position(a.coord.lat,a.coord.lon);
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
                a = await App.TodoManager.GetTodoItemModels(searchbarcity.Text);
                BindingContext = a.main;
                sashaCloud.Text = $"{a.weather[0].description} на улице";
                Temp.Text = $"{a.main.temp}°C - градусов цельсия";
                Fells.Text = $"{a.main.feels_like}°C - ощущение";
                Wind.Text = $"{a.wind.speed} - м.с ветер!";
                Hum.Text = $"{a.main.humidity} - г/м³Влажность";
                Pressure.Text = $"{a.main.pressure} - атм Давление";
                imagecloud.Source = ImageSource.FromUri(new Uri($"http://openweathermap.org/img/wn/{a.weather[0].icon}.png"));
                await GetLocation();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Введите название города\n{ex.Message}", "Ok");
            }
        }
    }
}