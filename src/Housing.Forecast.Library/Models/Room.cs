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
    [Table("Rooms")]
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        // Id we get from service hub
        [Required]
        public Guid RoomId { get; set; }

        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        [Required]
        public string Location { get; set; }

        [Required]
        public int Vacancy { get; set; }

        [Required]
        public int Occupancy { get; set; }

        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        [Required]
        public string Gender { get; set; }

        [Required]
        public Address Address { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Deleted { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
