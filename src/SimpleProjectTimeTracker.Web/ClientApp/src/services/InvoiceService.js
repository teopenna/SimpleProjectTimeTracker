import RestUtilities from './RestUtilities';
import { apiConfiguration } from '../configuration';

function fetchAllInvoices() {
    return RestUtilities.get(`${apiConfiguration.url}invoices`);
}

function createInvoices() {
    return RestUtilities.post(`${apiConfiguration.url}invoices`);
}

export { fetchAllInvoices, createInvoices };