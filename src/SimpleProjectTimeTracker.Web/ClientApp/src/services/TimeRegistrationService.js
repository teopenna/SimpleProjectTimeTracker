import RestUtilities from './RestUtilities';
import { apiConfiguration } from '../configuration';

function fetchAllTimeRegistrations() {
    return RestUtilities.get(`${apiConfiguration.url}timeregistrations`);
}

function fetchTimeRegistration(timeRegistrationId) {
    return RestUtilities.get(`${apiConfiguration.url}timeregistrations/${timeRegistrationId}`);
}

function updateTimeRegistration(timeRegistration) {
    return RestUtilities.put(`${apiConfiguration.url}timeregistrations/${timeRegistration.id}`, timeRegistration);
}

function createTimeRegistration(timeRegistration) {
    return RestUtilities.post(`${apiConfiguration.url}timeregistrations`, timeRegistration);
}

function saveTimeRegistration(timeRegistration) {
    if (timeRegistration.id) {
        return updateTimeRegistration(timeRegistration);
    } else {
        return createTimeRegistration(timeRegistration);
    }
}

function deleteTimeRegistration(timeRegistrationId) {
    return RestUtilities.delete(`${apiConfiguration.url}timeregistrations/${timeRegistrationId}`);
}

export { fetchAllTimeRegistrations, fetchTimeRegistration, saveTimeRegistration, deleteTimeRegistration };