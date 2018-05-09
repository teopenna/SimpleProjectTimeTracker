import RestUtilities from './RestUtilities';

class TimeRegistrationService {
    fetchAll() {
        return RestUtilities.get('http://localhost:8080/api/timeregistrations');
    }

    fetch(timeRegistrationId) {
        return RestUtilities.get(`http://localhost:8080/api/timeregistrations/${timeRegistrationId}`);
    }

    update(timeRegistration) {
        return RestUtilities.put(`http://localhost:8080/api/timeregistrations/${timeRegistration.id}`, timeRegistration);
    }

    create(timeRegistration) {
        return RestUtilities.post('http://localhost:8080/api/timeregistrations', timeRegistration);
    }

    save(timeRegistration) {
        if (timeRegistration.id) {
            return this.update(timeRegistration);
        } else {
            return this.create(timeRegistration);
        }
    }

    delete(timeRegistrationId) {
        return RestUtilities.delete(`http://localhost:8080/api/timeregistrations/${timeRegistrationId}`);
    }
};

export default TimeRegistrationService;