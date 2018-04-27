using System;
using System.Collections.Generic;

namespace SimpleProjectTimeTracker.Web.Models
{
    public class InvoiceEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; private set; }
        public string CustomerName { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal VatPercentage { get; set; }
        public ICollection<InvoiceDetailEntity> Details { get; set; }
    }
}
