using System;
using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Models;
using Isen.DotNet.Library.Repositories.Base;
using Isen.DotNet.Library.Repositories.Interfaces;

namespace Isen.DotNet.Library.Repositories.InMemory
{
    public class InMemoryClubRepository :
        BaseInMemoryRepository<Club>,
        IClubRepository
    {      
        public override List<Club> SampleData =>
            new List<Club>()
            {
                new Club() { Id = 1, Name = "Toulon", ZipCode = "83000" },
                new Club() { Id = 2, Name = "Marseille", ZipCode = "13000" },
                new Club() { Id = 3, Name = "Nice", ZipCode = "06000" },
                new Club() { Id = 4, Name = "Paris", ZipCode = "75000" },
                new Club() { Id = 5, Name = "Lyon", ZipCode = "69000" },
            };
    }
}