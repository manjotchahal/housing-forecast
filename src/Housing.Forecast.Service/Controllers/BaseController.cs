using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Housing.Forecast.Service.Controllers
{
  public abstract class BaseController : Controller
  {
    protected readonly ILogger logger;

    protected BaseController(ILoggerFactory loggerFactory)
    {
      logger = loggerFactory.CreateLogger(this.GetType().Name);
    }
  }
}
