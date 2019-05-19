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
    public class DbContextHistoriqueRepository :
        BaseDbRepository<Historique>,
        IHistoriqueRepository
    {
        public DbContextHistoriqueRepository(
            ApplicationDbContext dbContext) : 
            base(dbContext)
        {
        }

        public override IQueryable<Historique> Includes(IQueryable<Historique> includes)
        {
            var inc = base.Includes(includes);
            inc = inc.Include(e => e);
            return inc;
        }
    }
}
