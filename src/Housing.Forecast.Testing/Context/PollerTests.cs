using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Housing.Forecast.Context;
using Housing.Forecast.Context.Models;
using Xunit.Abstractions;

namespace Housing.Forecast.Testing.Context
{
    public class PollerTests
    {
        private ForecastContext _context;
        private static readonly DbContextOptions<ForecastContext> options = new DbContextOptionsBuilder<ForecastContext>()
            .UseInMemoryDatabase(databaseName: "InMemDb")
            .Options;
        private readonly ITestOutputHelper _output;

        public PollerTests(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public void UpdateNewName()
        {
            using (_context = new ForecastContext(options))
            {
                // Arrange
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                Name insertTest = TestDataGenerator.getTestName();

                // Act
                testPoller.UpdateName(insertTest);
                _context.SaveChanges();

                // Assert
                Name afterInsertTest = _context.Names.Where(p => p.NameId == insertTest.NameId).FirstOrDefault();
                Assert.Equal(insertTest, afterInsertTest);
            }
        }

        [Fact]
        public void UpdateModFirstName()
        {
            using (_context = new ForecastContext(options))
            {
                // Arrange
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                Name insertTest = TestDataGenerator.getTestName();
                _context.Names.Add(insertTest);
                _context.SaveChanges();

                // Act
                insertTest.First = "testChanged";
                testPoller.UpdateName(insertTest);
                _context.SaveChanges();

                // Assert
                Name afterInsertTest = _context.Names.Where(p => p.NameId == insertTest.NameId).FirstOrDefault();
                Assert.Equal(insertTest, afterInsertTest);
            }
        }

        [Fact]
        public void UpdateModMiddleName()
        {
            using (_context = new ForecastContext(options))
            {
                // Arrange
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                Name insertTest = TestDataGenerator.getTestName();
                _context.Names.Add(insertTest);
                _context.SaveChanges();

                // Act
                insertTest.Middle = "testChanged";
                testPoller.UpdateName(insertTest);
                _context.SaveChanges();

                // Assert
                Name afterInsertTest = _context.Names.Where(p => p.NameId == insertTest.NameId).FirstOrDefault();
                Assert.Equal(insertTest, afterInsertTest);
            }
        }

        [Fact]
        public void UpdateModLastName()
        {
            using (_context = new ForecastContext(options))
            {
                // Arrange
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                Name insertTest = TestDataGenerator.getTestName();
                _context.Names.Add(insertTest);
                _context.SaveChanges();

                // Act
                insertTest.Last = "testChanged";
                testPoller.UpdateName(insertTest);
                _context.SaveChanges();

                // Assert
                Name afterInsertTest = _context.Names.Where(p => p.NameId == insertTest.NameId).FirstOrDefault();
                Assert.Equal(insertTest, afterInsertTest);
            }
        }

        [Fact]
        public void UpdateNewUsers() {
            using (_context = new ForecastContext(options)) {
                // Arrange
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                ICollection<User> list = new List<User>();
                list.Add(TestDataGenerator.getTestUser());

                // Act
                testPoller.UpdateUsers(list);

                // Assert
                list = _context.Users.ToList();
                Assert.NotEmpty(list);
            }
        }

        [Fact]
        public void UpdateNameNoChange()
        {
            using (_context = new ForecastContext(options))
            {
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                Name insertTest = getNewName();
                testPoller.UpdateName(insertTest);
                _context.SaveChanges();
                testPoller.UpdateName(insertTest);
                _context.SaveChanges();
                Name afterInsertTest = _context.Names.Where(p => p.NameId == insertTest.NameId).FirstOrDefault();
                Assert.Equal(insertTest, afterInsertTest);
            }
        }

        [Fact]
        public void UpdateNewAddress()
        {
            using (_context = new ForecastContext(options))
            {
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                Address insertTest = getNewAddress();
                testPoller.UpdateAddress(insertTest);
                _context.SaveChanges();
                Address afterInsertTest = _context.Addresses.Where(p => p.AddressId == insertTest.AddressId).FirstOrDefault();
                Assert.Equal(insertTest, afterInsertTest);
            }
        }

        [Fact]
        public void UpdateModAddress1()
        {
            using (_context = new ForecastContext(options))
            {
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                Address insertTest = getNewAddress();
                testPoller.UpdateAddress(insertTest);
                _context.SaveChanges();
                insertTest.Address1 = "changed";
                testPoller.UpdateAddress(insertTest);
                _context.SaveChanges();
                Address afterInsertTest = _context.Addresses.Where(p => p.AddressId == insertTest.AddressId).FirstOrDefault();
                Assert.Equal(insertTest, afterInsertTest);
            }
        }

        [Fact]
        public void UpdateModAddress2()
        {
            using (_context = new ForecastContext(options))
            {
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                Address insertTest = getNewAddress();
                testPoller.UpdateAddress(insertTest);
                _context.SaveChanges();
                insertTest.Address2 = "changed";
                testPoller.UpdateAddress(insertTest);
                _context.SaveChanges();
                Address afterInsertTest = _context.Addresses.Where(p => p.AddressId == insertTest.AddressId).FirstOrDefault();
                Assert.Equal(insertTest, afterInsertTest);
            }
        }

        [Fact]
        public void UpdateModAddressCity()
        {
            using (_context = new ForecastContext(options))
            {
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                Address insertTest = getNewAddress();
                testPoller.UpdateAddress(insertTest);
                _context.SaveChanges();
                insertTest.City = "changed";
                testPoller.UpdateAddress(insertTest);
                _context.SaveChanges();
                Address afterInsertTest = _context.Addresses.Where(p => p.AddressId == insertTest.AddressId).FirstOrDefault();
                Assert.Equal(insertTest, afterInsertTest);
            }
        }

        [Fact]
        public void UpdateModAddressCountry()
        {
            using (_context = new ForecastContext(options))
            {
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                Address insertTest = getNewAddress();
                testPoller.UpdateAddress(insertTest);
                _context.SaveChanges();
                insertTest.Country = "changed";
                testPoller.UpdateAddress(insertTest);
                _context.SaveChanges();
                Address afterInsertTest = _context.Addresses.Where(p => p.AddressId == insertTest.AddressId).FirstOrDefault();
                Assert.Equal(insertTest, afterInsertTest);
            }
        }

        [Fact]
        public void UpdateModAddressPostalCode()
        {
            using (_context = new ForecastContext(options))
            {
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                Address insertTest = getNewAddress();
                testPoller.UpdateAddress(insertTest);
                _context.SaveChanges();
                insertTest.PostalCode = "changed";
                testPoller.UpdateAddress(insertTest);
                _context.SaveChanges();
                Address afterInsertTest = _context.Addresses.Where(p => p.AddressId == insertTest.AddressId).FirstOrDefault();
                Assert.Equal(insertTest, afterInsertTest);
            }
        }

        [Fact]
        public void UpdateModAddressState()
        {
            using (_context = new ForecastContext(options))
            {
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                Address insertTest = getNewAddress();
                testPoller.UpdateAddress(insertTest);
                _context.SaveChanges();
                insertTest.State = "changed";
                testPoller.UpdateAddress(insertTest);
                _context.SaveChanges();
                Address afterInsertTest = _context.Addresses.Where(p => p.AddressId == insertTest.AddressId).FirstOrDefault();
                Assert.Equal(insertTest, afterInsertTest);
            }
        }

        [Fact]
        public void UpdateNewBatch()
        {
            using (_context = new ForecastContext(options))
            {
                Poller testPoller = new Poller(_context, TimeSpan.MinValue);
                ICollection<Batch> Batches = new List<Batch>();
                Batch insertTest = getNewBatch();
                Batches.Add(insertTest);
                testPoller.UpdateBatches(Batches);
                Batch afterInsertTest = _context.Batches.Where(p => p.BatchId == insertTest.BatchId).FirstOrDefault();
                Assert.Equal(insertTest, afterInsertTest);
            }
        }
    }
}
