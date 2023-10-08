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
const inputTypes = {
    text: 'text',
    email: 'email',
    password: 'password',
    number: 'number',
    tel: 'tel',
    textarea: 'textarea',
    select: 'select',
    radio: 'radio',
    checkbox: 'checkbox',
    file: 'file',
    url: 'url',
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
    }).then(func),
    basicWithTwoButtonAndOneInputFunc: (title, text, icon, inputType, func) => Swal.fire({
        title: title,
        text: text,
        input: inputType,
        type: icon,
        allowOutsideClick: false,
        allowEscapeKey: false,
        allowEnterKey: false,
        showCancelButton: true,
        confirmButtonText: 'Tamam',
        cancelButtonText: 'Kapat'
    }).then(func)
}

const dateRangePickerOptions = {
    autoApply: true,
    buttonClasses: 'btn',
    applyClass: 'btn-primary',
    cancelClass: 'btn-secondary',
    ranges: {
        'Bugün': [moment(), moment().add(1, 'days')],
        'Dün': [moment().subtract(1, 'days'), moment()],
        'Son 7 Gün': [moment().subtract(6, 'days'), moment().add(1, 'days')],
        'Son 30 Gün': [moment().subtract(29, 'days'), moment().add(1, 'days')],
        'Bu Ay': [moment().startOf('month'), moment().endOf('month').add(1, 'days')],
        'Geçen Ay': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month').add(1, 'days')]
    },
    locale: {
        format: "DD.MM.YYYY",
        separator: " - ",
        customRangeLabel: "Özel",
        weekLabel: "H",
        daysOfWeek: [
            "Pa",
            "Pzt",
            "Sa",
            "Ça",
            "Pe",
            "Cu",
            "Cmr"
        ],
        monthNames: [
            "Ocak",
            "Şubat",
            "Mart",
            "Nisan",
            "Mayıs",
            "Haziran",
            "Temmuz",
            "Ağustos",
            "Eylül",
            "Ekim",
            "Kasım",
            "Aralık"
        ],
        firstDay: 0
    },
}