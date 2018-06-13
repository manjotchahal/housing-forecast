using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Housing.Forecast.Library.Models
{
    ///<summary>The Snapshot class is used to represent the supply and demand of Rooms and Users on any given date.</summary>
    public class Snapshot
    {
       
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int RoomCount { get; set; }
        public int UserCount { get; set; }
        public string Location { get; set; }
        public DateTime Created { get; set; }
        /// <summary>Default Constructor</summary>>
        /// <remarks>Sets all properties to empty, null, or impossible values that correspond 
        /// to invalid models that should be invalid if not replaced.</remarks>
        public Snapshot() {
            Id = Guid.Empty;
            Date = DateTime.MinValue;
            Location = null;
            RoomCount = -1;
            UserCount = -1;
        }
        /// <summary>Property validation</summary>>
        /// <remarks>Returns true if all properties are valid
        /// By default (through constructor), the model is invalid, properties to be filled in</remarks>
        public bool Validate() {
            bool result = Id != Guid.Empty &&
                Date > DateTime.MinValue &&
                !string.IsNullOrEmpty(Location) &&
                RoomCount >= 0 &&
                UserCount >= 0;
            return result;
        }
    }
}
