using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.UserFeatures.EditUser
{
    public class EditUserCommand : IRequest<Response<NoContent>>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Active { get; set; }
        public IEnumerable<short> Companies { get; set; }

        public EditUserCommand()
        {
            Companies = new List<short>();
        }
    }
}
