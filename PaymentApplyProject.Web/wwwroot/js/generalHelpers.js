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

const formatter = {
    toMoney: new Intl.NumberFormat('tr-TR', {
        style: 'currency',
        currency: 'TRY',
    })
}

const parser = {
    moneyToFloat: (money) => parseFloat(money.replaceAll('.', '').replaceAll(',', '.'))
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