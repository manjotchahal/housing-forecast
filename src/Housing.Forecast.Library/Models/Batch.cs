using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Housing.Forecast.Library.Models
{
    ///<summary>The Batch model is used to contain all of the pertinent information about a batch including start date, end date, location, type, occupancy, and location. </summary>
    ///<remarks>Each Batch contains a collection of User objects that represents the associates that belong to a batch.
    ///Each Batch will have a uniquely generated Guid Id as well as retain the primary key Guid of the previous database, which is stored in BatchId.</remarks>
    public class Batch
    {
        /// <summary>Default Constructor</summary>>
        /// <remarks>Sets all properties to empty, null, or impossible values that correspond 
        /// to invalid models that should be invalid if not replaced.</remarks>
        public Batch()
        {
            Id = Guid.Empty;
            BatchId = Guid.Empty;
            BatchName = "";
            BatchOccupancy = -1;
            BatchSkill = "";
            State = "";
        }

        /// <summary>Property validation</summary>>
        /// <remarks>Returns true if all properties are valid
        /// By default (through constructor), the model is invalid, properties to be filled in</remarks>
        public bool Validate()
        {
            if (Id == Guid.Empty) { return false; }
            if (BatchId == Guid.Empty) { return false; }
            if (String.IsNullOrEmpty(BatchName)) { return false; }
            if (BatchOccupancy < 0 ||  BatchOccupancy > 100) { return false; }
            if (String.IsNullOrEmpty(BatchSkill)) { return false; }
            if (String.IsNullOrEmpty(State)) { return false; }

            return true;
        }

        public Guid Id { get; set; }
        public Guid BatchId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BatchName { get; set; }
        public int BatchOccupancy { get; set; }
        public string BatchSkill { get; set; }
        public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
