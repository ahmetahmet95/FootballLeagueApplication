using DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;
using ModelsLibrary.Models.ViewModels;

namespace FootballLeagueWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsGroupApiController : ControllerBase
    {
        private readonly IRepository<TeamsGroup> _repository;

        public TeamsGroupApiController(IRepository<TeamsGroup> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetTeamsGroup")]
        public async Task<IEnumerable<TeamsGroup>> GetTeamsGroup()
        {
            var result = await _repository.GetAllAsync();
            return result.OrderByDescending(x => x.Id);
        }

        [HttpGet]
        [Route("GetTeamsGroupById/{id}")]
        public async Task<TeamsGroup> GetTeamsGroupById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result;
        }

        [HttpPost]
        [Route("CreateTeamsGroup")]
        public async Task<TeamsGroup> CreateTeamsGroup([FromBody] TeamsGroupViewModel model)
        {

            TeamsGroup teamsGroup = new TeamsGroup()
            {
                Id = model.Id,
                Name = model.Name,
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now,
                UpdatedBy = "Admin",
                UpdatedOn = DateTime.Now
            };
            var result = await _repository.CreateAsync(teamsGroup);
            await _repository.Save();
            return result;
        }

        [HttpPut]
        [Route("UpdateTeamsGroup")]
        public async Task<TeamsGroup> UpdateTeamsGroup([FromBody] TeamsGroupViewModel model)
        {
            TeamsGroup teamsGroup = new TeamsGroup()
            {
                Id = model.Id,
                Name = model.Name,
                UpdatedBy = "Admin",
                UpdatedOn = DateTime.Now
            };

            var entity = await _repository.GetByIdAsync(teamsGroup.Id);
            entity.Name = model.Name;

            var result = _repository.Update(entity);
            await _repository.Save();
            return result;
        }

        [HttpDelete]
        [Route("DeleteTeamById/{id}")]
        public async Task<TeamsGroup> DeleteTeamById(int id)
        {
            await _repository.DeleteByIdAsync(id);
            await _repository.Save();
            return null;
        }

    }
}
