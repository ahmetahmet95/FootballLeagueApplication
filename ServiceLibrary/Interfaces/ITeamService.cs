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
        List<TeamsGroup> GetTeamsGroup();
        List<PlayedMatches> GetTeamsByIdForPlayedMatches(int id);
        List<Teams> GetTeamsGroupByIdForTeams(int id);
        List<PlayedMatches> GetPlayedMatches();
        List<TeamsRank> GetTeamsRank(int id);
    }
}
