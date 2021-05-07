using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Models;

namespace RealEstate.Infrasctructure.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):
            base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyImage> PropertyImages { get; set; }

        public DbSet<PropertyTrace> PropertyTraces { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>()
                .HasOne<Owner>(x => x.Owner)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.OwnerId);


            modelBuilder.Entity<PropertyImage>()
                .HasOne<Property>(x => x.Property)
                .WithMany(x => x.PropertyImages)
                .HasForeignKey(x => x.PropertyId);


            modelBuilder.Entity<PropertyTrace>()
                .HasOne<Property>(x => x.Property)
                .WithMany(x => x.PropertyTraces)
                .HasForeignKey(x => x.PropertyId);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
