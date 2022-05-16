using ApiWeatherKrasnov.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApiWeatherKrasnov.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoListPage : ContentPage
    {
        public TodoListPage()
        {
            InitializeComponent();
        }
   
        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            try
            {
                TodoItemModel a = await App.TodoManager.GetTodoItemModels(searchbarcity.Text);
                BindingContext = a.main;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Введите название города\n{ex.Message}", "Ok");
            }
        }
    }
}