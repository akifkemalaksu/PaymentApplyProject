using MediatR;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Application.Features.UserFeatures.AuthenticateToken;
using PaymentApplyProject.Application.Dtos.UserDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.AuthenticateToken
{
    public class AuthenticateTokenCommandHandler : IRequestHandler<AuthenticateTokenCommand, Response<AuthenticateTokenResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IJwtAuthService _jwtTokenService;
        private readonly ICustomMapper _customMapper;

        public AuthenticateTokenCommandHandler(IPaymentContext paymentContext, IJwtAuthService jwtTokenService, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _jwtTokenService = jwtTokenService;
            _customMapper = customMapper;
        }

        public async Task<Response<AuthenticateTokenResult>> Handle(AuthenticateTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _paymentContext.Users
                .Where(x =>
                    x.Username == request.Username
                    && x.Password == request.Password
                    && !x.Deleted)
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
                return Response<AuthenticateTokenResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.VeriBulunamadi);

            if (!user.Roles.Any(x => x.Id == RoleConstants.CUSTOMER_ID))
                return Response<AuthenticateTokenResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.YetkinizYok);

            return _jwtTokenService.CreateToken(user);
        }
    }
}
