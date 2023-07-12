using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Dtos.KullaniciDtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.UserFeatures.Login;

namespace PaymentApplyProject.Application.Features.UserFeatures.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICookieAuthService _cookieTokenService;
        private readonly ICustomMapper _customMapper;

        public LoginCommandHandler(IPaymentContext paymentContext, ICookieAuthService cookieTokenService, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _cookieTokenService = cookieTokenService;
            _customMapper = customMapper;
        }

        public async Task<Response<NoContent>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _paymentContext
                .Users
                .Where(x =>
                    (x.Email == request.EmailUsername || x.Username == request.EmailUsername)
                    && x.Password == request.Password
                    && !x.Delete)
                .Select(x => new UserDto
                {
                    Name = x.Name,
                    Email = x.Email,
                    Username = x.Username,
                    Surname = x.Surname,
                    Id = x.Id,
                    Companies = x.UserCompanies.Select(kf => new CompanyDto
                    {
                        Name = kf.Company.Name,
                        Id = kf.CompanyId
                    }),
                    Roles = x.UserRoles.Select(ky => new RoleDto
                    {
                        Name = ky.Role.Name,
                        Id = ky.RoleId
                    })
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.OK, Messages.IncorrectLoginInfo);

            if (!user.Roles.Any(x => x.Id == RoleConstants.ADMIN_ID))
                return Response<NoContent>.Error(System.Net.HttpStatusCode.OK, Messages.NotAuthorized);

            await _cookieTokenService.SignInAsync(user, request.RememberMe);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK);
        }
    }
}
