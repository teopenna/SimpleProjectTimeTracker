namespace SimpleProjectTimeTracker.Web.Exceptions
{
    public class ProjectNotFoundException : SimpleProjectTimeTrackerException
    {
        public ProjectNotFoundException(int projectId)
            : base($"Project with Id {projectId} was not found.")
        {

        }
    }
}
