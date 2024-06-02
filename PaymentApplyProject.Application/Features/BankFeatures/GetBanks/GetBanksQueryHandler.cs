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
        private readonly ICustomMapper _mapper;

        public GetBanksQueryHandler(IPaymentContext paymentContext, ICustomMapper mapper)
        {
            _paymentContext = paymentContext;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<BankDto>>> Handle(GetBanksQuery request, CancellationToken cancellationToken)
        {
            var banksQuery = _paymentContext.Banks.AsQueryable();

            var banks = await _mapper.QueryMap<BankDto>(banksQuery)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return Response<IEnumerable<BankDto>>.Success(System.Net.HttpStatusCode.OK, banks);
        }
    }
}
