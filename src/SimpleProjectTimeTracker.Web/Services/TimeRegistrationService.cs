using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleProjectTimeTracker.Web.Exceptions;
using SimpleProjectTimeTracker.Web.Models;

namespace SimpleProjectTimeTracker.Web.Services
{
    public class TimeRegistrationService : ITimeRegistrationService
    {
        private readonly SimpleProjectTimeTrackerDbContext _dbContext;
        private readonly IMapper _mapper;

        public TimeRegistrationService(SimpleProjectTimeTrackerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TimeRegistration> CreateAsync(TimeRegistration timeRegistration, CancellationToken cancellationToken)
        {
            var timeRegistrationEntity = _mapper.Map<TimeRegistrationEntity>(timeRegistration);

            var timeRegistrationEntities = await _dbContext
                .TimeRegistrations
                .AsNoTracking()
                .ToListAsync();

            if (timeRegistrationEntities.Count >= 30)
            {
                throw new Exception("This is a demo application, you can add no more than 30 registrations.");
            }
            
            timeRegistrationEntity.Project = null;

            var newTimeRegistration = await _dbContext.TimeRegistrations.AddAsync(timeRegistrationEntity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Mapper.Map<TimeRegistration>(newTimeRegistration.Entity);
        }

        public async Task<TimeRegistration> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var timeRegistrationEntity = await GetTimeRegistrationById(id, cancellationToken);

            if (timeRegistrationEntity.Accounted)
            {
                throw new Exception("Time registration is already accounted, no changing or deleting is possible");
            }

            _dbContext.TimeRegistrations.Remove(timeRegistrationEntity);
            await _dbContext.SaveChangesAsync();

            return Mapper.Map<TimeRegistration>(timeRegistrationEntity);
        }

        public async Task<TimeRegistration> UpdateAsync(int id, TimeRegistration timeRegistration, CancellationToken cancellationToken)
        {
            var timeRegistrationEntity = await GetTimeRegistrationById(id, cancellationToken);

            if (timeRegistrationEntity.Accounted)
            {
                throw new Exception("Time registration is already accounted, no changing or deleting is possible");
            }

            if (timeRegistration.ProjectID != timeRegistrationEntity.ProjectID)
            {
                var projectEntity = await _dbContext
                    .Projects
                    .SingleOrDefaultAsync(p => p.Id == timeRegistration.ProjectID);

                timeRegistrationEntity.Project = projectEntity ?? throw new ProjectNotFoundException(timeRegistrationEntity.ProjectID);
                timeRegistrationEntity.ProjectID = projectEntity.Id;
            }

            timeRegistrationEntity.Date = timeRegistration.Date;
            timeRegistrationEntity.HoursWorked = timeRegistration.HoursWorked;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return timeRegistration;
        }

        public async Task<IEnumerable<TimeRegistration>> ReadAllAsync(CancellationToken cancellationToken)
        {
            var timeRegistrationEntities = await _dbContext.TimeRegistrations
                .Include("Project")
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TimeRegistration>>(timeRegistrationEntities);
        }

        public async Task<TimeRegistration> ReadSingleAsync(int id, CancellationToken cancellationToken)
        {
            var timeRegistrationEntity = await GetTimeRegistrationById(id, cancellationToken);

            return Mapper.Map<TimeRegistration>(timeRegistrationEntity);
        }

        private async Task<TimeRegistrationEntity> GetTimeRegistrationById(int id, CancellationToken cancellationToken)
        {
            var timeRegistrationEntity = await _dbContext
                .TimeRegistrations
                .SingleOrDefaultAsync(t => t.ID == id, cancellationToken);

            if (timeRegistrationEntity == null)
            {
                throw new TimeRegistrationNotFoundException(id);
            }

            return timeRegistrationEntity;
        }
    }
}
