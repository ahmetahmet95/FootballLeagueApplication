using DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;
using ServiceLibrary.Interfaces;

namespace FootballLeagueWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsRankApiController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsRankApiController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        #region TeamsRank

        [HttpGet]
        [Route("GetTeamsRank/{id}")]
        public List<TeamsRank> GetTeamsRank(int id)
        {
            var result = _teamService.GetTeamsRank(id);
            return result;
        }

        #endregion

    }
}
