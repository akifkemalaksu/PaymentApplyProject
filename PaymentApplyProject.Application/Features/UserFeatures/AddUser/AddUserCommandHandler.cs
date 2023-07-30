using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Helpers;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.UserFeatures.AddUser;

namespace PaymentApplyProject.Application.Features.UserFeatures.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _customMapper;

        public AddUserCommandHandler(IPaymentContext paymentContext, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _customMapper = customMapper;
        }

        public async Task<Response<NoContent>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var isExistSameUsername = await _paymentContext.Users.AnyAsync(x => x.Username == request.Username && !x.Delete, cancellationToken);
            if (isExistSameUsername)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AyniKullaniciAdinaSahipKayitVar);

            var isExistSameEmail = await _paymentContext.Users.AnyAsync(x => x.Email == request.Email && !x.Delete, cancellationToken);
            if (isExistSameEmail)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AyniMailAdresineSahipKayitVar);

            var password = PasswordGenerator.GeneratePassword();
            var user = _customMapper.Map<User>(request);
            user.Password = password;
            user.Active = true;
            user.UserCompanies = request.Companies.Select(x => new UserCompany
            {
                CompanyId = x,
            }).ToList();
            user.UserRoles = new List<UserRole> { new()
            {
                RoleId = RoleConstants.USER_ID
            }};

            await _paymentContext.Users.AddAsync(user, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
