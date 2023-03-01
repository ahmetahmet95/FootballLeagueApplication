using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models
{
    public class TeamsRank
    {
        [Key]
        public int Id { get; set; }

        public int? TeamsId { get; set; }

        [ForeignKey("TeamsId")]
        public virtual Teams? Teams { get; set; }

        [Required]
        public int Year { get; set; }

        [Required, MaxLength(50)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required, MaxLength(50)]
        public string UpdatedBy { get; set; }

        [Required]
        public DateTime UpdatedOn { get; set; }

        public int TotalPoint { get; set; }
    }
}
