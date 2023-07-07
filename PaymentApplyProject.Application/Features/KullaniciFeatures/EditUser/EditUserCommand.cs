using MediatR;
using PaymentApplyProject.Application.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.EditUser
{
    public class EditUserCommand : IRequest<Response<NoContent>>
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Email { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public bool AktifMi { get; set; }
        public IEnumerable<short> Firmalar { get; set; }

        public EditUserCommand()
        {
            Firmalar = new List<short>();
        }
    }
}
