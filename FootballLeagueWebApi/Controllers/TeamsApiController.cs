using DataAccess.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;

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
        [Route("UpdateTeamById/{id}")]
        public async Task<Teams> UpdateTeamById(int id)
        {
            var result = await _repository.UpdateAsync(model);
            await _repository.Save();
            return result;
        }

    }
}