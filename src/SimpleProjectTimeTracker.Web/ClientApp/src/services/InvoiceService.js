import RestUtilities from './RestUtilities';

class InvoiceService {
    fetchAll() {
        return RestUtilities.get('http://localhost:8080/api/invoices');
    }

    createInvoices() {
        return RestUtilities.post('http://localhost:8080/api/invoices');
    }
};

export default InvoiceService;