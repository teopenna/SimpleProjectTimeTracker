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

        public async Task<Byte[]> CreateAsync(CancellationToken cancellationToken)
        {
            var timeRegistrations = await _dbContext
                .TimeRegistrations
                .Where(t => !t.Accounted)
                .AsNoTracking()
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

            foreach (var customer in timeRegistrationCustomers)
            {
                var projectTimeRegistrations = from tr in timeRegistrations
                                               where tr.Project.CustomerName == customer.CustomerName
                                               orderby tr.ProjectId
                                               select new InvoiceDetailEntity
                                               {
                                                   ProjectName = tr.Project.Name,
                                                   HourlyRate = tr.Project.HourlyRate,
                                                   Date = tr.Date,
                                                   HoursWorked = tr.HoursWorked,
                                                   Amount = Math.Round(tr.HoursWorked * tr.Project.HourlyRate)
                                               };

                var totalAmount = projectTimeRegistrations
                    .Sum(p => p.Amount);

                var invoice = new InvoiceEntity
                {
                    CustomerName = customer.CustomerName,
                    Date = DateTime.Now.Date,
                    Details = projectTimeRegistrations.ToList(),
                    VatPercentage = customer.VatPercentage,
                    NetAmount = totalAmount,
                    VatAmount = Math.Round((totalAmount * customer.VatPercentage / 100), 2)
                };

                _dbContext.Invoices.Add(invoice);
                await _dbContext.SaveChangesAsync(CancellationToken.None);

                
            }

            return new Byte[] { };
        }

        public async Task<IEnumerable<Invoice>> ReadAllAsync(CancellationToken cancellationToken)
        {
            var invoiceEntities = await _dbContext.Invoices
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
