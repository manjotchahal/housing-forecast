using System;
using System.Collections.Generic;
using System.Text;
using Housing.Forecast.Context.Models;
using Housing.Forecast.Library.Models;


namespace Housing.Forecast.Context
{
    public static class ModelMapper
    {
        public static Models.Address MapAddress(Library.Models.Address libAddress)
        {
            Models.Address result = new Models.Address
            {
                Address1 = libAddress.Address1,
                Address2 = libAddress.Address2,
                AddressId = libAddress.AddressId,
                Batches = MapBatchesCollection(libAddress.Batches),
                City = libAddress.City,
                Country = libAddress.Country,
                PostalCode = libAddress.PostalCode,
                Rooms = MapRoomsCollection(libAddress.Rooms),
                State = libAddress.State,
                Users = MapUsersCollection(libAddress.Users)
            };

            return result;
        }

        public static Models.User MapUser(Library.Models.User libUser)
        {
            Models.User result = new Models.User
            {
                UserId = libUser.UserId,
                Address = MapAddress(libUser.Address),
                Batch = MapBatch(libUser.Batch),
                Gender = libUser.Gender,
                Location = libUser.Location,
                Email = libUser.Email,
                Name = MapName(libUser.Name),
                Room = MapRoom(libUser.Room),
                Type = libUser.Type
            };

            return result;
        }

        public static Models.Room MapRoom(Library.Models.Room libRoom)
        {
            Models.Room result = new Models.Room
            {
                Address = MapAddress(libRoom.Address),
                Gender = libRoom.Gender,
                Location = libRoom.Location,
                Occupancy = libRoom.Occupancy,
                RoomId = libRoom.RoomId,
                Users = MapUsersCollection(libRoom.Users),
                Vacancy = libRoom.Vacancy
            };

            return result;
        }

        public static Models.Name MapName(Library.Models.Name libName)
        {
            Models.Name result = new Models.Name
            {
                First = libName.First,
                Middle = libName.Middle,
                Last = libName.Last,
                NameId = libName.NameId,
                Users = MapUsersCollection(libName.Users)
            };

            return result;
        }

        public static Models.Batch MapBatch(Library.Models.Batch libBatch)
        {
            Models.Batch result = new Models.Batch
            {
                Address = MapAddress(libBatch.Address),
                BatchId = libBatch.BatchId,
                BatchName = libBatch.BatchName,
                BatchOccupancy = libBatch.BatchOccupancy,
                BatchSkill = libBatch.BatchSkill,
                EndDate = libBatch.EndDate,
                StartDate = libBatch.StartDate,
                Users = MapUsersCollection(libBatch.Users)
            };

            return result;
        }

        public static Models.Snapshot MapSnapshot(Library.Models.Snapshot libSnap)
        {
            Models.Snapshot result = new Models.Snapshot
            {
                Date = libSnap.Date,
                Location = libSnap.Location,
                RoomCount = libSnap.RoomCount,
                UserCount = libSnap.UserCount
            };

            return result;
        }

        private static ICollection<Models.User> MapUsersCollection(ICollection<Library.Models.User> users)
        {
            List<Models.User> result = new List<Models.User>();
            foreach(var user in users)
            {
                result.Add(MapUser(user));
            }
            return result;
        }

        private static ICollection<Models.Room> MapRoomsCollection(ICollection<Library.Models.Room> rooms)
        {
            List<Models.Room> result = new List<Models.Room>();
            foreach (var room in rooms)
            {
                result.Add(MapRoom(room));
            }
            return result;
        }

        private static ICollection<Models.Batch> MapBatchesCollection(ICollection<Library.Models.Batch> batches)
        {
            List<Models.Batch> result = new List<Models.Batch>();
            foreach (var batch in batches)
            {
                result.Add(MapBatch(batch));
            }
            return result;
        }
    }
}
