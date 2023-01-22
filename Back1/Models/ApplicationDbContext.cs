using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace Back1.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Marque> Marques { get; set; }
        public DbSet<ModePaiement> ModePaiments { get; set; }
        public DbSet<Offre> Offres { get; set; }
        public DbSet<Plainte> Plaintes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Voiture> Voitures { get; set; }

        

    }
}
