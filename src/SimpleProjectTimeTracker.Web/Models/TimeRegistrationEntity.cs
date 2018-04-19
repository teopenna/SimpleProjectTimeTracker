using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleProjectTimeTracker.Web.Models
{
    public class TimeRegistrationEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public Project Project { get; set; }
        public DateTime Date { get; set; }
        public decimal HoursWorked { get; set; }
        public bool Accounted { get; set; }
    }
}
