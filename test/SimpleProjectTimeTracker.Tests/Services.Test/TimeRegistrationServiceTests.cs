using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleProjectTimeTracker.Web;
using SimpleProjectTimeTracker.Web.Exceptions;
using SimpleProjectTimeTracker.Web.MappingProfiles;
using SimpleProjectTimeTracker.Web.Models;
using SimpleProjectTimeTracker.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SimpleProjectTimeTracker.Tests.Services.Test
{
    public class TimeRegistrationServiceTests
    {
        protected TimeRegistrationService Sut { get; }
        protected SimpleProjectTimeTrackerDbContext DbContext { get; }
        protected DbContextOptions<SimpleProjectTimeTrackerDbContext> Options { get; }
        protected IMapper Mapper { get; }

        public TimeRegistrationServiceTests()
        {
            var builder = new DbContextOptionsBuilder<SimpleProjectTimeTrackerDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            Options = builder.Options;
            DbContext = new SimpleProjectTimeTrackerDbContext(builder.Options);
            DbContext.Database.EnsureCreated();
            DbContext.SeedDatabase();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            Mapper = config.CreateMapper();

            Sut = new TimeRegistrationService(DbContext, Mapper);
        }

        public class ReadAllAsync : TimeRegistrationServiceTests
        {
            [Fact]
            public async void ShouldReturnAllTimeRegistrationEntities()
            {
                var expectedTimeRegistrationEntities = await DbContext.TimeRegistrations.ToListAsync();

                var expectedTimeRegistrations = Mapper.Map<IEnumerable<TimeRegistration>>(expectedTimeRegistrationEntities);
                var result = await Sut.ReadAllAsync(CancellationToken.None);

                Assert.IsAssignableFrom<IEnumerable<TimeRegistration>>(result);
                Assert.Equal(3, result.Count());
            }
        }

        public class ReadSingleAsync : TimeRegistrationServiceTests
        {
            [Fact]
            public async void ShouldReturnTimeRegistrationNotFoundExceptionIfTimeRegistrationIdDoesntExist()
            {
                var expectedTimeRegistrationEntities = await DbContext.TimeRegistrations.ToListAsync();

                var expectedTimeRegistrations = Mapper.Map<IEnumerable<TimeRegistration>>(expectedTimeRegistrationEntities);

                await Assert.ThrowsAsync<TimeRegistrationNotFoundException>(() => Sut.ReadSingleAsync(100, CancellationToken.None));
            }

            [Fact]
            public async void ShouldReturnCorrectTimeRegistrationIfIdExist()
            {
                var expectedTimeRegistrationEntity = await DbContext
                    .TimeRegistrations
                    .SingleOrDefaultAsync(t => t.Id == 1);

                var expectedTimeRegistration = Mapper.Map<TimeRegistration>(expectedTimeRegistrationEntity);
                var result = await Sut.ReadSingleAsync(1, CancellationToken.None);

                Assert.IsAssignableFrom<TimeRegistration>(result);
                Assert.Equal("Sample web project for BUILD event", result.ProjectName);
            }
        }
    }
}
