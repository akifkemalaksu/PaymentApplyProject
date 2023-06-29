using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.AuthenticateToken
{
    public class AuthenticateTokenResult
    {
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
