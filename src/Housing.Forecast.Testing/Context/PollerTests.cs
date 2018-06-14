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

namespace Housing.Forecast.Testing.Context
{
    public class PollerTests
    {
        private ForecastContext _context;
        private IRepo<User> _userRepository;
        private static readonly DbContextOptions<ForecastContext> options = new DbContextOptionsBuilder<ForecastContext>()
            .UseInMemoryDatabase(databaseName: "InMemDb")
            .Options;
        private readonly Mock<Poller> _testPoller;

        public PollerTests()
        {
            _context = new ForecastContext(options);
            _testPoller = new Mock<Poller>(_context);
            _testPoller.Setup(m => m.OnStart());
            _testPoller.Setup(m => m.OnStop());
            _testPoller.Setup(m => m.Poll());
            _testPoller.Setup(m => m.Update());

        }

        [Fact]
        public void OnStartTest()
        {
            _testPoller.Verify(m => m.OnStart());
        }

        [Fact]
        public void OnStopTest()
        {
            _testPoller.Verify(m => m.OnStop());
        }

        [Fact]
        public void UpdateTest()
        {
            _testPoller.Verify(m => m.Update());
        }

        [Fact]
        public void PollTest()
        {
            _testPoller.Verify(m => m.Poll());
        }
    }
}
