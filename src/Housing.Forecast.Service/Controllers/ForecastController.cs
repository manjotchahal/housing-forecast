using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Housing.Forecast.Context;
using Housing.Forecast.Context.Repos;
using System.Collections.Generic;

namespace Housing.Forecast.Service.Controllers
{
    [Route("api/[controller]")]
    public class ForecastController : BaseController
    {
        private readonly SnapshotRepo _snapshot;
        public ForecastController(ILoggerFactory loggerFactory, IQueueClient queueClientSingleton)
          : base(loggerFactory, queueClientSingleton) { }

        /// <summary>
        /// This endpoint will return all unique locations of rooms
        /// </summary>
        /// <return>
        /// Return a list of all unique locations of rooms.
        /// </return>
        // GET: api/forecast/Locations
        [HttpGet("Locations")]
        public IActionResult GetLocations()
        {
            try
            {

                List<string> locations = _snapshot.GetLocations();
                if (locations.Count == 0)
                {
                    return NotFound("No locations found.");
                }
                return Ok(locations);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return BadRequest("Something went wrong while processing the request.");
            }
        }

        protected override void UseReceiver()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ReceiverExceptionHandler)
            {
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(ReceiverMessageProcessAsync, messageHandlerOptions);
        }

        protected override void UseSender(Message message)
        {
            Task.Run(() =>
              SenderMessageProcessAsync(message)
            );
        }
    }
}
