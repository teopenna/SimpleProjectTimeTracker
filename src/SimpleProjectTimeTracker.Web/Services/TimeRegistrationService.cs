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

            var a = await _dbContext.TimeRegistrations.AddAsync(timeRegistrationEntity, cancellationToken);
            var i = await _dbContext.SaveChangesAsync(cancellationToken);

            return Mapper.Map<TimeRegistration>(a.Entity);
        }

        public async Task<TimeRegistration> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<TimeRegistration> UpdateAsync(int id, TimeRegistration timeRegistration, CancellationToken cancellationToken)
        {
            var timeRegistrationEntity = await _dbContext
                .TimeRegistrations
                .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
            
            if (timeRegistrationEntity == null)
            {
                throw new TimeRegistrationNotFoundException(id);
            }

            if (timeRegistration.ProjectId != timeRegistrationEntity.ProjectId)
            {
                var projectEntity = await _dbContext
                    .Projects
                    .SingleOrDefaultAsync(p => p.Id == timeRegistration.ProjectId);

                // TODO: Throw exception if project was not found

                timeRegistrationEntity.Project = projectEntity;
                timeRegistrationEntity.ProjectId = projectEntity.Id;
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
            throw new NotImplementedException();
        }
    }
}
