using PaymentApplyProject.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Services
{
    public interface IGrandPashaBetService
    {
        public Response<NoContent> ApplyPayment();
        public Response<NoContent> CancelPayment();
    }
}
