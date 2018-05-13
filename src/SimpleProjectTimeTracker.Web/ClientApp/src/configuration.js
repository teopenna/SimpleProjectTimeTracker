let apiUrl = 'http://localhost:8080/api/';

if (process.env.NODE_ENV === 'production') {
    apiUrl = 'http://simpleprojecttimetrackerweb.azurewebsites.net/api/';
}
export const apiConfiguration = {
    url: apiUrl
};