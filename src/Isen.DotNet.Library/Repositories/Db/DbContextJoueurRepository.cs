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
    public class DbContextJoueurRepository :
        BaseDbRepository<Joueur>,
        IJoueurRepository
    {
        public DbContextJoueurRepository(
            ApplicationDbContext dbContext) : 
            base(dbContext)
        {
        }

        public override IQueryable<Joueur> Includes(IQueryable<Joueur> includes)
        {
            var inc = base.Includes(includes);
            inc = inc.Include(e => e.BornIn);
            return inc;
        }
    }
}
