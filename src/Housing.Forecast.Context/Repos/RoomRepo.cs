using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Housing.Forecast.Context;
using Housing.Forecast.Context.Models;

namespace Housing.Forecast.Context.Repos
{
    public class RoomRepo : IRepo<Room>
    {
        private readonly IForecastContext _context;
        public RoomRepo(IForecastContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Find all of the distinct locations for every snapshot.
        /// </summary>
        /// <returns>
        /// This method will return a list of all distinct locations for the snapshots within the database.
        /// </returns>
        public IEnumerable<string> GetLocations()
        {
            var result = AsyncGetLocations();
            return result.Result;
        }
        private async Task<IEnumerable<string>> AsyncGetLocations() {
            return await Task.Factory.StartNew(() => {
                return _context.Rooms.Select(s => s.Location).Where(s => s != null).Distinct().ToList();
            });
        }

        public IEnumerable<Room> Get()
        {
            var result = AsyncDBCall(x => x != null);
            return result.Result;
        }

        public IEnumerable<Room> GetByLocation(DateTime datetime, string location)
        {
            var result = AsyncDBCall(r => r.Created.Date <= datetime.Date && (r.Deleted.Value == null || r.Deleted.Value.Date > datetime.Date) && r.Location == location);
            return result.Result;
        }

        public IEnumerable<Room> GetByDate(DateTime datetime)
        {
            var result = AsyncDBCall(r => r.Created.Date <= datetime.Date && (r.Deleted.Value == null || r.Deleted.Value.Date > datetime.Date));
            return result.Result;
        }

        /// <summary>
        /// Perform an asynchronous call to the database to retrieve data.
        /// </summary>
        /// <param name="lambda">The search condition in the form of a lambda expression.</param>
        /// <returns>
        /// A Task for the search result set.
        /// </returns>
        private async Task<IEnumerable<Room>> AsyncDBCall(Func<Room, bool> lamdba) {
            var task = await Task.Factory.StartNew(() => {
                return _context.Rooms.Where(lamdba).ToList();
            });
            return task;
        }
    }
}
