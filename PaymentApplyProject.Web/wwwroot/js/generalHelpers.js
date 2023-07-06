const httpMethods = {
    get: 'GET',
    post: 'POST',
    put: 'PUT',
    delete: 'DELETE'
}

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

        KTApp.blockPage();
        return fetch(url, fetchOptions)
            .then(response => {
                KTApp.unblockPage();
                return response.json()
            });
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

const formatter = {
    toMoney: (price) => price ? new Intl.NumberFormat('tr-TR', {
        style: 'currency',
        currency: 'TRY',
    }).format(price) : "",
    toGoodDate: (date) => date ? moment(date).format("DD.MM.YY HH:mm") : ""
}

const parser = {
    moneyToFloat: money => money ? parseFloat(money.replaceAll('.', '').replaceAll(',', '.')) : null
}

const icons = {
    warning: 'warning',
    error: 'error',
    success: 'success',
    info: 'info',
    question: 'question'
}
const swal = {
    basic: (title, text, icon) => Swal.fire({
        title: title,
        text: text,
        type: icon,
        allowOutsideClick: false,
        allowEscapeKey: false,
        allowEnterKey: false,
        confirmButtonText: 'Tamam'
    }),
    basicWithOneButtonFunc: (title, text, icon, func) => Swal.fire({
        title: title,
        text: text,
        type: icon,
        allowOutsideClick: false,
        allowEscapeKey: false,
        allowEnterKey: false,
        confirmButtonText: 'Tamam'
    }).then(func),
    basicWithTwoButtonFunc: (title, text, icon, func) => Swal.fire({
        title: title,
        text: text,
        type: icon,
        allowOutsideClick: false,
        allowEscapeKey: false,
        allowEnterKey: false,
        showCancelButton: true,
        confirmButtonText: 'Tamam',
        cancelButtonText: 'Kapat'
    }).then(func)
}