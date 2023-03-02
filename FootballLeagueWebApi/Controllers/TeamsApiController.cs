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

        public TeamsApiController(IRepository<Teams> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetTeams")]
        public async Task<IEnumerable<Teams>> GetTeams()
        {
            var result = await _repository.GetAllAsync();
            return result.OrderByDescending(x => x.Id);
        }

        [HttpGet]
        [Route("GetTeamById/{id}")]
        public async Task<Teams> GetTeamById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
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
                UpdatedOn = DateTime.Now
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
                UpdatedOn = DateTime.Now
            };

            var entity = await _repository.GetByIdAsync(teams.Id);
            entity.Name = model.Name;

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