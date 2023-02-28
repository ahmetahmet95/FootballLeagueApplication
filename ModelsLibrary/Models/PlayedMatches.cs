using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models
{
    public class PlayedMatches
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("HomeTeam"), Column(Order = 0)]
        public int? HomeTeamId { get; set; }

        [ForeignKey("GuestTeam"), Column(Order = 1)]
        public int? GuestTeamId { get; set; }

        [Required]
        public int FirstTeamScore { get; set; }

        [Required]
        public int SecondTeamScore { get; set; }

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
        public virtual Teams HomeTeam { get; set; }
        public virtual Teams GuestTeam { get; set; }
    }
}
