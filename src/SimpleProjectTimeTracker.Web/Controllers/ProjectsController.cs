using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleProjectTimeTracker.Web.Services;

namespace SimpleProjectTimeTracker.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        private SimpleProjectTimeTrackerDbContext _dbContext;

        public ProjectsController(SimpleProjectTimeTrackerDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "A valid DB Context must be supplied.");
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects(CancellationToken cancellationToken)
        {
            var projects = await _dbContext.Projects.ToListAsync();
            return Ok(projects);
        }
    }
}