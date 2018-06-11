using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;
using Housing.Forecast.Service.Controllers;

namespace Housing.Forecast.Context.ApiAccessors
{
    public class ApiMethods : IApiMethods
    {
        public ICollection<T> HttpGetFromApi<T>(string apiString)
        {
            ICollection<T> resultList = null;
            using (var client = new HttpClient())
            {
                // TODO: get actual URI string
                client.BaseAddress = new Uri(/*ServiceController.serviceUri.ToString() + */$"/api/{apiString}");
                var responseTask = client.GetAsync($"{apiString}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    resultList = JsonConvert.DeserializeObject<ICollection<T>>(readTask.Result);
                }
                else
                {
                    resultList = (ICollection<T>)Enumerable.Empty<T>();
                }
                return resultList;
            }
        }
    }
}