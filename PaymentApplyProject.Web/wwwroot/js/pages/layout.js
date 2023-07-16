let connection = new signalR.HubConnectionBuilder().withUrl("notification").build();
connection.on('displayNotification', (data) => {
    showNotification(notification)
    console.log("called");
});
connection.start().catch(e => console.error(e.toString())).then(response => console.log("connected"))
console.log(connection);
const showNotification = (notification) => {
    console.log(notification);
    var audio = new Audio('uploads/sounds/notification.mp3')
    //toastr.onclick = () => {
    //    window.location.href = notification.path
    //}
    toastr.success(notification.message)
    audio.play().catch((e) => {
        console.error(`Bir hata oluştu: ${e.toString()}`)
    })
}