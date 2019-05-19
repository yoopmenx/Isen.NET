using System;
using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Models;
using Isen.DotNet.Library.Repositories.Base;
using Isen.DotNet.Library.Repositories.Interfaces;

namespace Isen.DotNet.Library.Repositories.InMemory
{
    public class InMemoryJoueurRepository :
        BaseInMemoryRepository<Joueur>,
        IJoueurRepository
    {   
        private readonly IClubRepository _clubRepository;
        
        // Pattern d'Injection de Dépendance
        // aka IoC : Inversion of Control
        // aka DI : Dependency Injection
        public InMemoryJoueurRepository(
            IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        public override List<Joueur> SampleData =>
            new List<Joueur>()
            {
                new Joueur()
                { 
                    Id = 1, 
                    FirstName = "Miles", 
                    LastName = "DAVIS", 
                    Name = "DAVIS Miles",
                    DateOfBirth = new DateTime(1926,5, 26),
                    BornIn = _clubRepository.Single("Toulon")
                },
                new Joueur()
                { 
                    Id = 2, 
                    FirstName = "Bill", 
                    LastName = "EVANS", 
                    Name = "EVANS Bill",
                    DateOfBirth = new DateTime(1929,8, 16),
                    BornIn = _clubRepository.Single("Nice")
                },
                new Joueur()
                { 
                    Id = 3, 
                    FirstName = "John", 
                    LastName = "COLTRANE", 
                    Name = "COLTRANE John",
                    DateOfBirth = new DateTime(1926, 9, 26),
                    BornIn = _clubRepository.Single("Lyon")
                }
            };
    }
}