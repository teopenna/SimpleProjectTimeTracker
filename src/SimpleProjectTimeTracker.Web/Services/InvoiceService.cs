using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleProjectTimeTracker.Web.Models;

namespace SimpleProjectTimeTracker.Web.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly SimpleProjectTimeTrackerDbContext _dbContext;
        private readonly IMapper _mapper;

        public InvoiceService(SimpleProjectTimeTrackerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Invoice>> CreateAsync(CancellationToken cancellationToken)
        {
            var timeRegistrations = await _dbContext
                .TimeRegistrations
                .Include("Project")
                .Where(t => !t.Accounted)
                .ToListAsync();

            var timeRegistrationCustomers = from tr in timeRegistrations
                                            group tr by new
                                            {
                                                tr.Project.CustomerName,
                                                tr.Project.VatPercentage
                                            } into CustomerRegistrations
                                            select new
                                            {
                                                CustomerRegistrations.Key.CustomerName,
                                                CustomerRegistrations.Key.VatPercentage
                                            };

            var invoices = new List<Invoice>();

            foreach (var customer in timeRegistrationCustomers)
            {
                var invoiceDetails = from tr in timeRegistrations
                                     where tr.Project.CustomerName == customer.CustomerName
                                     orderby tr.ProjectId
                                     select new InvoiceDetailEntity
                                     {
                                         ProjectName = tr.Project.Name,
                                         HourlyRate = tr.Project.HourlyRate,
                                         Date = tr.Date,
                                         HoursWorked = tr.HoursWorked,
                                         Amount = Math.Round(tr.HoursWorked * tr.Project.HourlyRate),
                                         TimeRegistrationId = tr.Id
                                     };

                var timeRegistrationsToUpdate = from tr in timeRegistrations
                                                where tr.Project.CustomerName == customer.CustomerName
                                                select tr;

                foreach (var timeRegistrationToUpdate in timeRegistrationsToUpdate)
                {
                    timeRegistrationToUpdate.Accounted = true;
                }

                var totalAmount = invoiceDetails
                    .Sum(p => p.Amount);

                var invoice = new InvoiceEntity
                {
                    CustomerName = customer.CustomerName,
                    Date = DateTime.Now.Date,
                    Details = invoiceDetails.ToList(),
                    VatPercentage = customer.VatPercentage,
                    NetAmount = totalAmount,
                    VatAmount = Math.Round((totalAmount * customer.VatPercentage / 100), 2)
                };

                _dbContext.Invoices.Add(invoice);

                await _dbContext.SaveChangesAsync(CancellationToken.None);

                var createdInvoice = _mapper.Map<Invoice>(invoice);
                invoices.Add(createdInvoice);
            }

            return invoices;
        }

        public async Task<IEnumerable<Invoice>> ReadAllAsync(CancellationToken cancellationToken)
        {
            var invoiceEntities = await _dbContext.Invoices
                .Include("Details")
                .OrderByDescending(i => i.Date)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Invoice>>(invoiceEntities);
        }

        public async Task<Invoice> ReadSingleAsync(int id, CancellationToken cancellationToken)
        {
            var invoice = await _dbContext
                .Invoices
                .FirstOrDefaultAsync(i => i.Id == id);

            return _mapper.Map<Invoice>(invoice);
        }
    }
}
