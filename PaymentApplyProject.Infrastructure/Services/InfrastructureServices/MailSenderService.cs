using PaymentApplyProject.Application.Dtos.MailDtos;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Services.InfrastructureServices;
using System.Net;
using System.Net.Mail;

namespace PaymentApplyProject.Infrastructure.Services.InfrastructureServices
{
    public class MailSenderService : IMailSenderService
    {
        private readonly string _email;
        private readonly SmtpClient _smtpClient;

        public MailSenderService(SmtpSettings smtpSettings)
        {
            _email = smtpSettings.Username;

            _smtpClient = new SmtpClient(smtpSettings.Host)
            {
                UseDefaultCredentials = false,
                Port = smtpSettings.Port,
                Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password),
                EnableSsl = true,
            };
        }

        public Task SendAsync(MailDto mail)
        {
            var mailMessage = new MailMessage()
            {
                From = new MailAddress(_email),
                Subject = mail.Subject,
                Body = mail.Body,
                IsBodyHtml = mail.IsBodyHtml,
            };

            if (mail.Attachments != null && mail.Attachments.Any())
                Parallel.ForEach(mail.Attachments, mailMessage.Attachments.Add);

            Parallel.ForEach(mail.Recipients, mailMessage.To.Add);

            return _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
