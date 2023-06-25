using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Features.KullaniciFeatures.Authenticate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Services
{
    public interface IJwtTokenService
    {
        public Response<AuthenticateResult> CreateToken(KullaniciDto kullaniciDto);
    }
}
