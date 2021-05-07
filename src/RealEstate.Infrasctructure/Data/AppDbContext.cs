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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Owner>(p =>
            {
                p.HasMany(x => x.Properties).WithOne(x => x.Owner).HasForeignKey(y => y.OwnerId);
            });

            modelBuilder.Entity<Property>(p =>
            {
                p.HasOne(x => x.Owner).WithMany().HasForeignKey(y => y.OwnerId);
                p.HasMany(x => x.PropertyImages).WithOne(x => x.Property).HasForeignKey(x => x.PropertyId);
                p.HasMany(x => x.PropertyTraces).WithOne(x => x.Property).HasForeignKey(x => x.PropertyId);
            });

            modelBuilder.Entity<PropertyImage>(p =>
            {
                p.HasOne(x => x.Property).WithMany().HasForeignKey(y => y.PropertyId);
            });

            modelBuilder.Entity<PropertyTrace>(p =>
            {
                p.HasOne(x => x.Property).WithMany().HasForeignKey(y => y.PropertyId);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
