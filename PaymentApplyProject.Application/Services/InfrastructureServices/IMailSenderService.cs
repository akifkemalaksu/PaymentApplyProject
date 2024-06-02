using PaymentApplyProject.Application.Dtos.MailDtos;

namespace PaymentApplyProject.Application.Services.InfrastructureServices
{
    public interface IMailSenderService
    {
        public Task SendAsync(MailDto mail);
    }
}
