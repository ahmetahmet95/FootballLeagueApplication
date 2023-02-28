using DataAccess.Interface;
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

        [HttpGet]
        [Route("GetTeams")]
        public async Task<IEnumerable<Teams>> GetTeams()
        {
            var result =  await _repository.GetAllAsync();
            return result;
        }

        [HttpPost]
        [Route("CreateTeams")]
        public async Task<Teams> CreateTeams([FromBody] Teams model)
        {
            var result = await _repository.CreateAsync(model);
            return result;
        }
    }
}