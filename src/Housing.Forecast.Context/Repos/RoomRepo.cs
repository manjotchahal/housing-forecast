using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Housing.Forecast.Context;
using Housing.Forecast.Context.Models;

namespace Housing.Forecast.Context.Repos
{
    public class RoomRepo : IRepo<Room>, IDisposable
    {
        private readonly IForecastContext _context;
        public RoomRepo(IForecastContext context)
        {
            _context = context;
        }

        public IEnumerable<string> GetLocations()
        {
            return _context.Rooms.Select(r => r.Location).Where(r => r != null).Distinct();
        }

        public IEnumerable<Room> Get()
        {
            return _context.Rooms;
        }

        public IEnumerable<Room> GetByLocation(DateTime datetime, string location)
        {
            return _context.Rooms.Where(r => r.Created.Date <= datetime.Date && (r.Deleted == null || r.Deleted.Date > datetime.Date) && r.Location == location);
        }

        public IEnumerable<Room> GetByDate(DateTime datetime)
        {
            return _context.Rooms.Where(r => r.Created.Date <= datetime.Date && (r.Deleted == null || r.Deleted.Date > datetime.Date));
        }

        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
        }
    }
}
