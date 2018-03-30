using System;

namespace SimpleProjectTimeTracker.Web.Models
{
    public class TimeRegistrationEntity
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
        public DateTime Date { get; set; }
        public decimal HoursWorked { get; set; }
        public bool Accounted { get; set; }
    }
}
