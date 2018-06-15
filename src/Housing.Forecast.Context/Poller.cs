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
        private readonly TimeSpan _interval;

        public Poller(ForecastContext context, TimeSpan interval)
        {
            _context = context;
            _interval = interval;
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

        public void UpdateAddress(Address check)
        {
            var mod = _context.Addresses.Find(check.AddressId);
            if (mod == null)
            {
                check.Id = Guid.NewGuid();
                check.Created = DateTime.Today;
                _context.Addresses.Attach(check);
            }
            else if (mod.Address1 != check.Address1 ||
                    mod.Address2 != check.Address2 ||
                    mod.City != check.City ||
                    mod.Country != check.Country ||
                    mod.PostalCode != check.PostalCode ||
                    mod.State != check.State)
            {
                check.Id = mod.Id;
                _context.Entry(mod).CurrentValues.SetValues(check);                
            }
        }

        public void UpdateName(Name check)
        {
            var mod = _context.Names.Where(p => p.NameId == check.NameId).FirstOrDefault();
            if (mod == null)
            {
                check.Id = Guid.NewGuid();
                check.Created = DateTime.Today;
                _context.Names.Add(check);
            }
            else if (mod.First != check.First ||
                    mod.Last != check.Last ||
                    mod.Middle != check.Middle)
            {
                check.Id = mod.Id;
                _context.Entry(mod).CurrentValues.SetValues(check);
            }
        }

        public void UpdateBatches(ICollection<Batch> Batch)
        {
            //insert proper endpoint when we get it
            
            var dbBatches = _context.Batches;

            var joinBatchDelete = from New in Batch
                                  join Old in dbBatches
                                  on New.BatchId equals Old.BatchId
                                  where Old.Deleted == null &&
                                  New.BatchId == null
                                  select Old;
            var joinBatchNew = from New in Batch
                               join Old in dbBatches
                               on New.BatchId equals Old.BatchId into temp
                               from Old in temp.DefaultIfEmpty()
                               where Old == null
                               select New;
            var joinBatchDiff = from New in Batch
                                join Old in dbBatches
                                on New.BatchId equals Old.BatchId
                                where New.State != Old.State ||
                                New.BatchName != Old.BatchName ||
                                New.BatchOccupancy != Old.BatchOccupancy ||
                                New.BatchSkill != Old.BatchSkill ||
                                New.EndDate != Old.EndDate ||
                                New.StartDate != Old.StartDate
                                select New;

            foreach (var x in _context.Batches)
            {
                if (joinBatchDelete.Contains(x))
                {
                    x.Deleted = DateTime.Today;
                    var modify = _context.Batches.Find(x.BatchId);
                    _context.Entry(x).CurrentValues.SetValues(modify);
                }
            }
            foreach (var x in joinBatchDiff)
            {
                var modify = _context.Batches.Find(x.BatchId);
                x.Id = modify.Id;
                _context.Entry(x).CurrentValues.SetValues(modify);

            }
            foreach (var x in joinBatchNew)
            {
                x.Created = DateTime.Today;
                x.Id = Guid.NewGuid();
                _context.Batches.Add(x);
            }
            _context.SaveChanges();
        }

        public void UpdateUsers(ICollection<User> Users)
        {
            //insert proper endpoint when we get it
            var dbUsers = _context.Users;

            var joinUserDelete = from New in Users
                                 join Old in dbUsers
                                 on New.UserId equals Old.UserId
                                 where Old.Deleted == null &&
                                 New.UserId == null
                                 select Old;
            var joinUserNew = from New in Users
                              join Old in dbUsers
                              on New.UserId equals Old.UserId into temp
                               from Old in temp.DefaultIfEmpty()
                              where Old == null
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
                    _context.Entry(x).CurrentValues.SetValues(modify);
                }
            }
            foreach (var x in joinUserDiff)
            {
                var modify = _context.Users.Find(x.UserId);
                x.Id = modify.Id;
                _context.Entry(x).CurrentValues.SetValues(modify);
            }
            foreach (var x in joinUserNew)
            {
                UpdateName(x.Name);
                UpdateAddress(x.Address);
                x.Created = DateTime.Today;
                x.Id = Guid.NewGuid();
                _context.Users.Add(x);
            }
            _context.SaveChanges();

        }
        public void UpdateRooms(ICollection<Room> Rooms)
        {
            //insert proper endpoint when we get it
            var dbRooms = _context.Rooms;

            var joinRoomDelete = from New in Rooms
                                 join Old in dbRooms
                                 on New.RoomId equals Old.RoomId
                                 where Old.Deleted == null &&
                                 New.RoomId == null
                                 select Old;
            var joinRoomNew = from New in Rooms
                              join Old in dbRooms
                              on New.RoomId equals Old.RoomId into temp
                              from Old in temp.DefaultIfEmpty()
                              where Old == null
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
                    _context.Entry(x).CurrentValues.SetValues(modify);
                }
            }
            foreach (var x in joinRoomDiff)
            {
                var modify = _context.Rooms.Find(x.RoomId);
                x.Id = modify.Id;
                _context.Entry(x).CurrentValues.SetValues(modify);
            }
            foreach (var x in joinRoomNew)
            {
                UpdateAddress(x.Address);
                x.Created = DateTime.Today;
                x.Id = Guid.NewGuid();
                _context.Rooms.Add(x);
            }
            _context.SaveChanges();
        }

        public void Update()
        {
            ApiMethods api = new ApiMethods();
            var Batch = api.HttpGetFromApi<Batch>("");
            var Users = api.HttpGetFromApi<User>("");
            var Rooms = api.HttpGetFromApi<Room>("");


            UpdateUsers(Users);
            UpdateRooms(Rooms);
            UpdateBatches(Batch);
        }

        public void Poll()
        {
            CancellationToken cancellation = cts.Token;
            TimeSpan interval = _interval;
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


