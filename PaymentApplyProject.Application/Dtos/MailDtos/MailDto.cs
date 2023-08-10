using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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
