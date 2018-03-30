using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleProjectTimeTracker.Web.Models;

namespace SimpleProjectTimeTracker.Web.Services
{
    public class ProjectService : IProjectService
    {
        private SimpleProjectTimeTrackerDbContext _dbContext;

        public async Task<IEnumerable<Project>> ReadAllAsync()
        {
            var projectEntities = await _dbContext
                .Projects
                .ToListAsync();

            return Mapper.Map<IEnumerable<Project>>(projectEntities);
        }
    }
}
