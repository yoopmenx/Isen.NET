using System;
using System.Collections.Generic;
using System.Text;
using Isen.DotNet.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Isen.DotNet.Library.Context
{
    public class ApplicationDbContext : DbContext
    {
        // Collection des objets du modèle
        public DbSet<Club> ClubCollection { get; set; }
        public DbSet<Joueur> JoueurCollection { get; set; }

        // Constructeur avec signature obligatoire
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Préciser les tables et relations du modèle
            modelBuilder.Entity<Club>()
                .ToTable(nameof(Club));
            modelBuilder.Entity<Joueur>()
                .ToTable(nameof(Joueur))
                .HasOne(p => p.BornIn)
                .WithMany(c => c.JoueurCollection)
                .HasForeignKey(p => p.BornInId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
