using EstateAgency.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace EstateAgency.Data
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<RealEstate> RealEstates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<EstateType> EstateTypes { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<BuildingMaterial> BuildingMaterials { get; set; }
        public DbSet<Realtor> Realtors { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
    }
