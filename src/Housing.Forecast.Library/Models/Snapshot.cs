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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Date { get; set; }

        public int RoomCount { get; set; }

        public int UserCount { get; set; }

        public string Location { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        public Snapshot() {
            Id = Guid.Empty;
            Date = DateTime.MinValue;
            Location = null;
            RoomCount = -1;
            UserCount = -1;
        }

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
