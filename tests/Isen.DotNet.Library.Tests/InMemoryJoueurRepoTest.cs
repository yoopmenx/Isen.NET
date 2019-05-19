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
    public class JoueurRepoFactory
    {
        public static IJoueurRepository Create(
            IClubRepository clubRepository = null) =>
                new InMemoryJoueurRepository(clubRepository ?? 
                    new InMemoryClubRepository());           
    }

    public class InMemoryJoueurRepoTest
    {
        [Fact]
        public void SingleById()
        {
            var joueurRepo = JoueurRepoFactory.Create();
            
            var joueur1 = joueurRepo.Single(1);
            Assert.True(joueur1.Id == 1);

            var noJoueur = joueurRepo.Single(42);
            Assert.True(noJoueur == null);      
        }

        [Fact]
        public void SingleByName()
        {
            var joueurRepo = JoueurRepoFactory.Create();

            var miles = joueurRepo.Single("DAVIS Miles");
            Assert.True(miles.Name == "DAVIS Miles");

            var fake = joueurRepo.Single("Fake");
            Assert.True(fake == null);
        }

        [Fact]
        public void UpdateUpdate()
        {
            var joueurRepo = JoueurRepoFactory.Create();
            var initialCount = joueurRepo.Context
                .ToList()
                .Count();
                
            var miles = joueurRepo.Single("DAVIS Miles");
            miles.LastName = "DAVIS Jr.";
            miles.DateOfBirth = new DateTime(1980, 1, 2);

            joueurRepo.Update(miles);
            joueurRepo.SaveChanges();

            var FinalCount = joueurRepo.Context
                .ToList()
                .Count();

            var milesUpdated = 
                joueurRepo.Single(miles.Id);
            Assert.True(milesUpdated.LastName == "DAVIS Jr.");
            Assert.True(milesUpdated.DateOfBirth == new DateTime(1980, 1, 2));
            Assert.True(initialCount == FinalCount);
        }

        [Fact]
        public void UpdateCreate()
        {
            var joueurRepo = JoueurRepoFactory.Create();
            var initialCount = joueurRepo.Context
                .ToList()
                .Count();

            var parker = new Joueur() 
            {
                FirstName = "Charlie",
                LastName = "PARKER",
                Name = "PARKER Charlie",
                DateOfBirth = new DateTime(1920, 8, 29)
            };
            joueurRepo.Update(parker);
            joueurRepo.SaveChanges();

            var FinalCount = joueurRepo.Context
                .ToList()
                .Count();
            Assert.True(initialCount == FinalCount - 1);

            var parkerCreated = joueurRepo.Single("PARKER Charlie");
            Assert.True(parkerCreated != null);
            Assert.True(parkerCreated.Name == "PARKER Charlie");
            Assert.True(!parkerCreated.IsNew);
        }

        [Fact]
        public void Delete()
        {
            var joueurRepo = JoueurRepoFactory.Create();
            var initialCount = joueurRepo.Context
                .ToList()
                .Count();

            var miles = joueurRepo.Single("DAVIS Miles");
            joueurRepo.Delete(miles);
            joueurRepo.SaveChanges();
            var finalCount = joueurRepo.Context
                .ToList()
                .Count();

            Assert.True(finalCount == initialCount - 1);
            Assert.True(joueurRepo.Single("DAVIS Miles") == null);
        }

        [Fact]
        public void GetAll()
        {
            var joueurRepo = JoueurRepoFactory.Create();
            var contextCount = joueurRepo.Context
                .ToList()
                .Count();
            
            var getAllCount = joueurRepo
                .GetAll()
                .ToList()
                .Count();

            Assert.True(contextCount == getAllCount);
        }

        [Fact]
        public void Find()
        {
            var joueurRepo = JoueurRepoFactory.Create();
            var query = joueurRepo
                .Find(c => c.Name.Contains("e"));
            var result = query.ToList();

            var countCitiesFromQuery = 0;
            foreach(var c in joueurRepo.Context.ToList())
            {
                if(c.Name.Contains("e"))
                    countCitiesFromQuery++;
            }
            Assert.True(result.Count == countCitiesFromQuery);
        }

        [Fact]
        public void DiTest()
        {
            IClubRepository clubRepo = new InMemoryClubRepository();
            var joueurRepo = JoueurRepoFactory.Create(clubRepo);

            Assert.True(
                joueurRepo
                    .Single("DAVIS Miles")?.BornIn?.Name == "Toulon");
            var clubId = joueurRepo
                    .Single("DAVIS Miles")?.BornIn?.Id;
            var toulon = clubRepo.Single("Toulon");
            toulon.Name = "New York";
            clubRepo.Update(toulon);
            clubRepo.SaveChanges();

            Assert.True(
                joueurRepo
                    .Single("DAVIS Miles")?.BornIn?.Name == "New York");
            var updatedClubId = joueurRepo
                    .Single("DAVIS Miles")?.BornIn?.Id;

            Assert.True(clubId == updatedClubId);
        }
    }
}