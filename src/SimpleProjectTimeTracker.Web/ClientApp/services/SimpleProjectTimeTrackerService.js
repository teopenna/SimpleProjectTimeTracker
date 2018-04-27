import axios from 'axios';

export default class SimpleProjectTimeTrackerService {
    constructor(baseUrl) {
        const client = axios.create({
            baseURL: baseUrl,
        });

        this.client = client;
    }

    getProjects() {
        return this.client.get('projects');
    }

    getTimeRegistrations() {
        return this.client.get('timeregistrations');
    }

    getSingleTimeRegistration(id) {
        return this.client.get('timeregistrations/' + id);
    }

    saveTimeRegistration(timeRegistration) {
        let isBadRequest = false;
        let isJsonResponse = false;
        let request;
        const requestHeaders = {
            headers: {
                'Content-Type': 'application/json'
            }
        };

        if (timeRegistration.id) {
            // Update
            request = this.client.put('timeregistrations/' + timeRegistration.id, JSON.stringify(timeRegistration), requestHeaders);
        } else {
            // Create
            request = this.client.post('timeregistrations', JSON.stringify(timeRegistration), requestHeaders);
        }

        return request
            .then(response => {
                isBadRequest = (response.status === 400);

                let responseContentType = response.headers['content-type'];
                if (responseContentType && responseContentType.indexOf('application/json') !== -1) {
                    isJsonResponse = true;
                }
                return response.data;
            }).then(responseContent => {
                let response = {
                    isError: isBadRequest,
                    errorContent: isBadRequest ? responseContent : null,
                    content: isBadRequest ? null : responseContent
                };
                return response;
            });
    }

    deleteTimeRegistration(timeRegistration) {
        let isBadRequest = false;
        let isJsonResponse = false;

        return this.client.delete('timeregistrations/' + timeRegistration.id)
            .then(response => {
                isBadRequest = (response.status === 400);

                let responseContentType = response.headers['content-type'];
                if (responseContentType && responseContentType.indexOf('application/json') !== -1) {
                    isJsonResponse = true;
                }
                return response.data;
            }).then(responseContent => {
                let response = {
                    isError: isBadRequest,
                    errorContent: isBadRequest ? responseContent : null,
                    content: isBadRequest ? null : responseContent
                };
                return response;
            });
    }

    getInvoices() {
        return this.client.get('invoices');
    }

    createInvoices() {
        let isOk = true;

        return this.client.post('invoices')
            .then(response => {
                isOk = (response.status === 201);

                return response.data;
            }).then(responseContent => {
                let response = {
                    isError: !isOk,
                    errorContent: !isOk ? responseContent : null,
                    content: isOk ? responseContent : null
                };
                return response;
            });
    }
}