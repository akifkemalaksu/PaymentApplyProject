using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.UserFeatures.DeleteUser
{
    public class DeleteUserCommand : IRequest<Response<NoContent>>, ITransactional
    {
        public int Id { get; set; }
    }
}
