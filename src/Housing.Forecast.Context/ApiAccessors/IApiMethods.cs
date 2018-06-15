using System;
using System.Collections.Generic;

namespace Housing.Forecast.Context.ApiAccessors
{
    public interface IApiMethods
    {
        ICollection<T> HttpGetFromApi<T>(string apiString);
    }
}