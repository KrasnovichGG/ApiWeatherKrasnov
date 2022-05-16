using ApiWeatherKrasnov.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiWeatherKrasnov.Views
{
    public interface IRestService
    {
        Task<TodoItemModel> GetTodoItemAsync(string item);
    }
}
