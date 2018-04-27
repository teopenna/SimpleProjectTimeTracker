using System;
using System.Collections.Generic;

namespace SimpleProjectTimeTracker.Web.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Number
        {
            get
            {
                return Id > 0 ? Id.ToString().PadLeft(8, '0') : string.Empty;
            }

        }
        public string CustomerName { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount
        {
            get
            {
                return NetAmount + VatAmount;
            }
        }
        public decimal VatAmount
        {
            get
            {
                return Math.Round((NetAmount * VatPercentage / 100), 2);
            }
        }
        public decimal VatPercentage { get; set; }
        public List<InvoiceDetail> Details { get; set; }
    }
}
