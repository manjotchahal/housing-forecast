using System;
using Xunit;
using Housing.Forecast.Library.Models;
using Housing.Forecast.Service;

namespace Housing.Forecast.Testing.Library
{
    public class UserTests
    {
        [Fact]
        public void DefaultUserInvalidTest()
        {
            User test = new User();
            Assert.False(test.Validate());
        }

        [Fact]
        public void UserValidTest()
        {
            User test = TestDataGenerator.getTestUser();
            Assert.True(test.Validate());
        }

        [Fact]
        public void UserIdInvalidTest()
        {
            User test = new User();
            test.Id = Guid.Empty;
            Assert.False(test.Validate());
        }
        [Fact]
        public void UserUserIdInvalidTest()
        {
            User test = new User();
            test.UserId = Guid.Empty;
            Assert.False(test.Validate());
        }

        [Fact]
        public void UserAddressInvalidTest()
        {
            User test = new User();
            test.Address = null;
            Assert.False(test.Validate());
        }

        [Fact]
        public void UserGenderNullInvalidTest()
        {
            User test = new User();
            test.Gender = null;
            Assert.False(test.Validate());
        }

        [Fact]
        public void UserGenderEmptyInvalidTest()
        {
            User test = new User();
            test.Gender = "";
            Assert.False(test.Validate());
        }

        [Fact]
        public void UserLocationNullInvalidTest()
        {
            User test = new User();
            test.Location = null;
            Assert.False(test.Validate());
        }

        [Fact]
        public void UserLocationEmptyInvalidTest()
        {
            User test = new User();
            test.Location = "";
            Assert.False(test.Validate());
        }

        [Fact]
        public void UserNameInvalidTest()
        {
            User test = new User();
            test.Name = null;
            Assert.False(test.Validate());
        }

        [Fact]
        public void UserBatchInvalidTest()
        {
            User test = new User();
            test.Batch = null;
            Assert.False(test.Validate());
        }

        [Fact]
        public void UserEmailNullInvalidTest()
        {
            User test = new User();
            test.Email = null;
            Assert.False(test.Validate());
        }

        [Fact]
        public void UserEmailEmptyInvalidTest()
        {
            User test = new User();
            test.Email = "";
            Assert.False(test.Validate());
        }

        [Fact]
        public void UserTypeNullInvalidTest()
        {
            User test = new User();
            test.Type = null;
            Assert.False(test.Validate());
        }

        [Fact]
        public void UserTypeEmptyInvalidTest()
        {
            User test = new User();
            test.Type = "";
            Assert.False(test.Validate());
        }

    }
}
