using MediatR;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Features.KullaniciFeatures.AuthenticateToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.Login
{
    public class LoginCommand : IRequest<Response<NoContent>>
    {
        public string EmailKullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public bool BeniHatirla { get; set; }
    }
}
