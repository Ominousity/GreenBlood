using Domain;
using Measurments_Service.Be;
using Microsoft.EntityFrameworkCore;

namespace Measurments_Service.Repository
{
    public class MeasurmentContext: DbContext
    {
        public DbSet<MeasurmentBe> Measurments { get; set; }

        public MeasurmentContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO move connection string to appsettings.json
            optionsBuilder.UseNpgsql("Host=measurement-service;Port=5433;Database=MeasurmentDB;Username=postgres;Password=password");
        }
    }
    }
