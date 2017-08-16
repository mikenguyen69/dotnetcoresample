﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WCS.TeamService.Models;

namespace WCS.TeamService.Persistence
{
    public interface ITeamRepository
    {
        IEnumerable<Team> List();
       
        Team Get(Guid id);
        Team Add(Team team);
        Team Update(Team team);
        Team Delete(Guid id);

    }
}
