using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.BankDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.BankFeatures.GetBanks
{
    public class GetBanksQueryHandler : IRequestHandler<GetBanksQuery, Response<IEnumerable<BankDto>>>
    {
        private readonly IPaymentContext _paymentContext;

        public GetBanksQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<IEnumerable<BankDto>>> Handle(GetBanksQuery request, CancellationToken cancellationToken)
        {
            var banks = await _paymentContext.Banks.Select(x => new BankDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync(cancellationToken);

            return Response<IEnumerable<BankDto>>.Success(System.Net.HttpStatusCode.OK, banks);
        }
    }
}
