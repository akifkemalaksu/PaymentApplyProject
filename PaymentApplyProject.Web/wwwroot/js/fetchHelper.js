const fetchHelper = {
    send: (url, method, { data = null,errorFunc = null }) => {
        let fetchOptions = {
            method: method,
            headers: {
                "Content-Type": "application/json; charset = utf-8;"
            }
        };

        fetchOptions.body = JSON.stringify(data);

        return fetch(url, fetchOptions)
            .then(response => response.json());
    }
}