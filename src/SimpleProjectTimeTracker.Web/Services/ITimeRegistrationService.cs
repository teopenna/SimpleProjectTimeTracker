using SimpleProjectTimeTracker.Web.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleProjectTimeTracker.Web.Services
{
    public interface ITimeRegistrationService
    {
        Task<IEnumerable<TimeRegistration>> ReadAllAsync(CancellationToken cancellationToken);
        Task<TimeRegistration> ReadSingleAsync(int id, CancellationToken cancellationToken);
        Task<TimeRegistration> CreateAsync(TimeRegistration timeRegistration, CancellationToken cancellationToken);
        Task<TimeRegistration> UpdateAsync(int id, TimeRegistration timeRegistration, CancellationToken cancellationToken);
        Task<TimeRegistration> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
