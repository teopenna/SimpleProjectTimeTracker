namespace SimpleProjectTimeTracker.Web.Exceptions
{
    public class InvoiceNotFoundException : SimpleProjectTimeTrackerException
    {
        public InvoiceNotFoundException(int invoiceId)
            : base($"Invoice with Id {invoiceId} was not found.")
        {

        }
    }
}
