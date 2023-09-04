using PaymentApplyProject.Application.Dtos.MailDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Services.InfrastructureServices
{
    public interface IMailSenderService
    {
        public Task SendAsync(MailDto mail);
    }
}
