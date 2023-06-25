using MediatR;
using PaymentApplyProject.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.Authenticate
{
    public class AuthenticateCommand : IRequest<Response<AuthenticateResult>>
    {
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
    }
}
