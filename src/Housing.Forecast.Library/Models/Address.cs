using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Housing.Forecast.Library.Models
{
    ///<summary>The Address class contains standard address information that will be used for Users, Batches, and Rooms</summary>
    ///<remarks>
    ///Each Address Object will have its own uniquely generated Guid Id and retain the primary key Guid that was generated for it in the previous database into AddressId.
    ///Each Address Object will have a collection of Users, Objects, and Batches that share the Address.
    ///</remarks>
    [Table("Addresses")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid AddressId { get; set; }

        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        [Required]
        [StringLength(255)]
        public string Address1 { get; set; }

        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Address2 { get; set; }

        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        [Required]
        [StringLength(25)]
        public string City { get; set; }

        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string State { get; set; }

        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        [Required]
        [StringLength(5, MinimumLength = 5)]
        public string PostalCode { get; set; }

        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Country { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        public ICollection<Batch> Batches { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
