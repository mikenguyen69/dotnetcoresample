﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WCS.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using WCS.TeamService.Persistence;

namespace WCS.TeamService
{
    [Route("[controller]")]
    public class TeamsController : Controller
    {
        ITeamRepository repository;

        public TeamsController(ITeamRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public virtual IActionResult GetAllTeams()
        {
            //return new Team[] { new Team("one"), new Team("two") };
            return this.Ok(repository.List());
        }

        [HttpGet]
        public IActionResult GetTeam(Guid id)
        {
            Team team = repository.Get(id);

            if (team != null)
            {
                return this.Ok(team);
            }
            else
                return this.NotFound();
        }

        [HttpPost]
        public virtual IActionResult CreateTeam([FromBody]Team newTeam)
        {
            repository.Add(newTeam);

            //TODO: add test that asserts result is a 201 pointing to URL of the created team.
            //TODO: teams need IDs
            //TODO: return created at route to point to team details			
            return this.Created($"/teams/{newTeam.ID}", newTeam);
        }

        [HttpPut("{id}")]
        public virtual IActionResult UpdateTeam([FromBody]Team team, Guid id)
        {
            team.ID = id;

            if (repository.Update(team) == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(team);
            }
        }

        [HttpDelete("{id}")]
        public virtual IActionResult DeleteTeam(Guid id)
        {
            Team team = repository.Delete(id);

            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(team.ID);
            }
        }
    }
}
