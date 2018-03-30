using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleProjectTimeTracker.Web.Services;

namespace SimpleProjectTimeTracker.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        private ITimeRegistrationService _projectService;

        public ProjectsController(ITimeRegistrationService projectService)
        {
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService), "A valid Project service must be supplied.");
        }

        [HttpGet]
        [Route("api/timeregistrations")]
        public IActionResult GetProjectsWithTimeRegistrations()
        {
            throw new NotImplementedException();
        }
    }
}