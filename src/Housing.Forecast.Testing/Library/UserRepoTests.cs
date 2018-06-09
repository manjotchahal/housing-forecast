using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Housing.Forecast.Library;
using Housing.Forecast.Library.Models;
using Housing.Forecast.Library.Repos;
using Housing.Forecast.Service;

namespace Housing.Forecast.Testing.Library
{
    public class UserRepoTests
    {
        private ForecastContext _context;
        private IRepo<User> _userRepository;
        private static DbContextOptions<ForecastContext> options;

        [Fact]
        public void Get_ReturnCollection()
        {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<User> users;
                _userRepository = new UserRepo(_context);
                _context.Users.Add(getTestUser());
                _context.SaveChanges();

                // Act
                users = _userRepository.Get();

                // Assert
                Assert.NotEmpty(users);
            }
        }

        [Fact]
        public void GetByDate_WithValidDate_ReturnNonEmptyCollection()
        {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<User> users;
                _userRepository = new UserRepo(_context);
                _context.Users.Add(getTestUser());
                _context.SaveChanges();
                User user = _context.Users.FirstOrDefault();

                // Act
                users = _userRepository.GetByDate(user.Created);

                // Assert
                Assert.Equal(users.FirstOrDefault().Id, user.Id);
            }
        }

        [Fact]
        public void GetByDate_WithInvalidDate_ReturnEmptyCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<User> users;
                _userRepository = new UserRepo(_context);
                _context.Users.Add(getTestUser());
                _context.SaveChanges();
                User user = _context.Users.FirstOrDefault();

                // Act
                users = _userRepository.GetByDate(DateTime.MinValue);

                // Assert
                Assert.Empty(users);
            }
        }

        [Fact]
        public void GetLocations_ReturnCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<String> locations;
                _userRepository = new UserRepo(_context);
                _context.Users.Add(getTestUser());

                User user = getTestUser();
                user.Location = "Tampa";
                _context.Users.Add(user);

                _context.SaveChanges();

                // Act
                locations = _userRepository.GetLocations();

                // Assert
                Assert.Equal(locations.Count(), 2);
            }
        }

        [Fact]
        public void GetBetweenDates_ValidDateRange_ReturnNonEmptyCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<User> users;
                _userRepository = new UserRepo(_context);
                _context.Users.Add(getTestUser());
                _context.SaveChanges();

                // Act
                users = _userRepository.GetBetweenDates(DateTime.Now, DateTime.Now);

                // Assert
                Assert.NotEmpty(users);
            }
        }

        [Fact]
        public void GetBetweenDates_InvalidDateRange_ReturnEmptyCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<User> users;
                _userRepository = new UserRepo(_context);
                _context.Users.Add(getTestUser());
                _context.SaveChanges();

                // Act
                users = _userRepository.GetBetweenDates(DateTime.MinValue, DateTime.MinValue);

                // Assert
                Assert.Empty(users);
            }
        }

        [Fact]
        public void GetBetweenDatesAtLocation_ValidDateRangeValidLocation_ReturnNonEmptyCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<User> users;
                _userRepository = new UserRepo(_context);
                _context.Users.Add(getTestUser());
                _context.SaveChanges();

                // Act
                users = _userRepository.GetBetweenDatesAtLocation(DateTime.Now, DateTime.Now, "Reston");

                // Assert
                Assert.NotEmpty(users);
            }
        }

        [Fact]
        public void GetBetweenDatesAtLocation_InvalidDateRangeValidLocation_ReturnEmptyCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<User> users;
                _userRepository = new UserRepo(_context);
                _context.Users.Add(getTestUser());
                _context.SaveChanges();

                // Act
                users = _userRepository.GetBetweenDatesAtLocation(DateTime.MinValue, DateTime.MinValue, "Reston");

                // Assert
                Assert.Empty(users);
            }
        }

        [Fact]
        public void GetBetweenDatesAtLocation_ValidDateRangeInvalidLocation_ReturnEmptyCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<User> users;
                _userRepository = new UserRepo(_context);
                _context.Users.Add(getTestUser());
                _context.SaveChanges();

                // Act
                users = _userRepository.GetBetweenDatesAtLocation(DateTime.Now, DateTime.Now, "test");

                // Assert
                Assert.Empty(users);
            }
        }

        [Fact]
        public void GetBetweenDatesAtLocation_InvalidDateRangeInvalidLocation_ReturnEmptyCollection() {
            init();
            using(_context = new ForecastContext(options)) {
                // Arrange
                IEnumerable<User> users;
                _userRepository = new UserRepo(_context);
                _context.Users.Add(getTestUser());
                _context.SaveChanges();

                // Act
                users = _userRepository.GetBetweenDatesAtLocation(DateTime.MinValue, DateTime.MinValue, "test");

                // Assert
                Assert.Empty(users);
            }
        }

        private void init() {
            options = new DbContextOptionsBuilder<ForecastContext>()
                .UseInMemoryDatabase(databaseName: "InMemDb")
                .Options;
        }

        private User getTestUser() {
            User result = new User{
                Name = new Name {
                    NameId = Guid.NewGuid(),
                    First = "first",
                    Last = "last"
                },
                Id = Guid.NewGuid(),
                Location = "Reston",
                Email = "test@test.com",
                Gender = "M",
                Type = "test",
                UserId = Guid.NewGuid(),
                Created = DateTime.Now,
                Deleted = DateTime.MaxValue
            };
            return result;
        }
    }
}
