﻿using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Housing.Forecast.Library.Models;
using Housing.Forecast.Service;

namespace Housing.Forecast.Testing.Library
{
    public class BatchTests
    {
        private Batch getTestBatch()
        {
            Batch batch = new Batch
            {
                Id = Guid.NewGuid(),
                BatchId = Guid.NewGuid(),
                BatchName = "name",
                BatchOccupancy = 1,
                BatchSkill = ".Net",
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
                }
            };
            return batch;
        }

        [Fact]
        public void DefaultBatchInvalidTest()
        {
            Batch test = new Batch();
            Assert.False(test.Validate());
        }

        [Fact]
        public void BatchValidTest()
        {
            Batch test = getTestBatch();
            Assert.True(test.Validate());
        }

        [Fact]
        public void BatchIdInvalidTest()
        {
            Batch test = getTestBatch();
            test.Id = Guid.Empty;
            Assert.False(test.Validate());
        }

        [Fact]
        public void BatchBatchIdInvalidTest()
        {
            Batch test = getTestBatch();
            test.BatchId = Guid.Empty;
            Assert.False(test.Validate());
        }

        [Fact]
        public void BatchNameInvalidTest()
        {
            Batch test = getTestBatch();
            test.BatchName = "";
            Assert.False(test.Validate());
        }

        [Fact]
        public void BatchOccupancyLessInvalidTest()
        {
            Batch test = getTestBatch();
            test.BatchOccupancy = -1;
            Assert.False(test.Validate());
        }

        [Fact]
        public void BatchOccupancyOverInvalidTest()
        {
            Batch test = getTestBatch();
            test.BatchOccupancy = 101;
            Assert.False(test.Validate());
        }

        [Fact]
        public void BatchSkillInvalidTest()
        {
            Batch test = getTestBatch();
            test.BatchSkill = "";
            Assert.False(test.Validate());
        }

        [Fact]
        public void BatchAddressInvalidTest()
        {
            Batch test = getTestBatch();
            test.Address = null;
            Assert.False(test.Validate());
        }

    }
}
