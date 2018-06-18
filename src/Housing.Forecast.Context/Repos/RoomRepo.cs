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

        public IEnumerable<string> GetLocations()
        {
            var task = Task.Factory.StartNew(() => {
                return _context.Rooms.Select(r => r.Location).Where(r => r != null).Distinct();
            });
            task.Wait();
            return task.Result;
        }

        public IEnumerable<Room> Get()
        {
            var task = Task.Factory.StartNew(() => {
                return _context.Rooms;
            });
            task.Wait();
            return task.Result;
        }

        public IEnumerable<Room> GetByLocation(DateTime datetime, string location)
        {
            var task = Task.Factory.StartNew(() => {
                return _context.Rooms.Where(r => r.Created.Date <= datetime.Date && (r.Deleted.Value == null || r.Deleted.Value.Date > datetime.Date) && r.Location == location);
            });
            task.Wait();
            return task.Result;
        }

        public IEnumerable<Room> GetByDate(DateTime datetime)
        {
            var task = Task.Factory.StartNew(() => {
                return _context.Rooms.Where(r => r.Created.Date <= datetime.Date && (r.Deleted.Value == null || r.Deleted.Value.Date > datetime.Date));
            });
            task.Wait();
            return task.Result;
        }
    }
}
