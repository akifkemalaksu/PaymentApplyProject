using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.UserFeatures.AddUser
{
    public class AddUserCommand : IRequest<Response<NoContent>>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<short> Companies { get; set; }
        public short RoleId { get; set; }

        public AddUserCommand()
        {
            Companies = new List<short>();
        }
    }
}
