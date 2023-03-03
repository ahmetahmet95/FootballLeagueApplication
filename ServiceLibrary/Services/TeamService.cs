using DataAccessLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ModelsLibrary.Models;
using ServiceLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
                   Year = played.Year,
                   FirstTeamGoal = played.FirstTeamGoal,
                   SecondTeamGoal = played.SecondTeamGoal
               };

            return teams.ToList();
        }

        public List<Teams> GetTeamsGroupByIdForTeams(int id)
        {
            var teamsModel =
               from teams in _dbContext.Teams
               join teamsGroup in _dbContext.TeamsGroup on teams.TeamsGroupId equals teamsGroup.Id
               where teams.Id == id
               select new Teams
               {
                   Id = teams.Id,
                   Name = teams.Name,
                   TeamsGroup = teamsGroup
               };

            return teamsModel.ToList();
        }

        public List<Teams> GetTeams()
        {
            var teamModel = from teams in _dbContext.Teams
                            join teamGroup in _dbContext.TeamsGroup on teams.TeamsGroupId equals teamGroup.Id
                            orderby teams.Id descending
                            where teams.Id > 0
                            select new Teams() { Id = teams.Id, Name = teams.Name, TeamsGroup = teamGroup };

            return teamModel.ToList();
        }

        public List<Teams> GetTeamsGroup()
        {
            var teamModel = from teamsGroup in _dbContext.TeamsGroup
                            where teamsGroup.Id > 0
                            select new Teams() { Id = teamsGroup.Id, Name = teamsGroup.Name };

            return teamModel.ToList();
        }


        public List<PlayedMatches> GetPlayedMatches()
        {
            var playedMatches =
               from played in _dbContext.PlayedMatches
               join teamsOne in _dbContext.Teams on played.FirstTeamId equals teamsOne.Id
               join teamsTwo in _dbContext.Teams on played.SecondTeamId equals teamsTwo.Id
               orderby played.Id descending
               select new PlayedMatches
               {
                   Id = played.Id,
                   FirstTeam = teamsOne,
                   SecondTeam = teamsTwo,
                   FirstTeamScore = played.FirstTeamScore,
                   SecondTeamScore = played.SecondTeamScore,
                   Year = played.Year,
                   FirstTeamGoal = played.FirstTeamGoal,
                   SecondTeamGoal = played.SecondTeamGoal
               };

            return playedMatches.ToList();
        }

        public List<TeamsRank> GetTeamsRank(int id = 0)
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
                   TotalPoint = ranks.TotalPoint,
                   TeamsGroup = teams.TeamsGroup
               };

            if (id != 0)
            {
                teamsRank = teamsRank.Where(x => x.TeamsGroup.Id == id);
            }

            return teamsRank.ToList();
        }

        List<TeamsGroup> ITeamService.GetTeamsGroup()
        {
            var teamModel = from teams in _dbContext.TeamsGroup
                            where teams.Id > 0
                            select new TeamsGroup() { Id = teams.Id, Name = teams.Name };

            return teamModel.ToList();
        }
    }
}
