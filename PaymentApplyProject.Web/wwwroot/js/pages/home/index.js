connection.on('displayNotification', (data) => {
    showReport()
});

let showReport = async () => {
    let resultHtml = await fetchHelper.sendText(`/home/PaymentReport`, httpMethods.get);

    let ktContainer = $(".kt-container.report")

    ktContainer.html(resultHtml);
}

showReport()