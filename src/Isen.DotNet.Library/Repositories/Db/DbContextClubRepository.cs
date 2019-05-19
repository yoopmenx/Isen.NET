using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Isen.DotNet.Library.Context;
using Isen.DotNet.Library.Models;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Isen.DotNet.Library.Repositories.Db
{
    public class DbContextClubRepository : 
        BaseDbRepository<Club>,
        IClubRepository
    {
        public DbContextClubRepository(
            ApplicationDbContext dbContext) : 
            base(dbContext)
        {
        }

        public override IQueryable<Club> Includes(IQueryable<Club> includes)
        {
            var inc = base.Includes(includes);
            inc = inc.Include(c => c.JoueurCollection);
            return inc;
        }
    }
}
