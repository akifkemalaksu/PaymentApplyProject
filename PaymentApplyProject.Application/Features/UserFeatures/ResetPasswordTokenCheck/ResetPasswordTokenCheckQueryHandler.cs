using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Services.InfrastructureServices;

namespace PaymentApplyProject.Application.Features.UserFeatures.ResetPasswordTokenCheck
{
    public class ResetPasswordTokenCheckQueryHandler : IRequestHandler<ResetPasswordTokenCheckQuery, Response<ResetPasswordTokenCheckResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICacheService _cacheService;

        public ResetPasswordTokenCheckQueryHandler(IPaymentContext paymentContext, ICacheService cacheService)
        {
            _paymentContext = paymentContext;
            _cacheService = cacheService;
        }

        public Response<ResetPasswordTokenCheckResult> Handle(ResetPasswordTokenCheckQuery request, CancellationToken cancellationToken)
        {
            var userId = _cacheService.Get<int?>(request.Token);
            if (userId == null)
                return Response<ResetPasswordTokenCheckResult>.Error(System.Net.HttpStatusCode.OK, Messages.LinkSuresiDolmusVeyaKullanilmis);

            var resetPasswordTokenCheckResult = new ResetPasswordTokenCheckResult
            {
                Token = request.Token
            };
            return Response<ResetPasswordTokenCheckResult>.Success(System.Net.HttpStatusCode.OK, resetPasswordTokenCheckResult);
        }

        Task<Response<ResetPasswordTokenCheckResult>> IRequestHandler<ResetPasswordTokenCheckQuery, Response<ResetPasswordTokenCheckResult>>.Handle(ResetPasswordTokenCheckQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
