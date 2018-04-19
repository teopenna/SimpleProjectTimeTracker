using System;
using System.Collections.Generic;

namespace SimpleProjectTimeTracker.Web.Models
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerName { get; set; }
        public DateTime DueDate { get; set; }
        public decimal HourlyRate { get; set; }
        public List<TimeRegistrationEntity> TimeRegistrations { get; set; }
    }
}
