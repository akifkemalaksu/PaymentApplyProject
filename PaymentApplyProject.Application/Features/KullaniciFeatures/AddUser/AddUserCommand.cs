using MediatR;
using PaymentApplyProject.Application.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.AddUser
{
    public class AddUserCommand : IRequest<Response<NoContent>>
    {
        public string KullaniciAdi { get; set; }
        public string Email { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public IEnumerable<short> Firmalar { get; set; }
    }
}
