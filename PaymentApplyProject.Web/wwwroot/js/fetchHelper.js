const fetchHelper = {
    send: (url, method, data) => {
        let fetchOptions = {
            method: method,
            headers: {
                "Content-Type": "application/json; charset = utf-8;"
            }
        };

        if (data)
            fetchOptions.body = JSON.stringify(data);

        return fetch(url, fetchOptions)
            .then(response => response.json());
    },
    sendText: (url, method, data) => {
        let fetchOptions = {
            method: method,
            headers: {
                "Content-Type": "application/json; charset = utf-8;"
            }
        };

        if (data)
            fetchOptions.body = JSON.stringify(data);

        return fetch(url, fetchOptions)
            .then(response => response.text());
    },
}