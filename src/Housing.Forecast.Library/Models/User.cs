using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Housing.Forecast.Library.Models
{
    ///<summary>The User model is used to contain all of the pertinent information about a user including name, location, room, address, email, gender, employee type, and batch they belong to. </summary>
    ///<remarks>    
    ///Each User object will have its own uniquely generated Guid Id and retain the primary key Guid that was generated for it in the previous database into UserId.
    ///</remarks>
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Name Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Location { get; set; }

        public Room Room { get; set; }
        public Address Address { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Type { get; set; }

        [Required]
        public Batch Batch { get; set; }

        [DataType(DataType.DateTime)]
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        [Column(TypeName = "datetime2")]
        public DateTime Deleted { get; set; }
    }
}
