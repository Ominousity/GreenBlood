using API;
using Microsoft.EntityFrameworkCore;

namespace PatientRepository;

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
