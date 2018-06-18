using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Housing.Forecast.Context;
using Housing.Forecast.Context.Models;

namespace Housing.Forecast.Context.Repos
{
    public class UserRepo : IRepo<User>
    {
        private readonly IForecastContext _context;
        public UserRepo(IForecastContext context)
        {
            _context = context;
        }

        public IEnumerable<string> GetLocations()
        {
            var task = Task.Factory.StartNew(() => {
                return _context.Users.Select(r => r.Location).Where(r => r != null).Distinct();
            });
            task.Wait();
            return task.Result;
        }

        public IEnumerable<User> Get()
        {
            var task = Task.Factory.StartNew(() => {
                return _context.Users;
            });
            task.Wait();
            return task.Result;
        }

        public IEnumerable<User> GetByLocation(DateTime datetime, string location)
        {
            var task = Task.Factory.StartNew(() => {
                return _context.Users.Where(r => r.Created.Date <= datetime.Date && (r.Deleted.Value == null || r.Deleted.Value.Date > datetime.Date) && r.Location == location);
            });
            task.Wait();
            return task.Result;
        }

        public IEnumerable<User> GetByDate(DateTime datetime)
        {
            var task = Task.Factory.StartNew(() => {
                return _context.Users.Where(u => u.Created.Date <= datetime.Date && (u.Deleted.Value == null || u.Deleted.Value.Date > datetime.Date));
            });
            task.Wait();
            return task.Result;
        }
    }
}
