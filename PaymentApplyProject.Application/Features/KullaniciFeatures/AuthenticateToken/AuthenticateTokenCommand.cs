using MediatR;
using PaymentApplyProject.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.KullaniciFeatures.AuthenticateToken;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.AuthenticateToken
{
    public class AuthenticateTokenCommand : IRequest<Response<AuthenticateTokenResult>>
    {
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
    }
}
