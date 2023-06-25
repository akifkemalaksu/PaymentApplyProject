using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Dtos.GrandPashaBetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Services
{
    public interface IGrandPashaBetService
    {
        public Task<Response<CreateDepositResponseDto>> CreateDepositAsync(CreateDepositDto createDeposit);
        public Task<Response<NoContent>> ApplyDepositAsync();
        public Task<Response<NoContent>> ApplyWithdrawAsync();
        public Task<Response<NoContent>> CancelDepositAsync();
        public Task<Response<NoContent>> CancelWithdrawAsync();
    }
}
