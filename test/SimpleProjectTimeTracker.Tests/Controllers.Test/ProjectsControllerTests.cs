using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleProjectTimeTracker.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace SimpleProjectTimeTracker.Web.Controllers
{
    public class ProjectsControllerTests
    {
        protected ProjectsController Sut { get; }
        protected SimpleProjectTimeTrackerDbContext DbContext { get; }
        protected DbContextOptions<SimpleProjectTimeTrackerDbContext> Options { get; }

        public ProjectsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<SimpleProjectTimeTrackerDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            Options = builder.Options;
            DbContext = new SimpleProjectTimeTrackerDbContext(builder.Options);
            Sut = new ProjectsController(DbContext);
        }

        [Fact]
        public void ControllerWithNullDbContextShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProjectsController(null));
        }

        public class GetProjects : ProjectsControllerTests
        {
            [Fact]
            public async void ShouldReturnOkWithProjectItems()
            {
                IEnumerable<Project> expectedProjects;

                using (var context = new SimpleProjectTimeTrackerDbContext(Options))
                {
                    context.Projects.Add(new Project
                    {
                        Id = 1,
                        CustomerName = "Microsoft",
                        Name = "Sample web project for BUILD event",
                        DueDate = new DateTime(2018, 7, 31),
                        HourlyRate = 45
                    });
                    context.Projects.Add(new Project
                    {
                        Id = 2,
                        CustomerName = "Amazon",
                        Name = "Creation of a Picking List application",
                        DueDate = new DateTime(2018, 9, 30),
                        HourlyRate = 47
                    });
                    await context.SaveChangesAsync();

                    expectedProjects = await context.Projects.ToListAsync();
                }

                var result = await Sut.GetProjects(CancellationToken.None);

                var okResult = Assert.IsType<OkObjectResult>(result);
                var okResultValue = Assert.IsAssignableFrom<IEnumerable<Project>>(okResult.Value);
                Assert.Equal(2, okResultValue.Count());
            }
        }
    }
}
