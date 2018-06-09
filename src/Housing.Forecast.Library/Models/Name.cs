using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Housing.Forecast.Library.Models
{
    ///<summary>The Name Class contains standard information regarding a User's name including first name, middle name, and last name.</summary>
    ///<remarks>
    ///Each Name will have a uniquely generated Guid Id as well as retain the primary key Guid of the previous database, which is stored in NameId.
    ///Each Name object will have a collection of Users in the case that multiple users share the same name.
    ///</remarks>
    [Table("Names")]
    public class Name
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [Required]
        public Guid NameId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        public string First { get; set; }

        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Middle { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Last { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
