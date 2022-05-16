using ApiWeatherKrasnov.Model;
using ApiWeatherKrasnov.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiWeatherKrasnov.Services
{
    public class TodoManager
    {
        IRestService service;

        public TodoManager(IRestService restService)
        {
            service = restService;
        }
        public Task<TodoItemModel> GetTodoItemModels(string item)
        {
            return service.GetTodoItemAsync(item);
        }
    }

    
}    
   

    
        
    

