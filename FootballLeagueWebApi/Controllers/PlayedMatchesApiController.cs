using DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;
using ModelsLibrary.Models.ViewModels;
using ServiceLibrary.Interfaces;

namespace FootballLeagueWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayedMatchesApiController : ControllerBase
    {

        private readonly IRepository<PlayedMatches> _repository;
        private readonly ITeamService _teamsService;

        public PlayedMatchesApiController(IRepository<PlayedMatches> repository, ITeamService teamsService)
        {
            _repository = repository;
            _teamsService = teamsService;
        }

        #region PlayedMatches

        [HttpGet]
        [Route("GetTeamsForCombo")]
        public List<Teams> GetTeamsForCombo()
        {
            var result = _teamsService.GetTeams();
            return result;
        }

        [HttpGet]
        [Route("GetTeamsByIdForCombo/{id}")]
        public List<PlayedMatches> GetTeamsByIdForCombo(int id)
        {
            var result = _teamsService.GetTeamsByIdForPlayedMatches(id);
            return result;
        }

        [HttpPost]
        [Route("CreatePlayedMatches")]
        public async Task<Teams> CreatePlayedMatches([FromBody] PlayedMatches model)
        {
            var result = await _repository.CreateAsync(model);
            await _repository.Save();
            return null;
        }

        [HttpGet]
        [Route("GetPlayedMatches")]
        public List<PlayedMatches> GetPlayedMatches()
        {
            var result = _teamsService.GetPlayedMatches();
            return result;
        }

        [HttpPut]
        [Route("UpdatePlayedMatches")]
        public async Task<PlayedMatches> UpdatePlayedMatches([FromBody] PlayedMatches model)
        {
            _repository.Update(model);
            await _repository.Save();
            return null;
        }

        [HttpDelete]
        [Route("DeletePlayedMatchesById/{id}")]
        public async Task<PlayedMatches> DeletePlayedMatchesById(int id)
        {
            await _repository.DeleteByIdAsync(id);
            await _repository.Save();
            return null;
        }

        
        #endregion
    }
}
