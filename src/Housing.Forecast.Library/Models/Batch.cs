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
    [Table("Batches")]
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
            Address = null;
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
            if (Address == null) { return false; }

            return true;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [Required]
        public Guid BatchId { get; set; }


        [DataType(DataType.DateTime)]
        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }


        [DataType(DataType.Text)]
        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string BatchName { get; set; }

        [Range(0, 100)]
        [Required]
        public int BatchOccupancy { get; set; }

        [DataType(DataType.Text)]
        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string BatchSkill { get; set; }

        [Required]
        public Address Address { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Deleted { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
