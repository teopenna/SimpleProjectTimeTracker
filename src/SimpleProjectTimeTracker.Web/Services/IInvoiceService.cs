using SimpleProjectTimeTracker.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleProjectTimeTracker.Web.Services
{
    public interface IInvoiceService
    {
        Task<Byte[]> CreateAsync(CancellationToken cancellationToken);
        Task<Invoice> ReadSingleAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Invoice>> ReadAllAsync(CancellationToken cancellationToken);
    }
}
