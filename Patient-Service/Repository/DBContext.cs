using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class DBContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DBContext()
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>().HasKey(p => p.Ssn);
    }
}
