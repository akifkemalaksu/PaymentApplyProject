using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.UserDtos;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Localizations;

namespace PaymentApplyProject.Application.Features.UserFeatures.GetUserByIdAndRole
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _customMapper;

        public GetUserByIdQueryHandler(IPaymentContext paymentContext, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _customMapper = customMapper;
        }

        public async Task<Response<GetUserByIdResult>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _paymentContext.Users
                .Where(x =>
                    x.Id == request.Id
                    && !x.Deleted)
                .Select(x => new GetUserByIdResult
                {
                    Id = request.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email,
                    Username = x.Username,
                    Active = x.Active,
                    Companies = x.UserCompanies.Where(x => !x.Deleted).Select(kf => new CompanyDto
                    {
                        Name = kf.Company.Name,
                        Id = kf.CompanyId
                    })
                    .ToList(),
                    Role = x.UserRoles.Where(x => !x.Deleted).Select(ky => new RoleDto
                    {
                        Name = ky.Role.Name,
                        Id = ky.RoleId
                    })
                    .FirstOrDefault(),
                }).FirstOrDefaultAsync();

            return user is null
                ? Response<GetUserByIdResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.KullaniciBulunamadi)
                : Response<GetUserByIdResult>.Success(System.Net.HttpStatusCode.OK, user);
        }
    }
}
