﻿class RestUtilities {
    static get(url) {
        return RestUtilities.request('GET', url);
    }

    static delete(url) {
        return RestUtilities.request('DELETE', url);
    }

    static put(url, data) {
        return RestUtilities.request('PUT', url, data);
    }

    static post(url, data) {
        return RestUtilities.request('POST', url, data);
    }

    static request(method, url, data) {
        let isBadRequest = false;
        let body = data;
        let headers = new Headers();
        
        headers.set('Accept', 'application/json');

        if (data) {
            if ((typeof data === 'object')) {
                headers.set('Content-Type', 'application/json');
                body = JSON.stringify(data);
            } else {
                headers.set('Content-Type', 'application/x-www-form-urlencoded');
            }
        }

        return fetch(url, {
            method: method,
            headers: headers,
            body: body
        }).then((response) => {
            isBadRequest = (response.status === 400);

            let responseContentType = response.headers.get("content-type");
            if (responseContentType && responseContentType.indexOf("application/json") !== -1) {
                return response.json();
            } else {
                return response.text();
            }
        }).then((responseContent) => {
            let response = {
                is_error: isBadRequest,
                error_content: isBadRequest ? responseContent : null,
                content: isBadRequest ? null : responseContent
            };
            return response;
        });
    }
}

export default RestUtilities;