using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Isen.DotNet.Library.Models;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace Isen.DotNet.Library.Context
{
    public class SeedData
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IClubRepository _clubRepository;
        private readonly IJoueurRepository _joueurRepository;

        public SeedData(
            ApplicationDbContext dbContext,
            IClubRepository clubRepository,
            IJoueurRepository joueurRepository)
        {
            _dbContext = dbContext;
            _clubRepository = clubRepository;
            _joueurRepository = joueurRepository;
        }

        public void DropCreateDatabase()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        public void AddClubs()
        {
            // Ne rien faire s'il y a déjà des clubs
            if (_dbContext.ClubCollection.Any()) return;

            var clubs = new List<Club>
            {
                new Club { Name = "Paris St.Germain", Logo = "https://upload.wikimedia.org/wikipedia/fr/8/86/Paris_Saint-Germain_Logo.svg", Latitude = 48.842368F, Longitude = 2.253177F},
                new Club { Name = "Paris FC", Logo = "https://upload.wikimedia.org/wikipedia/fr/d/db/Logo_Paris_FC_2011.svg", Latitude = 48.819289F, Longitude = 2.346438F},
                new Club { Name = "Olympique lyonnais", Logo = "https://upload.wikimedia.org/wikipedia/fr/e/e2/Olympique_lyonnais_%28logo%29.svg", Latitude = 45.7614F, Longitude = 4.9765F},
                new Club { Name = "Montpellier HSC", Logo = "https://upload.wikimedia.org/wikipedia/commons/9/99/Montpellier_H%C3%A9rault_Sport_Club_%28logo%2C_2000%29.svg", Latitude = 43.698101F, Longitude = 4.009557F},
                new Club { Name = "Girondins de Bordeaux", Logo = "https://upload.wikimedia.org/wikipedia/fr/7/76/Logo_des_Girondins_de_Bordeaux.svg", Latitude = 44.8672F, Longitude = -0.6171F},
                new Club { Name = "FC Fleury 91", Logo = "https://upload.wikimedia.org/wikipedia/fr/4/40/Logo_Football_Club_Fleury_91_C%C5%93ur_d%27Essonne.png", Latitude = 48.6189F, Longitude = 2.393F},
                new Club { Name = "EA Guingamp", Logo = "https://upload.wikimedia.org/wikipedia/fr/9/99/En_Avant_de_Guingamp_logo.svg", Latitude = 48.5056F, Longitude = -2.7437F},
                new Club { Name = "Dijon FCO", Logo = "https://upload.wikimedia.org/wikipedia/fr/c/c9/LogoDFCO.svg", Latitude = 47.324614F, Longitude = 5.068243F},
                new Club { Name = "Atlético de Madrid", Logo = "https://upload.wikimedia.org/wikipedia/fr/9/93/Logo_Atl%C3%A9tico_Madrid_2017.svg", Latitude = 40.459289F, Longitude = -3.859908F},
                new Club { Name = "Arsenal WFC", Logo = "https://upload.wikimedia.org/wikipedia/fr/5/53/Arsenal_FC.svg", Latitude = 51.6589F, Longitude = -0.2723F}

            };
            clubs.ForEach(club => _clubRepository.Update(club));
            _clubRepository.SaveChanges();
        }

        public void AddJoueurs()
        {
            // Ne rien faire si non vide
            if(_dbContext.JoueurCollection.Any()) return;

            var joueurs = new List<Joueur>
            {
                new Joueur
                {
                    FirstName = "Sarah",
                    LastName = "Bouhaddi",
                    DateOfBirth = new DateTime(1986,10,17),
                    BornIn = _clubRepository.Single("Olympique lyonnais")
                },
                new Joueur
                {
                    FirstName = "Solène",
                    LastName = "Durand",
                    DateOfBirth = new DateTime(1994,11,20),
                    BornIn = _clubRepository.Single("EA Guingamp")
                },
                new Joueur
                {
                    FirstName = "Pauline",
                    LastName = "Peyraud-Magnin",
                    DateOfBirth = new DateTime(1992,3,17),
                    BornIn = _clubRepository.Single("Arsenal WFC")
                },
                new Joueur
                {
                    FirstName = "Julie",
                    LastName = "Debever",
                    DateOfBirth = new DateTime(1988,4,18),
                    BornIn = _clubRepository.Single("EA Guingamp")
                },
                new Joueur
                {
                    FirstName = "Sakina",
                    LastName = "Karchaoui",
                    DateOfBirth = new DateTime(1996,1,26),
                    BornIn = _clubRepository.Single("Montpellier HSC")
                },
                new Joueur
                {
                    FirstName = "Amel",
                    LastName = "Majri",
                    DateOfBirth = new DateTime(1993,1,25),
                    BornIn = _clubRepository.Single("Olympique lyonnais")
                },
                new Joueur
                {
                    FirstName = "Griedge",
                    LastName = "Mbock",
                    DateOfBirth = new DateTime(1995,2,2),
                    BornIn = _clubRepository.Single("Olympique lyonnais")
                },
                new Joueur
                {
                    FirstName = "Ève",
                    LastName = "Périsset",
                    DateOfBirth = new DateTime(1994,12,24),
                    BornIn = _clubRepository.Single("Paris St.Germain")
                },
                new Joueur
                {
                    FirstName = "Wendie",
                    LastName = "Renard",
                    DateOfBirth = new DateTime(1990,7,2),
                    BornIn = _clubRepository.Single("Olympique lyonnais")
                },
                new Joueur
                {
                    FirstName = "Marion",
                    LastName = "Torrent",
                    DateOfBirth = new DateTime(1992,4,17),
                    BornIn = _clubRepository.Single("Montpellier HSC")
                },
                new Joueur
                {
                    FirstName = "Aïssatou",
                    LastName = "Tounkara",
                    DateOfBirth = new DateTime(1995,3,16),
                    BornIn = _clubRepository.Single("Atlético de Madrid")
                },
                new Joueur
                {
                    FirstName = "Charlotte",
                    LastName = "Bilbault",
                    DateOfBirth = new DateTime(1990,6,5),
                    BornIn = _clubRepository.Single("Paris FC")
                },
                new Joueur
                {
                    FirstName = "Élise",
                    LastName = "Bussaglia",
                    DateOfBirth = new DateTime(1985,9,24),
                    BornIn = _clubRepository.Single("Dijon FCO")
                },
                new Joueur
                {
                    FirstName = "Maéva",
                    LastName = "Clemaron",
                    DateOfBirth = new DateTime(1992,11,10),
                    BornIn = _clubRepository.Single("FC Fleury 91")
                },
                new Joueur
                {
                    FirstName = "Grace",
                    LastName = "Geyoro",
                    DateOfBirth = new DateTime(1997,7,2),
                    BornIn = _clubRepository.Single("Paris St.Germain")
                },
                new Joueur
                {
                    FirstName = "Amandine",
                    LastName = "Henry",
                    DateOfBirth = new DateTime(1989,9,28),
                    BornIn = _clubRepository.Single("Olympique lyonnais")
                },
                new Joueur
                {
                    FirstName = "Gaëtane",
                    LastName = "Thiney",
                    DateOfBirth = new DateTime(1985,10,28),
                    BornIn = _clubRepository.Single("Paris FC")
                },
                new Joueur
                {
                    FirstName = "Viviane",
                    LastName = "Asseyi",
                    DateOfBirth = new DateTime(1993,11,20),
                    BornIn = _clubRepository.Single("Girondins de Bordeaux")
                },
                new Joueur
                {
                    FirstName = "Delphine",
                    LastName = "Cascarino",
                    DateOfBirth = new DateTime(1997,2,5),
                    BornIn = _clubRepository.Single("Olympique lyonnais")
                },
                new Joueur
                {
                    FirstName = "Kadidiatou",
                    LastName = "Diani",
                    DateOfBirth = new DateTime(1994,4,1),
                    BornIn = _clubRepository.Single("Paris St.Germain")
                },
                new Joueur
                {
                    FirstName = "Valérie",
                    LastName = "Gauvin",
                    DateOfBirth = new DateTime(1996,6,1),
                    BornIn = _clubRepository.Single("Montpellier HSC")
                },
                new Joueur
                {
                    FirstName = "Emelyne",
                    LastName = "Laurent",
                    DateOfBirth = new DateTime(1998,11,4),
                    BornIn = _clubRepository.Single("EA Guingamp")
                },
                new Joueur
                {
                    FirstName = "Eugénie",
                    LastName = "Le Sommer",
                    DateOfBirth = new DateTime(1989,5,18),
                    BornIn = _clubRepository.Single("Olympique lyonnais")
                }
            };
            joueurs.ForEach(club => _joueurRepository.Update(club));
            _joueurRepository.SaveChanges();
        }
    }
}
