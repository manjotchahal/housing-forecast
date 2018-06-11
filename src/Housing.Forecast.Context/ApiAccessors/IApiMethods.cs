using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Forecast.Context.ApiAccessors
{
    public interface IApiMethods
    {
        ICollection<T> HttpGetFromApi<T>(string apiString);
    }
}