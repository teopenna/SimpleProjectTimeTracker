import RestUtilities from './RestUtilities';
import { apiConfiguration } from '../configuration';

function fetchAllProjects() {
    return RestUtilities.get(`${apiConfiguration.url}projects`);
}

export { fetchAllProjects };