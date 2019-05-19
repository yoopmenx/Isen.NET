using System;
using Xunit;
using Isen.DotNet.Library.Lists;
using System.Collections.Generic;
using Isen.DotNet.Library.Repositories.InMemory;
using System.Linq;
using Isen.DotNet.Library.Models;
using Isen.DotNet.Library.Repositories.Interfaces;

namespace Isen.DotNet.Library.Tests
{
    public class ClubRepoFactory
    {
        public static IClubRepository Create() =>
            new InMemoryClubRepository();
    }
    public class InMemoryClubRepoTest
    {
        [Fact]
        public void SingleById()
        {
            var clubRepo = ClubRepoFactory.Create();
            
            var club1 = clubRepo.Single(1);
            Assert.True(club1.Id == 1);

            var noClub = clubRepo.Single(42);
            Assert.True(noClub == null);      
        }

        [Fact]
        public void SingleByName()
        {
            var clubRepo = ClubRepoFactory.Create();

            var toulon = clubRepo.Single("Toulon");
            Assert.True(toulon.Name == "Toulon");

            var fake = clubRepo.Single("Fake");
            Assert.True(fake == null);
        }

        [Fact]
        public void UpdateUpdate()
        {
            var clubRepo = ClubRepoFactory.Create();
            var initialCount = clubRepo.Context
                .ToList()
                .Count();
                
            var toulon = clubRepo.Single("Toulon");
            toulon.Name = "Toulon sur Mer";
            toulon.ZipCode = "83200";

            clubRepo.Update(toulon);
            clubRepo.SaveChanges();

            var FinalCount = clubRepo.Context
                .ToList()
                .Count();

            var toulonUpdated = 
                clubRepo.Single(toulon.Id);
            Assert.True(toulonUpdated.Name == "Toulon sur Mer");
            Assert.True(toulonUpdated.ZipCode == "83200");
            Assert.True(initialCount == FinalCount);
        }

        [Fact]
        public void UpdateCreate()
        {
            var clubRepo = ClubRepoFactory.Create();
            var initialCount = clubRepo.Context
                .ToList()
                .Count();

            var gap = new Club() 
            {
                Name = "Gap",
                ZipCode = "05000"
            };
            clubRepo.Update(gap);
            clubRepo.SaveChanges();

            var FinalCount = clubRepo.Context
                .ToList()
                .Count();
            Assert.True(initialCount == FinalCount - 1);

            var gapCreated = clubRepo.Single("Gap");
            Assert.True(gapCreated != null);
            Assert.True(gapCreated.ZipCode == "05000");
            Assert.True(!gapCreated.IsNew);
        }

        [Fact]
        public void Delete()
        {
            var clubRepo = ClubRepoFactory.Create();
            var initialCount = clubRepo.Context
                .ToList()
                .Count();

            var toulon = clubRepo.Single("Toulon");
            clubRepo.Delete(toulon);
            clubRepo.SaveChanges();
            var finalCount = clubRepo.Context
                .ToList()
                .Count();

            Assert.True(finalCount == initialCount - 1);
            Assert.True(clubRepo.Single("Toulon") == null);
        }

        [Fact]
        public void GetAll()
        {
            var clubRepo = ClubRepoFactory.Create();
            var contextCount = clubRepo.Context
                .ToList()
                .Count();
            
            var getAllCount = clubRepo
                .GetAll()
                .ToList()
                .Count();

            Assert.True(contextCount == getAllCount);
        }

        [Fact]
        public void Find()
        {
            var clubRepo = ClubRepoFactory.Create();
            var query = clubRepo
                .Find(c => c.Name.Contains("e"));
            var result = query.ToList();

            var countCitiesFromQuery = 0;
            foreach(var c in clubRepo.Context.ToList())
            {
                if(c.Name.Contains("e"))
                    countCitiesFromQuery++;
            }
            Assert.True(result.Count == countCitiesFromQuery);
        }
    }
}