
using Microsoft.EntityFrameworkCore;
using ConcreteCrackManager.Models;

namespace ConcreteCrackManager.Data
{
    public class ConcreteDbContext : DbContext
    {
        public ConcreteDbContext(DbContextOptions<ConcreteDbContext> options) : base(options) { }

        public DbSet<AppRole> AppRoles { get; set; } = null!;
        public DbSet<AppUser> AppUsers { get; set; } = null!;
        public DbSet<Inspection> Inspections { get; set; } = null!;
        public DbSet<ImageEntity> Images { get; set; } = null!;
        public DbSet<Defect> Defects { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("app");

            modelBuilder.Entity<AppRole>().ToTable("AppRoles");
            modelBuilder.Entity<AppUser>().ToTable("AppUsers");
            modelBuilder.Entity<Inspection>().ToTable("Inspections");
            modelBuilder.Entity<ImageEntity>().ToTable("Images");
            modelBuilder.Entity<Defect>().ToTable("Defects");
        }
    }
}
