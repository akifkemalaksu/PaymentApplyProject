$.fn.modal.Constructor.prototype._enforceFocus = function () { };

let connection = new signalR.HubConnectionBuilder().withUrl("/notification").build();
connection.on('displayNotification', (data) => {
    showNotification(data)
});
connection.start().then(() => console.log("SignalR connected.")).catch(e => console.error("There is an error with SignalR connection: " + e.toString()));
const showNotification = (notification) => {
    var audio = new Audio('/uploads/sounds/notification.mp3')
    toastr.options.onclick = () => {
        window.location.href = notification.path
    }
    toastr.success(notification.message)
    audio.play().catch((e) => {
        console.error(`There is an error: ${e.toString()}`)
    })
}