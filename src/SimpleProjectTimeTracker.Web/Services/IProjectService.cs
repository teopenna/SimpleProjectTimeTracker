using SimpleProjectTimeTracker.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleProjectTimeTracker.Web.Services
{
    interface IProjectService
    {
        Task<IEnumerable<Project>> ReadAllAsync();
    }
}
