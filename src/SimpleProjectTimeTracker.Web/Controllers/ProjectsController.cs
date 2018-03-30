using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleProjectTimeTracker.Web.Services;

namespace SimpleProjectTimeTracker.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        private IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService), "A valid Project service must be supplied.");
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects(CancellationToken cancellationToken)
        {
            var projects = await _projectService.ReadAllAsync(cancellationToken);
            return Ok(projects);
        }
    }
}