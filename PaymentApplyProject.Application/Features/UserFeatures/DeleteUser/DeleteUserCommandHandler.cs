using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Localizations;

namespace PaymentApplyProject.Application.Features.UserFeatures.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public DeleteUserCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _paymentContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Deleted, cancellationToken);

            if (user == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.KullaniciBulunamadi);

            _paymentContext.Users.Remove(user);

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
