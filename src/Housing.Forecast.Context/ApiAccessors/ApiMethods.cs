using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;

namespace Housing.Forecast.Context.ApiAccessors
{
    /// <summary>
    /// An ApiMethods instance is used to request resources from the service hub.
    /// </summary>
    public class ApiMethods : IApiMethods
    {
        /// <summary>
        /// A ForecastContext instance represents a session with the database and is used to query and save instances of the entities.
        /// </summary>
        /// <returns>
        /// A ForecastContext instance contains DbSets of Users, Rooms, Batches, Addresses, and Names.
        /// </returns>
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