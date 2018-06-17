using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Housing.Forecast.Context;
using Housing.Forecast.Context.Models;

namespace Housing.Forecast.Context.Repos
{
    public class SnapshotRepo : ISnapshotRepo
    {
        private readonly IForecastContext _context;
        public SnapshotRepo(IForecastContext context)
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
            return _context.Snapshots.Select(s => s.Location).Where(s => s != null).Distinct();
        }

        /// <summary>
        /// Find all of the snapshots within the database.
        /// </summary>
        /// <returns>
        /// This method will return a list of the all snapshots that are stored within the database.
        /// </returns>
        public IEnumerable<Snapshot> Get()
        {
            return _context.Snapshots;
        }

        /// <summary>
        /// Find all snapshots that over the specified range of dates.
        /// </summary>
        /// <param name="Start">The starting point of the date range for the snapshots.</param>
        /// <param name="End">The ending pointing of the date range for the snapshots.</param>
        /// <returns>
        /// This method will return a list of snapshots that are within the specified range of dates.
        /// </returns>
        public IEnumerable<Snapshot> GetBetweenDates(DateTime Start, DateTime End)
        {
            return _context.Snapshots.Where(s => s.Date.Date >= Start.Date && s.Date.Date <= End.Date);
        }

        /// <summary>
        /// Find all of the snapshots for the specified location that were created on the provided date.
        /// </summary>
        /// <param name="datetime">The date the snapshot should have been created on.</param>
        /// <param name="location">The location the snapshot should be for.</param>
        /// <returns>
        /// This method should a list of all snapshots for the specified location that were craeted on the provided date that are stored in the database.
        /// </returns>
        public IEnumerable<Snapshot> GetByLocation(DateTime datetime, string location)
        {
            return _context.Snapshots.Where(s => s.Date.Date == datetime.Date && s.Location.Equals(location));
        }

        /// <summary>
        /// Find all of the snapshots for the specified location that fall within the provided date range.
        /// </summary>
        /// <param name="Start">The starting point of the date range for the snapshots.</param>
        /// <param name="End">The ending point of the date range for the snapshots.</param>
        /// <param name="location">The location the snapshots should be tied to.</param>
        /// <returns>
        /// This method should return a list of all snapshots that fall within the date range and tied to the location provided.
        /// </returns>
        public IEnumerable<Snapshot> GetBetweenDatesAtLocation(DateTime Start, DateTime End, string location)
        {
            return _context.Snapshots.Where(s => s.Date.Date >= Start.Date && s.Date.Date <= End.Date && s.Location.Equals(location));
        }

        /// <summary>
        /// Find all snapshots that were created on the specified date.
        /// </summary>
        /// <param name="datetime">The date the snapshot should be created on.</param>
        /// <returns>
        /// This method should return a list of all snapshots within the datebase that were created on the specified date.
        /// </returns>
        public IEnumerable<Snapshot> GetByDate(DateTime datetime)
        {
            return _context.Snapshots.Where(s => s.Date.Date == datetime.Date);
        }

        /// <summary>
        /// Dispose of the context object once it falls out of scope. This is be automatically called once the using statement ends.
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
        }
    }
}
