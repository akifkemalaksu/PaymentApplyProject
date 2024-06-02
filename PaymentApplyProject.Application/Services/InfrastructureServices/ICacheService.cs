namespace PaymentApplyProject.Application.Services.InfrastructureServices
{
    public interface ICacheService
    {
        public T Get<T>(string key);
        public void Set<T>(string key, T value);
        public void Set<T>(string key, T value, TimeSpan timeSpan);
        public void Remove(string key);
    }
}
