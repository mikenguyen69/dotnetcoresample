using Xunit;
using System.Collections.Generic;
using WCS.TeamService.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WCS.TeamService.Tests;

namespace WCS.TeamService
{

    public class TeamsControllerTest
    {
        
        [Fact]
        public void QueryTeamListReturnsCorrectTeams()
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());

            var rawTeams = (IEnumerable<Team>)
                (controller.GetAllTeams() as ObjectResult).Value;
            List<Team> teams = new List<Team>(rawTeams);
            Assert.Equal(teams.Count, 2);
            Assert.Equal(teams[0].Name, "one");
            Assert.Equal(teams[1].Name, "two");
        }

        [Fact]
        public void GetTeamRetrievesTeam()
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());

            string sampleName = "sample";
            Guid id = Guid.NewGuid();
            Team st = new Team(sampleName, id);
            controller.CreateTeam(st);

            Team retrievedTeam = (Team)(controller.GetTeam(id) as ObjectResult).Value;
            Assert.Equal(retrievedTeam.Name, sampleName);
            Assert.Equal(retrievedTeam.ID, id);

        }

        //public async void CreateTeamAddsTeamToList()
        //{
        //    //TeamsController controller = new TeamsController();
        //    var teams = (IEnumerable<Team>)
        //        (await controller.GetAllTeams() as ObjectResult).Value;

        //    List<Team> original = new List<Team>(teams);

        //    Team t = new Team("sample");
        //    var result = await controller.CreateTeam(t);

        //    var newTeamsRaw = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;

        //    List<Team> newTeams = new List<Team>(newTeamsRaw);
        //    Assert.Equal(newTeams.Count, original.Count + 1);
        //    var sampleTeam = newTeams.FirstOrDefault(target => target.Name = "sample");
        //    Assert.NotNull(sampleTeam);
        //}
    }
}
