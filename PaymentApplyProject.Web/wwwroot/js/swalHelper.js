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
    }).then(func)
}