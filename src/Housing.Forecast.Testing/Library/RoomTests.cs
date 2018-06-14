using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Housing.Forecast.Library.Models;

using Housing.Forecast.Service;

namespace Housing.Forecast.Testing.Library
{
    public class RoomTests
    {
        private Room getTestRoom()
        {
            Room result = new Room
            {
                Id = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                Location = "Reston",
                Vacancy = 1,
                Occupancy = 1,
                Gender = "F",
                Address = new Address
                {
                    Id = Guid.NewGuid(),
                    AddressId = Guid.NewGuid(),
                    Address1 = "1600 Pennsylvania Ave",
                    Address2 = "Apt. 110-B",
                    City = "Reston",
                    State = "VA",
                    PostalCode = "12345-1234",
                    Country = "USA",
                    Created = DateTime.Now
                },
                Created = DateTime.Now,
                Deleted = DateTime.MaxValue
            };
            return result;
        }

        [Fact]
        public void DefaultRoomInvalidTest()
        {
            Room test = new Room();
            Assert.False(test.Validate());         
        }

        [Fact]
        public void RoomValidTest()
        {
            Room test = getTestRoom();
            Assert.True(test.Validate());
        }

        [Fact]
        public void RoomIdInvalidTest()
        {
            Room test = getTestRoom();
            test.Id = Guid.Empty;
            Assert.False(test.Validate());
        }

        [Fact]
        public void RoomRoomIdInvalidTest()
        {
            Room test = getTestRoom();
            test.RoomId = Guid.Empty;
            Assert.False(test.Validate());
        }

        [Fact]
        public void RoomLocationNullInvalidTest()
        {
            Room test = getTestRoom();
            test.Location = null;
            Assert.False(test.Validate());
        }

        [Fact]
        public void RoomLocatioEmptyInvalidTest()
        {
            Room test = getTestRoom();
            test.Location = "";
            Assert.False(test.Validate());
        }

        [Fact]
        public void RoomGenderNullInvalidTest()
        {
            Room test = getTestRoom();
            test.Gender = null;
            Assert.False(test.Validate());
        }

        [Fact]
        public void RoomGenderEmptyInvalidTest()
        {
            Room test = getTestRoom();
            test.Gender = "";
            Assert.False(test.Validate());
        }

        [Fact]
        public void RoomOccupancyInvalidTest()
        {
            Room test = getTestRoom();
            test.Occupancy = -1;
            Assert.False(test.Validate());
        }

        [Fact]
        public void RoomVacancyInvalidTest()
        {
            Room test = getTestRoom();
            test.Vacancy = -1;
            Assert.False(test.Validate());
        }

        [Fact]
        public void RoomAddressInvalidTest()
        {
            Room test = getTestRoom();
            test.Address = null;
            Assert.False(test.Validate());
        }
    }
}
