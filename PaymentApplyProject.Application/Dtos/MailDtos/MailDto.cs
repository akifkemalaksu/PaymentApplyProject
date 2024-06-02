using System.Net.Mail;

namespace PaymentApplyProject.Application.Dtos.MailDtos
{
    public class MailDto
    {
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public required IEnumerable<string> Recipients { get; set; }
        public IEnumerable<Attachment> Attachments { get; set; }
    }
}
