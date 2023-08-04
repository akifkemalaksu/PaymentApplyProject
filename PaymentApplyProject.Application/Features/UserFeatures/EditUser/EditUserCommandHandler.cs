using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.UserFeatures.EditUser;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public EditUserCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _paymentContext.Users
                .Include(x => x.UserCompanies)
                .FirstOrDefaultAsync(x =>
                x.Id == request.Id
                && !x.Deleted
            , cancellationToken);

            if (user == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, string.Format(Messages.VeriBulunamadi, nameof(User)));

            var isExistSameUsername = await _paymentContext.Users.AnyAsync(x =>
                x.Username == request.Username
                && x.Id != request.Id
                && !x.Deleted
            , cancellationToken);
            if (isExistSameUsername)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AyniKullaniciAdinaSahipKayitVar);

            var isExistSameEmail = await _paymentContext.Users.AnyAsync(x =>
                x.Email == request.Email
                && x.Id != request.Id
                && !x.Deleted
            , cancellationToken);
            if (isExistSameEmail)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AyniMailAdresineSahipKayitVar);

            user.Name = request.Name;
            user.Surname = request.Surname;
            user.Username = request.Username;
            user.Email = request.Email;
            user.Active = request.Active;
            user.UserCompanies = request.Companies.Select(x => new UserCompany
            {
                CompanyId = x
            })
            .ToList();

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
