using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models.ViewModels
{
    public class PlayedMatchesViewModel
    {
        public int Id { get; set; }
        public int? FirstTeamId { get; set; }
        public int? SecondTeamId { get; set; }
        public int FirstTeamScore { get; set; }
        public int SecondTeamScore { get; set; }
        public int FirstTeamGoal { get; set; }
        public int SecondTeamGoal { get; set; }
    }
}
