using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleProjectTimeTracker.Web.Exceptions;
using SimpleProjectTimeTracker.Web.Models;
using SimpleProjectTimeTracker.Web.Services;

namespace SimpleProjectTimeTracker.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/timeregistrations")]
    public class TimeRegistrationsController : Controller
    {
        private ITimeRegistrationService _timeRegistrationService;

        public TimeRegistrationsController(ITimeRegistrationService timeRegistrationService)
        {
            _timeRegistrationService = timeRegistrationService ?? throw new ArgumentNullException(nameof(timeRegistrationService), "A valid Time Registration service must be supplied");
        }

        [HttpGet]
        public async Task<IActionResult> GetTimeRegistrations(CancellationToken cancellationToken)
        {
            var timeRegistrations = await _timeRegistrationService.ReadAllAsync(cancellationToken);
            return Ok(timeRegistrations);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TimeRegistration timeRegistration, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTimeRegistration = await _timeRegistrationService.CreateAsync(timeRegistration, cancellationToken);

            return CreatedAtAction(nameof(Create), createdTimeRegistration);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TimeRegistration timeRegistration, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedTimeRegistration = await _timeRegistrationService.UpdateAsync(id, timeRegistration, cancellationToken);
                return Ok(updatedTimeRegistration);
            }
            catch (TimeRegistrationNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var deletedTimeRegistration = await _timeRegistrationService.DeleteAsync(id, cancellationToken);
                return Ok(deletedTimeRegistration);
            }
            catch (TimeRegistrationNotFoundException)
            {
                return NotFound();
            }
        }
    }
}