using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Services.InfrastructureServices;

namespace PaymentApplyProject.Application.Features.UserFeatures.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICacheService _cacheService;
        public ResetPasswordCommandHandler(IPaymentContext paymentContext, ICacheService cacheService)
        {
            _paymentContext = paymentContext;
            _cacheService = cacheService;
        }

        public async Task<Response<NoContent>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _cacheService.Get<int?>(request.Token);
            if (userId == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.OK, Messages.LinkSuresiDolmusVeyaKullanilmis);

            var user = await _paymentContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            user.Password = request.Password;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            _cacheService.Remove(request.Token);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.SifreDegistirmeIslemiBasarili);
        }
    }
}
