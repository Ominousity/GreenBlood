using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Patient_Repo
{
    public class DBContext : DbContext
    {
        public DbSet<PatientBe> Patients { get; set; }
        public DBContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO move connection string to appsettings.json
            optionsBuilder.UseSqlServer("Server=localhost;Database=patientData;User ID=API;Password=SuperSecret7!;TrustServerCertificate=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientBe>().HasKey(p => p.SSN);
        }
    }
}
