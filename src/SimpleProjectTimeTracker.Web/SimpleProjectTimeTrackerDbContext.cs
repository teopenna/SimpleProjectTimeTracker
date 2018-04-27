using Microsoft.EntityFrameworkCore;
using SimpleProjectTimeTracker.Web.Helpers;
using SimpleProjectTimeTracker.Web.Models;

namespace SimpleProjectTimeTracker.Web
{
    public class SimpleProjectTimeTrackerDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeRegistrationEntity> TimeRegistrations { get; set; }
        public DbSet<InvoiceEntity> Invoices { get; set; }

        public SimpleProjectTimeTrackerDbContext(DbContextOptions<SimpleProjectTimeTrackerDbContext> options)
            : base(options)
        {
        }

        public void SeedDatabase()
        {
            SampleDataGenerator.SeedData(this);
        }
    }
}
