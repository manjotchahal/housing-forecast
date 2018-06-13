using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Housing.Forecast.Context;
using Housing.Forecast.Context.Repos;
using System.Collections.Generic;
using System.Linq;
using Housing.Forecast.Library.Models;
using System.Globalization;

namespace Housing.Forecast.Service.Controllers
{
    [Route("api/[controller]")]
    public class ForecastController : BaseController
    {
        private readonly SnapshotRepo _snapshot;
        public ForecastController(ILoggerFactory loggerFactory, IQueueClient queueClientSingleton, IRepo<Snapshot> snapshot)
          : base(loggerFactory, queueClientSingleton) { _snapshot = (SnapshotRepo)snapshot; }

        /// <summary>
        /// This endpoint will return all unique locations of snapshots
        /// </summary>
        /// <return>
        /// Return a list of all unique locations of snapshots.
        /// </return>
        // GET: api/forecast/Locations
        [HttpGet("Locations")]
        public async Task<IActionResult> GetLocations()
        {
            try
            {
                List<string> locations = _snapshot.GetLocations().ToList();
                if (locations.Count == 0)
                {
                    return await Task.Run(() => NotFound("No locations found.")); // No snapshots found in the DB.
                }
                return await Task.Run(() => Ok(locations)); // Return all of the distinct locations the snapshots are tied to.

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message); // Log the error
                return await Task.Run(() => BadRequest("Something went wrong while processing the request."));
            }
        }

        /// <summary>
        /// This endpoint will return all of the snapshots to the caller.
        /// </summary>
        /// <return>
        /// Return all of the snapshots in the database.
        /// </return>
        // GET: api/forecast/Snapshots
        [Route("Snapshots")]
        [HttpGet]
        public async Task<IActionResult> Get() // .NET Core doesn't have IHttpActionResult instead we use IActionResult
        {
            try
            {
                List<Snapshot> snapshots = _snapshot.Get().ToList();

                if (snapshots == null)
                {
                    return await Task.Run(() => NotFound("There are no snapshots in the database."));
                }

                return await Task.Run(() => Ok(snapshots));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return await Task.Run(() => BadRequest("Error occurred while processing request."));
            }
        }

        /// <summary>
        /// Return the snapshot that was created on the provided date.
        /// </summary>
        /// <remarks>
        /// The format for date is yyyy-mm-dd
        /// </remarks>
        /// <param name="date">The date of the snapshot.</param>
        /// <return>
        /// Return the snapshot that was created on the provided date.
        /// </return>
        // GET: api/forecast/Snapshots/createdDate
        [HttpGet("Snapshots/{date:datetime}")]
        public async Task<IActionResult> Get(DateTime date)
        {
            try
            {
                // check if the models are correct?
                if (!Validate(date))
                {
                    return await Task.Run(() => BadRequest("Not valid input"));
                }

                List<Snapshot> snapshots = _snapshot.GetByDate(date).ToList();
                if (snapshots == null)
                {
                    // Let's create a new snapshot for the requested date
                    snapshots = CreateSnapshot(date);
                    if (snapshots == null)
                    {
                        return await Task.Run(() => NotFound("No snapshots found with the passed search critiea."));
                    }
                }

                return await Task.Run(() => Ok(snapshots));

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return await Task.Run(() => BadRequest("Something went wrong while processing the request."));
            }
        }

        /// <summary>
        /// This endpoint will find and return all snapshots were cover the provided date range.
        /// </summary>
        /// <format>The format for the dates is yyyy-mm-dd.</format>
        /// <param name="startDate">The starting point of the search.</param>
        /// <param name="endDate">The ending point of the saerch.</param>
        /// <returns>
        /// Return a list of all snapshots that created between the two provided dates.
        /// </returns>
        // GET: api/forecast/Snapshots/start/end
        [HttpGet("SnapshotsRange/{startDate:datetime}/{endDate:datetime}")]
        public async Task<IActionResult> Get(DateTime startDate, DateTime endDate)
        {
            try
            {
                // check if the models are correct?
                if (!Validate(startDate, endDate))
                {
                    return await Task.Run(() => BadRequest("Not valid input"));
                }

                List<Snapshot> snapshots = _snapshot.GetBetweenDates(startDate, endDate).ToList();

                if (snapshots == null)
                {
                    // Let's create a new snapshot for the requested date
                    snapshots = CreateSnapshot(startDate, endDate);
                    if (snapshots == null)
                    {
                        return await Task.Run(() => NotFound("No snapshots found with the passed search critiea."));
                    }
                }

                return await Task.Run(() => Ok(snapshots));

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return await Task.Run(() => BadRequest("Something went wrong while processing the request."));
            }
        }

