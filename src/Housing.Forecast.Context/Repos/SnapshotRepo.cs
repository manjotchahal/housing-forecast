using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Housing.Forecast.Context;
using Housing.Forecast.Context.Models;
using l = Housing.Forecast.Library.Models;

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
            var result = AsyncGetLocations();
            return result.Result;
        }
        private async Task<IEnumerable<string>> AsyncGetLocations() {
            return await Task.Factory.StartNew(() => {
                return _context.Snapshots.Select(s => s.Location).Where(s => s != null).Distinct().ToList();
            });
        }

        /// <summary>
        /// Find all of the snapshots within the database.
        /// </summary>
        /// <returns>
        /// This method will return a list of the all snapshots that are stored within the database.
        /// </returns>
        public IEnumerable<Snapshot> Get()
        {
            var result = AsyncDBCall(x => x != null);
            return result.Result;
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
            var result = AsyncDBCall(s => s.Date.Date >= Start.Date && s.Date.Date <= End.Date);
            return result.Result;
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
            var result = AsyncDBCall(s => s.Date.Date == datetime.Date && s.Location.Equals(location));
            return result.Result;
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
            var result = AsyncDBCall(s => s.Date.Date >= Start.Date && s.Date.Date <= End.Date && s.Location.Equals(location));
            return result.Result;
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
            var result = AsyncDBCall(s => s.Date.Date == datetime.Date);
            return result.Result;
        }

        /// <summary>
        /// Convert a list of Context's Snapshot objects to Library's Snapshot objects
        /// </summary>
        /// <param name="snapshots">List of Snapshots that need to be mapped.</param>
        /// <returns>
        /// Returns a new of the mapped Snapshots.
        /// </returns>
        private IEnumerable<l.Snapshot> Map(IEnumerable<Snapshot> snapshots)
        {
            var snaps = new List<l.Snapshot>();
            foreach (var snap in snapshots)
            {
                snaps.Add(Map(snap));
            }

            return snaps;
        }

        /// <summary>
        /// Map the Context's Snapshot model to Library's Snapshot model
        /// </summary>
        /// <param name="snapshot">Snapshot that needs to be mapped.</param>
        /// <returns>
        /// Return the mapped Snapshot model.
        /// </returns>
        private l.Snapshot Map(Snapshot snapshot)
        {
            var snap = new l.Snapshot()
            {
                Id = Guid.Empty,
                Date = snapshot.Date,
                Created = snapshot.Created,
                Location = snapshot.Location,
                RoomOccupancyCount = snapshot.RoomOccupancyCount,
                UserCount = snapshot.UserCount
            };

            return snap;
        }

        /// <summary>
        /// Perform an asynchronous call to the database to retrieve data.
        /// </summary>
        /// <param name="lambda">The search condition in the form of a lambda expression.</param>
        /// <returns>
        /// A Task for the search result set.
        /// </returns>
        private async Task<IEnumerable<Snapshot>> AsyncDBCall(Func<Snapshot, bool> lamdba) {
            var task = await Task.Factory.StartNew(() => {
                return _context.Snapshots.Where(lamdba).ToList();
            });
            return task;
        }

        /// <summary>
        /// Add the newly created snapshots to the database
        /// </summary>
        /// <param name="snapshots">Snapshots that need to be added to the database.</param>
        /// <returns>
        /// Returns true if the snapshots are added succuessfully else return false.
        /// </returns>
        public bool AddSnapshots(IEnumerable<Snapshot> snapshots)
        {
            try
            {
                foreach (var snap in snapshots)
                {
                    _context.Snapshots.Add(snap);
                }
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
