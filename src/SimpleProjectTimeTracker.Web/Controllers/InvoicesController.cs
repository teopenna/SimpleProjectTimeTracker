using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleProjectTimeTracker.Web.Exceptions;
using SimpleProjectTimeTracker.Web.Models;
using SimpleProjectTimeTracker.Web.Services;

namespace SimpleProjectTimeTracker.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/invoices")]
    public class InvoicesController : Controller
    {
        private IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService ?? throw new ArgumentNullException(nameof(invoiceService), "A valid Invoice service must be supplied");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IEnumerable<TimeRegistration> timeRegistrations, CancellationToken cancellationToken)
        {
            var createdInvoice = await _invoiceService.CreateAsync(cancellationToken);

            return CreatedAtAction(nameof(Create), createdInvoice);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                var invoice = await _invoiceService.ReadSingleAsync(id, cancellationToken);
                return Ok(invoice);
            }
            catch (InvoiceNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetInvoices(CancellationToken cancellationToken)
        {
            var invoices = await _invoiceService.ReadAllAsync(cancellationToken);
            return Ok(invoices);
        }

        //public async Task<IActionResult> Index([FromServices])
    }
}