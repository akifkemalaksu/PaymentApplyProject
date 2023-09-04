using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Services.HubServices
{
    public interface IDepositPaymentHubRedirectionService
    {
        Task Redirect(string redirectUrl, string hash, CancellationToken cancellationToken = default);
    }
}
