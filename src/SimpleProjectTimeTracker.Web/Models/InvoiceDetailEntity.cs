using System;

namespace SimpleProjectTimeTracker.Web.Models
{
    public class InvoiceDetailEntity
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public string ProjectName { get; set; }
        public DateTime Date { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Amount { get; set; }
    }
}
