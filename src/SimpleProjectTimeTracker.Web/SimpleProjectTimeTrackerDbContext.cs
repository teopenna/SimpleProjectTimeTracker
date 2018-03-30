using Microsoft.EntityFrameworkCore;
using SimpleProjectTimeTracker.Web.Models;

namespace SimpleProjectTimeTracker.Web
{
    public class SimpleProjectTimeTrackerDbContext : DbContext
    {
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<TimeRegistrationEntity> TimeRegistrations { get; set; }

        public SimpleProjectTimeTrackerDbContext(DbContextOptions<SimpleProjectTimeTrackerDbContext> options)
            : base(options)
        {
        }
    }
}
