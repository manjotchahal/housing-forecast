using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Forecast.Context.ApiAccessors
{
    public interface IApiMethods
    {
        ICollection<T> HttpGetFromApi<T>(string portNumber, string model);
    }
}