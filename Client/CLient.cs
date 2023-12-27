using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Test2.Models;

namespace Client
{
    public class CLient
    {
        private readonly string apiUrl;

        public CLient(string apiBaseUrl)
        {
            apiUrl = $"{apiBaseUrl}/api/Users/GetByLogin"; // Замените на фактический URL вашего API
        }
        
        public async Task<User> GetUserByLogin(string login)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{apiUrl}/{login}");

                if (response.IsSuccessStatusCode)
                {
                    string userJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(userJson);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null; // Возвращаем null, если пользователь не найден
                }
                else
                {
                    // Обработка других ошибок
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }
            }
        }

        // Добавьте другие методы для работы с API по мере необходимости (POST, PUT, DELETE и т.д.)
    }

}
