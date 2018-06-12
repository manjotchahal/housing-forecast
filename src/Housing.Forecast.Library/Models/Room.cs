using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Housing.Forecast.Library.Models
{
    ///<summary>The Room model is used to contain all of the pertinent information about a room including location, vacancy, occupancy, room gender, and address. </summary>
    ///<remarks>
    ///Each Room object will have its own uniquely generated Guid Id and retain the primary key Guid that was generated for it in the previous database into RoomId.
    ///Each Room object will have a collection of User objects that share the same Room.
    ///</remarks>
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public string Location { get; set; }

        public int Vacancy { get; set; }

        public int Occupancy { get; set; }

        public string Gender { get; set; }

        public Address Address { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Deleted { get; set; }

        public ICollection<User> Users { get; set; }

        /// <summary>Default Constructor</summary>>
        /// <remarks>Sets all properties to empty, null, or impossible values that correspond 
        /// to invalid models that should be invalid if not replaced.</remarks>
        public Room()
        {
            Id = Guid.Empty;
            RoomId = Guid.Empty;
            Location = null;
            Vacancy = -1;
            Occupancy = -1;
            Gender = null;
            Address = null;
        }

        /// <summary>Property validation</summary>>
        /// <remarks>Returns true if all properties are valid
        /// By default (through constructor), the model is invalid, properties to be filled in</remarks>
        public bool Validate()
        {
            if (Id == Guid.Empty) { return false; }
            if (RoomId == Guid.Empty) { return false; }
            if (String.IsNullOrEmpty(Location)) { return false; }
            if (Vacancy == -1) { return false; }
            if (Occupancy == -1) { return false; }
            if (String.IsNullOrEmpty(Gender)) { return false; }
            if (Address == null) { return false; }

            return true;
        }
    }
}
