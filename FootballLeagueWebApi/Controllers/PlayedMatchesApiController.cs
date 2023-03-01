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

        [HttpGet]
        [Route("GetPlayedMatches")]
        public List<PlayedMatches> GetPlayedMatches()
        {
            var result = _teamService.GetPlayedMatches();
            return result;
        }

        [HttpPost]
        [Route("CreatePlayedMatches")]
        public async Task<PlayedMatches> CreatePlayedMatches([FromBody] PlayedMatchesViewModel model)
        {
            PlayedMatches playedMatches = new PlayedMatches()
            {
                Id = model.Id,
                FirstTeamId = model.FirstTeamId,
                FirstTeamScore = model.FirstTeamScore,
                SecondTeamId = model.SecondTeamId,
                SecondTeamScore = model.SecondTeamScore,
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now,
                UpdatedBy = "Admin",
                UpdatedOn = DateTime.Now,
                Year = DateTime.Now.Year
            };

            var result = await _repository.CreateAsync(playedMatches);
            await _repository.Save();
            return result;
        }

        [HttpPut]
        [Route("UpdatePlayedMatches")]
        public async Task<PlayedMatches> UpdatePlayedMatches([FromBody] PlayedMatchesViewModel model)
        {
            PlayedMatches playedMatches = new PlayedMatches()
            {
                Id = model.Id,
                FirstTeamId = model.FirstTeamId,
                FirstTeamScore = model.FirstTeamScore,
                SecondTeamId = model.SecondTeamId,
                SecondTeamScore = model.SecondTeamScore,
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now,
                UpdatedBy = "Admin",
                UpdatedOn = DateTime.Now,
                Year = DateTime.Now.Year,
                FirstTeam = null,
                SecondTeam = null
            };

            var result = _repository.Update(playedMatches);
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
