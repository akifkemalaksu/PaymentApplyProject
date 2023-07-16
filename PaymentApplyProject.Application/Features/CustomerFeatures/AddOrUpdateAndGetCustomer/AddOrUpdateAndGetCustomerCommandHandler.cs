using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Application.Features.CustomerFeatures.AddOrUpdateAndGetCustomer;

namespace PaymentApplyProject.Application.Features.CustomerFeatures.AddOrUpdateAndGetCustomer
{
    public class AddOrUpdateAndGetCustomerCommandHandler : IRequestHandler<AddOrUpdateAndGetCustomerCommand, Response<AddOrUpdateAndGetCustomerResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public AddOrUpdateAndGetCustomerCommandHandler(IPaymentContext paymentContext, IAuthenticatedUserService authenticatedUserService)
        {
            _paymentContext = paymentContext;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<Response<AddOrUpdateAndGetCustomerResult>> Handle(AddOrUpdateAndGetCustomerCommand request, CancellationToken cancellationToken)
        {
            var userInfos = _authenticatedUserService.GetUserInfo();
            if (userInfos == null || userInfos.Companies.Any())
                return Response<AddOrUpdateAndGetCustomerResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            var company = userInfos.Companies.First();

            var customer = await _paymentContext.Customers.FirstOrDefaultAsync(x => x.CompanyId == company.Id && x.Username == request.Username && !x.Delete, cancellationToken);
            if (customer == null)
            {
                customer = new()
                {
                    Username = request.Username,
                    Name = request.Name,
                    Surname = request.Surname,
                    CompanyId = company.Id,
                    Active = true
                };
                await _paymentContext.Customers.AddAsync(customer, cancellationToken);
                await _paymentContext.SaveChangesAsync(cancellationToken);
            }
            else if (!customer.Active)
                return Response<AddOrUpdateAndGetCustomerResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.PassiveCustomer);
            else if (customer.Name != request.Name || customer.Surname != request.Surname)
            {
                customer.Name = request.Name;
                customer.Surname = request.Surname;
                await _paymentContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                /*
                 * todo: burada sadece para yatırma için kontrol yapıldı 
                 * ancak bu işlem genel bir işlem olarak kullanılabilir 
                 * bu para yatırma var mı kontrolünü farklı bir feature olarak tanımlanabilir
                 * (**gereksiz işlem olmaması için yeni eklenen müşteriye kontrol yapılmaması lazım)
                 */
                var isExistsParaYatirma = await _paymentContext.Deposits.CountAsync(x =>
                    x.CustomerId == customer.Id
                    && x.DepositStatusId == DepositStatusConstants.BEKLIYOR
                    && !x.Delete, cancellationToken) > 0;
                if (isExistsParaYatirma)
                    return Response<AddOrUpdateAndGetCustomerResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsPendingTransaction);
            }

            var addOrUpdateAndGetMusteriResult = new AddOrUpdateAndGetCustomerResult { CustomerId = customer.Id };
            return Response<AddOrUpdateAndGetCustomerResult>.Success(System.Net.HttpStatusCode.OK, addOrUpdateAndGetMusteriResult);
        }
    }
}
