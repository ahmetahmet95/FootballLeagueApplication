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
        private readonly ITeamService _teamService;

        public PlayedMatchesApiController(IRepository<PlayedMatches> repository, ITeamService teamService)
        {
            _repository = repository;
            _teamService = teamService;
        }

        #region PlayedMatches

        [HttpGet]
        [Route("GetTeamsForCombo")]
        public List<Teams> GetTeamsForCombo()
        {
            var result = _teamService.GetTeams();
            return result;
        }

        [HttpGet]
        [Route("GetTeamsByIdForCombo/{id}")]
        public List<PlayedMatches> GetTeamsByIdForCombo(int id)
        {
            var result = _teamService.GetTeamsByIdForPlayedMatches(id);
            return result;
        }

        [HttpPost]
        [Route("CreatePlayedMatches")]
        public async Task<PlayedMatches> CreatePlayedMatches([FromBody] PlayedMatches model)
        {
            var result = await _repository.CreateAsync(model);
            await _repository.Save();
            return result;
        }

        [HttpGet]
        [Route("GetPlayedMatches")]
        public List<PlayedMatches> GetPlayedMatches()
        {
            var result = _teamService.GetPlayedMatches();
            return result;
        }

        [HttpPut]
        [Route("UpdatePlayedMatches")]
        public async Task<PlayedMatches> UpdatePlayedMatches([FromBody] PlayedMatches model)
        {
           var result = _repository.Update(model);
           await _repository.Save();
           return result;
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
