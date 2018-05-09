import RestUtilities from './RestUtilities';

class ProjectService {
    fetchAll() {
        return RestUtilities.get('http://localhost:8080/api/projects');
    }
};

export default ProjectService;