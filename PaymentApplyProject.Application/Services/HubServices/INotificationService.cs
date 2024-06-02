namespace PaymentApplyProject.Application.Services.HubServices
{
    public interface INotificationService
    {
        public Task CreateNotification(object data, CancellationToken cancellationToken = default);
        public Task CreateNotificationToSpecificUsers(IEnumerable<string> usernames, object data, CancellationToken cancellationToken = default);
    }
}
