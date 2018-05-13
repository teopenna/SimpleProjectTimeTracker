let apiUrl = 'http://localhost:8080/api/';

if (process.env.NODE_ENV === 'production') {
    apiUrl = 'https://myapi.com/api/';
}
export const apiConfiguration = {
    url: apiUrl
};