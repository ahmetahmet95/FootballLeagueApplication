using DataAccess.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;
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
        private readonly ITeamService _teamsService;

        public TeamsApiController(IRepository<Teams> repository, ITeamService teamsService)
        {
            _repository = repository;
            _teamsService = teamsService;
        }

        [HttpPost]
        [Route("CreateTeam")]
        public async Task<Teams> CreateTeam([FromBody] Teams model)
        {
            var result = await _repository.CreateAsync(model);
            await _repository.Save();
            return null;
        }

        [HttpGet]
        [Route("GetTeams")]
        public async Task<IEnumerable<Teams>> GetTeams()
        {
            var result =  await _repository.GetAllAsync();
            return result;
        }

        [HttpGet]
        [Route("GetTeamById/{id}")]
        public async Task<Teams> GetTeamById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result;
        }

        [HttpPut]
        [Route("UpdateTeam")]
        public async Task<Teams> UpdateTeam([FromBody] Teams model)
        {
            var entity = await _repository.GetByIdAsync(model.Id);
            entity.Name = model.Name;
            entity.UpdatedBy = model.UpdatedBy;
            entity.UpdatedOn = model.UpdatedOn;

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