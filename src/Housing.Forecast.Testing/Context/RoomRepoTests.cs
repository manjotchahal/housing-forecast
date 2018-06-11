using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Housing.Forecast.Context;
using Housing.Forecast.Context.Repos;
using Housing.Forecast.Library.Models;
using Housing.Forecast.Service;

namespace Housing.Forecast.Testing.Library
{
    public class RoomRepoTests
    {
        private ForecastContext _context;
        private IRepo<Room> _roomRepository;
        private static DbContextOptions<ForecastContext> options;

        [Fact]
        public void Get_ReturnCollection()
        {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<Room> rooms;
                _roomRepository = new RoomRepo(_context);
                _context.Rooms.Add(getTestRoom());
                _context.SaveChanges();

                // Act
                rooms = _roomRepository.Get();

                // Assert
                Assert.NotEmpty(rooms);
            }
        }

        [Fact]
        public void GetByDate_WithValidDate_ReturnNonEmptyCollection()
        {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<Room> rooms;
                _roomRepository = new RoomRepo(_context);
                _context.Rooms.Add(getTestRoom());
                _context.SaveChanges();
                Room room = _context.Rooms.FirstOrDefault();

                // Act
                rooms = _roomRepository.GetByDate(room.Created);

                // Assert
                Assert.Equal(rooms.FirstOrDefault().Id, room.Id);
            }
        }

        [Fact]
        public void GetByDate_WithInvalidDate_ReturnEmptyCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<Room> rooms;
                _roomRepository = new RoomRepo(_context);
                _context.Rooms.Add(getTestRoom());
                _context.SaveChanges();
                Room room = _context.Rooms.FirstOrDefault();

                // Act
                rooms = _roomRepository.GetByDate(DateTime.MinValue);

                // Assert
                Assert.Empty(rooms);
            }
        }

        [Fact]
        public void GetLocations_ReturnCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<String> locations;
                _roomRepository = new RoomRepo(_context);
                _context.Rooms.Add(getTestRoom());

                Room room = getTestRoom();
                room.Location = "Tampa";
                _context.Rooms.Add(room);

                _context.SaveChanges();

                // Act
                locations = _roomRepository.GetLocations();

                // Assert
                Assert.Equal(2, locations.Count());
            }
        }

        [Fact]
        public void GetByLocations_ValidDateValidLocation_ReturnNonEmptyCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<Room> rooms;
                _roomRepository = new RoomRepo(_context);
                _context.Rooms.Add(getTestRoom());
                _context.SaveChanges();
                Room room = _context.Rooms.FirstOrDefault();

                // Act
                rooms = _roomRepository.GetByLocation(room.Created, room.Location);

                // Assert
                Assert.NotEmpty(rooms);
            }
        }

        [Fact]
        public void GetByLocations_InvalidDateValidLocation_ReturnEmptyCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<Room> rooms;
                _roomRepository = new RoomRepo(_context);
                _context.Rooms.Add(getTestRoom());
                _context.SaveChanges();
                Room room = _context.Rooms.FirstOrDefault();

                // Act
                rooms = _roomRepository.GetByLocation(DateTime.MinValue, room.Location);

                // Assert
                Assert.Empty(rooms);
            }
        }

        [Fact]
        public void GetByLocations_ValidDateInvalidLocation_ReturnEmptyCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<Room> rooms;
                _roomRepository = new RoomRepo(_context);
                _context.Rooms.Add(getTestRoom());
                _context.SaveChanges();
                Room room = _context.Rooms.FirstOrDefault();

                // Act
                rooms = _roomRepository.GetByLocation(room.Created, "Tampa");

                // Assert
                Assert.Empty(rooms);
            }
        }

        [Fact]
        public void GetByLocations_InvalidDateInvalidLocation_ReturnEmptyCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<Room> rooms;
                _roomRepository = new RoomRepo(_context);
                _context.Rooms.Add(getTestRoom());
                _context.SaveChanges();
                Room room = _context.Rooms.FirstOrDefault();

                // Act
                rooms = _roomRepository.GetByLocation(DateTime.MinValue, "Tampa");

                // Assert
                Assert.Empty(rooms);
            }
        }

        private void init() {
            options = new DbContextOptionsBuilder<ForecastContext>()
                .UseInMemoryDatabase(databaseName: "InMemDb")
                .Options;
        }

        private Room getTestRoom() {
            Room result = new Room{
                Id = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                Location = "Reston",
                Vacancy = 1,
                Occupancy = 1,
                Gender = "F",
                Address = new Address{
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
                Deleted = DateTime.MaxValue
            };
            return result;
        }
    }
}
