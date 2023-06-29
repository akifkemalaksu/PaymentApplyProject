using PaymentApplyProject.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.KullaniciFeatures.AuthenticateToken;

namespace PaymentApplyProject.Application.Services
{
    public interface IJwtAuthService
    {
        public Response<AuthenticateTokenResult> CreateToken(KullaniciDto kullaniciDto);
    }
}
