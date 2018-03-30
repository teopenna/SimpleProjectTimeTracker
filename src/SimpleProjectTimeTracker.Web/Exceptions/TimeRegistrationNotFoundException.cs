namespace SimpleProjectTimeTracker.Web.Exceptions
{
    public class TimeRegistrationNotFoundException : SimpleProjectTimeTrackerException
    {
        public TimeRegistrationNotFoundException(string projectName)
            : base($"Time registration for project {projectName} was not found.")
        {

        }

        public TimeRegistrationNotFoundException(int timeRegistrationId)
            : base($"Time registration with Id {timeRegistrationId} was not found.")
        {

        }
    }
}
