using Domain;
using Measurments_Service.Be;
using Microsoft.EntityFrameworkCore;

namespace Measurments_Service.Repository
{
    public class MeasurmentContext: DbContext
    {
        public DbSet<MeasurmentBe> Measurments { get; set; }

        public MeasurmentContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
