using System;
using System.Runtime.Serialization;

namespace SimpleProjectTimeTracker.Web.Exceptions
{
    public class SimpleProjectTimeTrackerException : Exception
    {
        public SimpleProjectTimeTrackerException()
        {
        }

        public SimpleProjectTimeTrackerException(string message) : base(message)
        {
        }

        public SimpleProjectTimeTrackerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SimpleProjectTimeTrackerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
