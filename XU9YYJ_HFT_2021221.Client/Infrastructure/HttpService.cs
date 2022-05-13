using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using XU9YYJ_HFT_2021221.Models.Models;

namespace XU9YYJ_HFT_2021221.Client.Infrastructure
{
    public class HttpService
    {
        string controllerName;

        Uri baseAddress;

        JsonSerializerOptions serializerOptions;

        public HttpService(string controllerName, string baseAddress)
        {
            this.controllerName = controllerName;
            this.baseAddress = new Uri(baseAddress);
            serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        }
        private string GetActionName(string actionName)
        {
            string actionName1 = actionName;
            Console.WriteLine($"----------{controllerName}/{actionName1}----------");
            return $"{controllerName}/{actionName1}";
        }

        private void InitClient(HttpClient client)
        {
            client.BaseAddress = baseAddress;
        }
        public List<T> GetAll<T>(string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.GetAsync(GetActionName(actionName ?? nameof(GetAll))).GetAwaiter().GetResult(); // Block here
                return JsonSerializer.Deserialize<List<T>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), serializerOptions);


            }
        }

        public T Get<T, TKey>(TKey id, string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.GetAsync($"{GetActionName(actionName ?? nameof(Get))}/{id}").GetAwaiter().GetResult(); // Block here
                return JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), serializerOptions);
            }
        }

        public ApiResult Create<T>(T entity, string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.PostAsJsonAsync(GetActionName(actionName ?? nameof(Create)), entity).GetAwaiter().GetResult(); // Block here
                return JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), serializerOptions);
            }
        }

        public ApiResult Update<T>(T entity, string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.PutAsJsonAsync(GetActionName(actionName ?? nameof(Update)), entity).GetAwaiter().GetResult(); // Block here
                return JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), serializerOptions);
            }
        }

        public ApiResult Delete<TKey>(TKey id, string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.DeleteAsync($"{GetActionName(actionName ?? nameof(Delete))}/{id}").GetAwaiter().GetResult(); // Block here
                return JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), serializerOptions);
            }
        }


    }
}
