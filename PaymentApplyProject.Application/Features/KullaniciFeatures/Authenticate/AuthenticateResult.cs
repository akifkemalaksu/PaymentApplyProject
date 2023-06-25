using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.Authenticate
{
    public class AuthenticateResult
    {
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
