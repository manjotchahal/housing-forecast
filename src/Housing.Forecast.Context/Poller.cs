using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Housing.Forecast.Context.ApiAccessors;
using Housing.Forecast.Context.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using NLog;

namespace Housing.Forecast.Context
{
    public class Poller : IPoller
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ForecastContext _context;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private Task mainTask = null;

        public Poller(ForecastContext context)
        {
            _context = context;
        }

        public void OnStart()
        {
            mainTask = new Task(Poll, cts.Token, TaskCreationOptions.LongRunning);
            mainTask.Start();
        }

        public void OnStop()
        {
            cts.Cancel();
            mainTask.Wait();
        }

        public void UpdateBatches(ApiMethods api)
        {
            var Batch = api.HttpGetFromApi<Batch>("");
            
            var dbBatches = _context.Batches;

            var joinBatchDelete = from New in Batch
                                  join Old in dbBatches
               on New.BatchId equals Old.BatchId
                                  where Old.Deleted == null &&
                                  New.BatchId == null
                                  select Old;
            var joinBatchNew = from New in Batch
                               join Old in dbBatches
            on New.BatchId equals Old.BatchId
                               where Old.BatchId == null
                               select New;
            var joinBatchDiff = from New in Batch
                                join Old in dbBatches
             on New.BatchId equals Old.BatchId
                                where New.Address.AddressId != Old.Address.AddressId ||
                                New.BatchName != Old.BatchName ||
                                New.BatchOccupancy != Old.BatchOccupancy ||
                                New.BatchSkill != Old.BatchSkill ||
                                New.EndDate != Old.EndDate ||
                                New.StartDate != Old.StartDate
                                select New;

            foreach (var x in _context.Users)
            {
                if (joinUserDelete.Contains(x))
                {
                    x.Deleted = DateTime.Today;
                    var modify = _context.Users.Find(x.UserId);
                    _context.Entry(modify).CurrentValues.SetValues(x);
                }
                if (joinUserDiff.Contains(x))
                {
                    var modify = _context.Users.Find(x.UserId);
                    _context.Entry(modify).CurrentValues.SetValues(x);
                }
            }
            foreach (var x in joinUserNew)
            {
                x.Created = DateTime.Today;
                _context.Users.Add(x);
            }
            _context.SaveChanges();
        }

        public void UpdateUsers(ApiMethods api)
        {
            var Users = api.HttpGetFromApi<User>("");
            var dbUsers = _context.Users;
            var joinUserDelete = from New in Users
                                 join Old in dbUsers
               on New.UserId equals Old.UserId
                                 where Old.Deleted == null &&
                                 New.UserId == null
                                 select Old;
            var joinUserNew = from New in Users
                              join Old in dbUsers
            on New.UserId equals Old.UserId
                              where Old.UserId == null
                              select New;
            var joinUserDiff = from New in Users
                               join Old in dbUsers
             on New.UserId equals Old.UserId
                               where New.Address.AddressId != Old.Address.AddressId ||
                               New.Batch.BatchId != Old.Batch.BatchId ||
                               New.Email != Old.Email ||
                               New.Gender != Old.Gender ||
                               New.Location != Old.Location ||
                               New.Name.NameId != Old.Name.NameId ||
                               New.Room.RoomId != Old.Room.RoomId ||
                               New.Type != Old.Type
                               select New;

            foreach (var x in _context.Users)
            {
                if (joinUserDelete.Contains(x))
                {
                    x.Deleted = DateTime.Today;
                    var modify = _context.Users.Find(x.UserId);
                    _context.Entry(modify).CurrentValues.SetValues(x);
                }
                if (joinUserDiff.Contains(x))
                {
                    var modify = _context.Users.Find(x.UserId);
                    _context.Entry(modify).CurrentValues.SetValues(x);
                }
            }
            foreach (var x in joinUserNew)
            {
                x.Created = DateTime.Today;
                _context.Users.Add(x);
            }
            _context.SaveChanges();

        }
        public void UpdateRooms(ApiMethods api)
        {
            var Rooms = api.HttpGetFromApi<Room>("");
            
            var dbRooms = _context.Rooms;
            
            var joinRoomDelete = from New in Rooms
                                 join Old in dbRooms
               on New.RoomId equals Old.RoomId
                                 where Old.Deleted == null &&
                                 New.RoomId == null
                                 select Old;
            var joinRoomNew = from New in Rooms
                              join Old in dbRooms
            on New.RoomId equals Old.RoomId
                              where Old.RoomId == null
                              select New;
            var joinRoomDiff = from New in Rooms
                               join Old in dbRooms
             on New.RoomId equals Old.RoomId
                               where New.Address.AddressId != Old.Address.AddressId ||
                               New.Location != Old.Location ||
                               New.Occupancy != Old.Occupancy ||
                               New.Gender != Old.Gender ||
                               New.Vacancy != Old.Vacancy
                               select New;

            foreach (var x in _context.Rooms)
            {
                if (joinRoomDelete.Contains(x))
                {
                    x.Deleted = DateTime.Today;
                    var modify = _context.Rooms.Find(x.RoomId);
                    _context.Entry(modify).CurrentValues.SetValues(x);
                }
                if (joinRoomDiff.Contains(x))
                {
                    var modify = _context.Rooms.Find(x.RoomId);
                    _context.Entry(modify).CurrentValues.SetValues(x);
                }
            }
            foreach (var x in joinRoomNew)
            {
                x.Created = DateTime.Today;
                _context.Rooms.Add(x);
            }
            _context.SaveChanges();
        }

        public void Update()
        {
            ApiMethods api = new ApiMethods();

            //none of these point to anything because we have don't have our endpoint URIs
            var Users = api.HttpGetFromApi<User>("");
            var Rooms = api.HttpGetFromApi<Room>("");
            var Batch = api.HttpGetFromApi<Batch>("");

            var dbUsers = _context.Users;
            var dbRooms = _context.Rooms;
            var dbBatches = _context.Batches;

            

            foreach (var x in _context.Users)
            {
                if (joinUserDelete.Contains(x))
                {
                    x.Deleted = DateTime.Today;
                    var modify = _context.Users.Find(x.UserId);
                    _context.Entry(modify).CurrentValues.SetValues(x);
                }
                if (joinUserDiff.Contains(x))
                {
                    var modify = _context.Users.Find(x.UserId);
                    _context.Entry(modify).CurrentValues.SetValues(x);
                }
            }
            foreach (var x in joinUserNew)
            {
                x.Created = DateTime.Today;
                _context.Users.Add(x);
            }
            _context.SaveChanges();
        }

        public void Poll()
        {
            CancellationToken cancellation = cts.Token;
            TimeSpan interval = TimeSpan.FromDays(1);
            while (!cancellation.WaitHandle.WaitOne(interval))
            {
                try
                {
                    Update();
                    if (cancellation.IsCancellationRequested)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    //string fromTimeString = result.ToString(“hh’:’mm”);
                    //interval = Convert.ToDouble(ConfigurationSettings.AppSettings[“WaitAfterSuccessInterval”]);
                    //interval = TimeSpan.FromMinutes((ConfigurationSettings.AppSettings[“WaitAfterSuccessInterval”]));
                    //interval = TimeSpan.FromMinutes((Convert.ToDouble(ConfigurationSettings.AppSettings[“WaitAfterSuccessInterval”])));
                    //TimeSpan result = TimeSpan.FromMinutes(interval);
                }
            }
        }

    }
}


