using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using WCS.TeamService.Models;
using WCS.TeamService.Persistence;
using Xunit;

namespace WCS.TeamService.Tests
{
    public class MembersControllerTest
    {
        [Fact]
        public void CreateMemberAddsTeamToList()
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestController", teamId);
            repository.Add(team);

            Guid newMemberId = Guid.NewGuid();
            Member newMember = new Member(newMemberId);
            controller.CreateMember(newMember, teamId);

            team = repository.Get(teamId);
            Assert.True(team.Members.Contains(newMember));
        }

        [Fact]
        public void CreateMembertoNonexistantTeamReturnsNotFound()
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);

            Guid teamId = Guid.NewGuid();

            Guid newMemberId = Guid.NewGuid();
            Member newMember = new Member(newMemberId);
            var result = controller.CreateMember(newMember, teamId);

            Assert.True(result is NotFoundResult);
        }
    }
}
