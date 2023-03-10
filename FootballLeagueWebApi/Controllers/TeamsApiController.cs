using DataAccess.Interface;
using DataAccessLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;
using ModelsLibrary.Models.ViewModels;
using ServiceLibrary.Interfaces;
using ServiceLibrary.Services;
using System.ComponentModel.DataAnnotations;


namespace FootballLeagueWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsApiController : ControllerBase
    {

        private readonly IRepository<Teams> _repository;
        private readonly ITeamService _teamService;   

        public TeamsApiController(IRepository<Teams> repository, ITeamService teamService)
        {
            _repository = repository;
            _teamService = teamService;
        }

        [HttpGet]
        [Route("GetTeams")]
        public List<Teams> GetTeams()
        {
            var result = _teamService.GetTeams();
            return result;
        }

        [HttpGet]
        [Route("GetTeamsGroupForCombo")]
        public List<TeamsGroup> GetTeamsGroupForCombo()
        {
            var result = _teamService.GetTeamsGroup();
            return result;
        }

        [HttpGet]
        [Route("GetTeamsGroupByIdForCombo/{id}")]
        public List<Teams> GetTeamsGroupByIdForCombo(int id)
        {
            var result = _teamService.GetTeamsGroupByIdForTeams(id);
            return result;
        }

        [HttpPost]
        [Route("CreateTeam")]
        public async Task<Teams> CreateTeam([FromBody] TeamsViewModel model)
        {

            Teams teams = new Teams()
            {
                Id = model.Id,
                Name = model.Name,
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now,
                UpdatedBy = "Admin",
                UpdatedOn = DateTime.Now,
                TeamsGroupId = model.TeamsGroupId
            };
            var result = await _repository.CreateAsync(teams);
            await _repository.Save();
            return result;
        }
     
        [HttpPut]
        [Route("UpdateTeam")]
        public async Task<Teams> UpdateTeam([FromBody] TeamsViewModel model)
        {
            Teams teams = new Teams()
            {
                Id = model.Id,
                Name = model.Name,
                UpdatedBy = "Admin",
                UpdatedOn = DateTime.Now,
                TeamsGroupId = model.TeamsGroupId
            };

            var entity = await _repository.GetByIdAsync(teams.Id);
            entity.Name = model.Name;
            entity.TeamsGroupId = model.TeamsGroupId;
            var result = _repository.Update(entity);
            await _repository.Save();
            return result;
        }

        [HttpDelete]
        [Route("DeleteTeamById/{id}")]
        public async Task<Teams> DeleteTeamById(int id)
        {
            await _repository.DeleteByIdAsync(id);
            await _repository.Save();
            return null;
        }

    }
}