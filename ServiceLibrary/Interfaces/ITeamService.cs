using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Interfaces
{
    public interface ITeamService
    {
        List<Teams> GetTeams();

        List<PlayedMatches> GetTeamsByIdForPlayedMatches(int id);

        List<PlayedMatches> GetPlayedMatches();
    }
}
