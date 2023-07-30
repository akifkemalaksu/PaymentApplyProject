using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Application.Features.UserFeatures.GetUserByIdAndRole;
using PaymentApplyProject.Application.Dtos.UserDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.GetUserByIdAndRole
{
    public class GetUserByIdAndRoleQueryHandler : IRequestHandler<GetUserByIdAndRoleQuery, Response<GetUserByIdAndRoleResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _customMapper;

        public GetUserByIdAndRoleQueryHandler(IPaymentContext paymentContext, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _customMapper = customMapper;
        }

        public async Task<Response<GetUserByIdAndRoleResult>> Handle(GetUserByIdAndRoleQuery request, CancellationToken cancellationToken)
        {
            var user = await _paymentContext.Users
                .Where(x =>
                    x.Id == request.Id
                    && x.UserRoles.Any(ky =>
                        ky.RoleId == request.RoleId
                        && !ky.Delete
                    )
                    && !x.Delete)
                .Select(x => new GetUserByIdAndRoleResult
                {
                    Id = request.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email,
                    Username = x.Username,
                    Active = x.Active,
                    Companies = x.UserCompanies.Where(x => !x.Delete).Select(kf => new CompanyDto
                    {
                        Name = kf.Company.Name,
                        Id = kf.CompanyId
                    })
                    .ToList(),
                    Roles = x.UserRoles.Where(x => !x.Delete).Select(ky => new RoleDto
                    {
                        Name = ky.Role.Name,
                        Id = ky.RoleId
                    })
                    .ToList(),
                }).FirstOrDefaultAsync();

            if (user is null)
                return Response<GetUserByIdAndRoleResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.VeriBulunamadi);

            return Response<GetUserByIdAndRoleResult>.Success(System.Net.HttpStatusCode.OK, user);
        }
    }
}
