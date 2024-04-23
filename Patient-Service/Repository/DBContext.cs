using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class DBContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Measurement> Measurements { get; set; }
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>().HasKey(p => p.Id);
        modelBuilder.Entity<Patient>().HasMany(p => p.Measurements);
    }
}
