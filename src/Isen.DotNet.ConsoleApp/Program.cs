using System;
using System.Collections.Generic;
using Isen.DotNet.Library;
using Isen.DotNet.Library.Lists;
using Isen.DotNet.Library.Models;
using Isen.DotNet.Library.Repositories.InMemory;
using Isen.DotNet.Library.Repositories.Interfaces;

namespace Isen.DotNet.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IClubRepository clubRepo = 
                new InMemoryClubRepository();
            IJoueurRepository joueurRepo = 
                new InMemoryJoueurRepository(clubRepo);

            foreach(var p in joueurRepo.Context)
                Console.WriteLine(p);

            var toulon = clubRepo.Single("Toulon");
            toulon.Name = "New York";
            clubRepo.Update(toulon);
            clubRepo.SaveChanges();

            foreach(var p in joueurRepo.Context)
                Console.WriteLine(p);
        }
    }
}
