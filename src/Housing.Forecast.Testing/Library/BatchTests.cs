using System;
using Xunit;
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
                State = "VA"
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
        public void BatchStateInvalidTest()
        {
            Batch test = getTestBatch();
            test.State = null;
            Assert.False(test.Validate());
        }

    }
}
