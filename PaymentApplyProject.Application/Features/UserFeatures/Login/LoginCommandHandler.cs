using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.UserFeatures.Login;
using PaymentApplyProject.Application.Dtos.UserDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Services.InfrastructureServices;

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
                    && !x.Deleted)
                .Select(x => new UserDto
                {
                    Name = x.Name,
                    Email = x.Email,
                    Username = x.Username,
                    Surname = x.Surname,
                    Id = x.Id,
                    Companies = x.UserCompanies.Where(x => !x.Deleted).Select(kf => new CompanyDto
                    {
                        Name = kf.Company.Name,
                        Id = kf.CompanyId
                    }),
                    Roles = x.UserRoles.Where(x => !x.Deleted).Select(ky => new RoleDto
                    {
                        Name = ky.Role.Name,
                        Id = ky.RoleId
                    }),
                    Active = x.Active
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.OK, Messages.GirisBilgisiYanlis);

            if (!user.Active)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.OK, Messages.KullaniciAktifDegil);

            if (!(user.DoesHaveAdminRole() || user.DoesHaveUserRole()))
                return Response<NoContent>.Error(System.Net.HttpStatusCode.OK, Messages.YetkinizYok);


            await _cookieTokenService.SignInAsync(user, request.RememberMe);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK);
        }
    }
}
