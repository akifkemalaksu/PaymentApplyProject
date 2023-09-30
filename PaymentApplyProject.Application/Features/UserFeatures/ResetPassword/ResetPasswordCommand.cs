using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Localizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.UserFeatures.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Response<NoContent>>, ITransactional
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
    }
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public ResetPasswordCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _paymentContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            user.Password = request.Password;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.SifreDegistirmeIslemiBasarili);
        }
    }
}
