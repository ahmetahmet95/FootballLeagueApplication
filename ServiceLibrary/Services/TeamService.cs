using DataAccessLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ModelsLibrary.Models;
using ServiceLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _dbContext;
        public TeamService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<PlayedMatches> GetTeamsByIdForPlayedMatches(int id)
        {
            var teams =

               from played in _dbContext.PlayedMatches
               join teamsOne in _dbContext.Teams on played.FirstTeamId equals teamsOne.Id
               join teamsTwo in _dbContext.Teams on played.SecondTeamId equals teamsTwo.Id
               where played.Id == id
               select new PlayedMatches
               {
                   Id = played.Id,
                   FirstTeam = teamsOne,
                   SecondTeam = teamsTwo,
                   FirstTeamScore = played.FirstTeamScore,
                   SecondTeamScore = played.SecondTeamScore,
                   Year = played.Year
               };

            return teams.ToList();
        }

        public List<Teams> GetTeams()
        {
            var teamModel = from teams in _dbContext.Teams
                            where teams.Id > 0
                            select new Teams() { Id = teams.Id, Name = teams.Name };

            return teamModel.ToList();
        }

        public List<PlayedMatches> GetPlayedMatches()
        {
            var playedMatches =

               from played in _dbContext.PlayedMatches
               join teamsOne in _dbContext.Teams on played.FirstTeamId equals teamsOne.Id
               join teamsTwo in _dbContext.Teams on played.SecondTeamId equals teamsTwo.Id
               select new PlayedMatches { 
                   Id = played.Id,
                   FirstTeam = teamsOne,
                   SecondTeam = teamsTwo,
                   FirstTeamScore = played.FirstTeamScore,
                   SecondTeamScore = played.SecondTeamScore,
                   Year = played.Year
               };

            return playedMatches.ToList();
        }

        public List<TeamsRank> GetTeamsRank()
        {
            var teamsRank =

               from ranks in _dbContext.TeamsRank
               join teams in _dbContext.Teams on ranks.TeamsId equals teams.Id
               orderby ranks.TotalPoint descending
               select new TeamsRank
               {
                   Id = ranks.Id,
                   Teams = teams,
                   Year = ranks.Year,
                   TotalPoint = ranks.TotalPoint
               };

            return teamsRank.ToList();
        }
    }
}
