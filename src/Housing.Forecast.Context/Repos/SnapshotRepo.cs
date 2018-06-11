using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Housing.Forecast.Library;
using Housing.Forecast.Library.Models;

namespace Housing.Forecast.Context.Repos
{
    public class SnapshotRepo : IRepo<Snapshot>, IDisposable
    {
        private readonly IForecastContext _context;
        public SnapshotRepo(IForecastContext context)
        {
            _context = context;
        }

        public IEnumerable<string> GetLocations()
        {
            return _context.Snapshots.Select(s => s.Location).Where(s => s != null).Distinct();
        }

        public IEnumerable<Snapshot> Get()
        {
            return _context.Snapshots;
        }

        public IEnumerable<Snapshot> GetBetweenDates(DateTime Start, DateTime End)
        {
            return _context.Snapshots.Where(s => s.Date <= Start && s.Date >= End);
        }

        public IEnumerable<Snapshot> GetByLocation(DateTime datetime, string location)
        {
            return _context.Snapshots.Where(s => s.Date == datetime && s.Location.Equals(location));
        }

        public IEnumerable<Snapshot> GetBetweenDatesAtLocation(DateTime Start, DateTime End, string location)
        {
            return _context.Snapshots.Where(s => s.Date <= Start && s.Date >= End && s.Location.Equals(location));
        }

        public IEnumerable<Snapshot> GetByDate(DateTime datetime)
        {
            return _context.Snapshots.Where(s => s.Date == datetime);
        }

        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
        }
    }
}
