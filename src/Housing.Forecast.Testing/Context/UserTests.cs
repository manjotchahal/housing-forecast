using Housing.Forecast.Context.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Housing.Forecast.Testing.Context
{
    public class UserTests
    {
        private User getTestUser()
        {
            User result = new User
            {
                Name = new Name
                {
                    NameId = Guid.NewGuid(),
                    First = "first",
                    Last = "last"
                },
                Batch = new Batch
                {
                    Id = Guid.NewGuid(),
                    BatchId = Guid.NewGuid(),
                    BatchName = "fakebatch",
                    BatchOccupancy = 20,
                    BatchSkill = "fakeskill",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.MaxValue,
                    State = "VA",
                    Created = DateTime.Now,
                    Deleted = DateTime.Now
                },
                Address = null,
                Id = Guid.NewGuid(),
                Location = "Reston",
                Email = "test@test.com",
                Gender = "M",
                Type = "test",
                UserId = Guid.NewGuid(),
                Created = DateTime.Now,
                Deleted = DateTime.Now
            };
            return result;
        }
        [Fact]
        public void DefaultUserInvalidTest()
        {
            User test = new User();
            Assert.False(test.Validate());
        }

        [Fact]
        public void UserValidTest()
        {
            User test = getTestUser();
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
