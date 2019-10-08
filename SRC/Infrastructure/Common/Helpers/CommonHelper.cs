using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class CommonHelper
    {
        public static void WriteConsole(string message, ConsoleType type = ConsoleType.Infor)
        {
            Console.Write(DateTime.Now.ToString("HH:mm:ss.fff dd-MM-yyyy"));
            Console.Write(" - ");
            switch(type)
            {
                case ConsoleType.Infor:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("infor");
                    Console.ResetColor();
                    break;
                case ConsoleType.Warnning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("warrning");
                    Console.ResetColor();
                    break;
                case ConsoleType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("error");
                    Console.ResetColor();
                    break;
            }
            Console.Write(" - ");
            Console.WriteLine(message);
        }

        public static Assembly GetAssemblyByName(string name)
        {
            return GlobalConfig.Modules.FirstOrDefault(assembly => assembly.Name == name)?.Assembly;
        }

        public static TResult Mapper<Tin, TResult>(Tin input) where TResult : Tin
        {
            TResult result = (TResult)Activator.CreateInstance(typeof(TResult));

            var destPro = result.GetType().GetProperties();
            foreach (PropertyInfo p in input.GetType().GetProperties())
            {
                var des = destPro.Where(d => d.Name == p.Name).FirstOrDefault();
                if (des != null)
                {
                    des.SetValue(result, p.GetValue(input));
                }
            }

            return result;
        }

        public static async Task<UserHttpResponse> HttpGet(string baseUri, string path, string accessToken = "", string lang = "")
        {
            //Create httpClient
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("AccessToken", accessToken);
            client.DefaultRequestHeaders.Add("Lang", lang);
            client.BaseAddress = new Uri(baseUri);

            //Request
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                return new UserHttpResponse()
                {
                    IsSuccess = true,
                    StatusCode = response.StatusCode,
                    Content = await response.Content.ReadAsStringAsync()
                };
            }

            return new UserHttpResponse()
            {
                IsSuccess = false,
                StatusCode = response.StatusCode
            };
        }

        public static async Task<UserHttpResponse> HttpPost<T>(string baseUri, string path, T data, string accessToken = "", string lang = "")
        {
            //Create httpClient
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("AccessToken", accessToken);
            client.DefaultRequestHeaders.Add("Lang", lang);
            client.BaseAddress = new Uri(baseUri);

            //Request
            HttpResponseMessage response = await client.PostAsync(path, new StringContent(JsonConvert.SerializeObject(data),
                                                                                          System.Text.Encoding.UTF8,
                                                                                          "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return new UserHttpResponse()
                {
                    IsSuccess = true,
                    StatusCode = response.StatusCode,
                    Content = await response.Content.ReadAsStringAsync()
                };
            }

            return new UserHttpResponse()
            {
                IsSuccess = false,
                StatusCode = response.StatusCode
            };
        }
    }
}
