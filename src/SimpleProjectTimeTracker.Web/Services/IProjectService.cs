using SimpleProjectTimeTracker.Web.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleProjectTimeTracker.Web.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> ReadAllAsync(CancellationToken cancellationToken);
    }
}