        /// <summary>
        /// This endpoint will find and return all snapshots for the provided location and cover the provided date range.
        /// </summary>
        /// <format>The format for the dates is yyyy-mm-dd and the format for location is a string of aplhabet characters.</format>
        /// <param name="startDate">The starting point of the search date range.</param>
        /// <param name="endDate">The ending point of the search date range.</param>
        /// <param name="location">The City name the snapshots should cover.</param>
        /// <returns>
        /// Returns a list of all snapshots for the specified city location that cover the provided search dates.
        /// </returns>
        // GET: api/forecast/Snapshots/start/end/location
        [HttpGet("SnapshotsByLocation/{startDate:datetime}/{endDate:datetime}/{location:alpha}")]
        public async Task<IActionResult> Get(DateTime startDate, DateTime endDate, string location)
        {
            try
            {
                if (!String.IsNullOrEmpty(location))
                {
                    location.ToLower(); // make location to be lowercase
                }

                if (location == "all")
                {
                    // Redirect the call to another endpoint
                    return RedirectToAction("Get", new { startDate, endDate });
                }

                // check if the models are correct?
                if (!Validate(startDate, endDate, location))
                {
                    return await Task.Run(() => BadRequest("Not valid input"));
                }

                TextInfo text = new CultureInfo("en-US", false).TextInfo;
                List<Snapshot> snapshots = _snapshot.GetBetweenDatesAtLocation(startDate, endDate, text.ToTitleCase(location)).ToList();
                if (snapshots == null)
                {
                    // Let's create a new snapshot for the requested date
                    snapshots = CreateSnapshot(startDate, endDate, location);
                    if (snapshots == null)
                        return await Task.Run(() => NotFound("No snapshots found with the passed search critiea."));
                }

                return await Task.Run(() => Ok(snapshots));

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return await Task.Run(() => BadRequest("Something went wrong while processing the request."));
            }
        }

        /// <summary>
        /// validate the user's input
        /// </summary>
        /// <param name="start">The earliest date the snapshot should have been created on.</param>
        /// <param name="end">The lastest date the snapshot shold have beed created on.</param>
        /// <param name="location">The city name the snapshot is tied to.</param>
        /// <returns>
        /// Returns true if all inputs are valid otherwise returns false if any input fails
        /// </returns>
        private bool Validate(DateTime start, DateTime? end = null, string location = null)
        {
            try
            {
                // First let's find the earlist snapshot date
                var earlist = _snapshot.Get().Min(x => x.Date);

                // The City locations that are supported for the search
                var cities = _snapshot.GetLocations().Distinct().ToList();

                // Remove 'All' from the cities list
                cities.Remove("All");
                
                foreach (var city in cities)
                {
                    city.ToLower();
                }

                if (end == null)
                {
                    // Only need to validate start to see that it's on/after the earliest snapshot date.
                    if (start < earlist)
                    {
                        return false; // Failed
                    }
                }
                else if (location == null)
                {
                    // Need to make sure that start is on/after the earlist snapshot date and that end is after start.
                    if (start < earlist || start > end)
                    {
                        return false; // failed
                    }
                }
                else
                {
                    // Validate all three inputs
                    if (start < earlist || start > end || cities.IndexOf(location) < 0)
                    {
                        return false; // Failed
                    }
                }
                return true; // Passed all validation checks
            }
            catch (Exception ex)
            {
                // There was an erro while trying to validate the user's input so log the error return false
                logger.LogError(ex.Message);
                return false;
            }
        }

        private List<Snapshot> CreateSnapshot(DateTime start, DateTime? end = null, string location = null)
        {
            try
            {
                var _room = new RoomRepo(new ForecastContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ForecastContext>()));
                var _user = new UserRepo(new ForecastContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ForecastContext>()));
                List<Room> rooms = new List<Room>();
                List<User> users = new List<User>();
                TextInfo text = new CultureInfo("en-US", false).TextInfo;
                List<Snapshot> snapshots = new List<Snapshot>();

                if (end == null)
                {
                    rooms = _room.GetByDate(start).ToList();
                    users = _user.GetByDate(start).ToList();

                    var snapshot = new Snapshot()
                    {
                        Date = start,
                        RoomCount = rooms.Count,
                        UserCount = users.Count,
                        Location = "All",
                        Created = DateTime.Now
                    };

                    snapshots.Add(snapshot);
                }
                else if (location == null)
                {
                    for (DateTime i = start; i <= end; i.AddDays(1))
                    {
                        rooms = _room.GetByDate(i).ToList();
                        users = _user.GetByDate(i).ToList();

                        var snapshot = new Snapshot()
                        {
                            Date = start,
                            RoomCount = rooms.Count,
                            UserCount = users.Count,
                            Location = "All",
                            Created = DateTime.Now
                        };

                        snapshots.Add(snapshot);
                    }
                }
                else
                {
                    for (DateTime i = start; i <= end; i.AddDays(1))
                    {
                        rooms = _room.GetByLocation(i, location).ToList();
                        users = _user.GetByLocation(i, location).ToList();

                        var snapshot = new Snapshot()
                        {
                            Date = start,
                            RoomCount = rooms.Count,
                            UserCount = users.Count,
                            Location = text.ToTitleCase(location),
                            Created = DateTime.Now
                        };

                        snapshots.Add(snapshot);
                    }
                }

                using (ForecastContext db = new ForecastContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ForecastContext>()))
                {
                    foreach (var snap in snapshots)
                    {
                        // Added the new snapshots to the database here.
                        db.Snapshots.Add(snap);
                    }
                    db.SaveChanges(); // Save the changes to the database
                }

                return snapshots;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null; // return null
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
