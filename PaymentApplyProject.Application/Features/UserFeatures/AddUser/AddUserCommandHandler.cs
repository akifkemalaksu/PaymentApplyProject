using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Helpers;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.UserFeatures.AddUser;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.MailDtos;
using PaymentApplyProject.Application.Services.InfrastructureServices;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PaymentApplyProject.Application.Features.UserFeatures.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _customMapper;
        private readonly IMailSenderService _mailSenderService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddUserCommandHandler(IPaymentContext paymentContext, ICustomMapper customMapper, IMailSenderService mailSenderService, IHttpContextAccessor httpContextAccessor)
        {
            _paymentContext = paymentContext;
            _customMapper = customMapper;
            _mailSenderService = mailSenderService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<NoContent>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var isExistSameUsername = await _paymentContext.Users.AnyAsync(x => x.Username == request.Username && !x.Deleted, cancellationToken);
            if (isExistSameUsername)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AyniKullaniciAdinaSahipKayitVar);

            var isExistSameEmail = await _paymentContext.Users.AnyAsync(x => x.Email == request.Email && !x.Deleted, cancellationToken);
            if (isExistSameEmail)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AyniMailAdresineSahipKayitVar);

            var password = GeneratorHelper.GeneratePassword();
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
            }
            //, new (){
            //    RoleId = RoleConstants.ACCOUNTING_ID }
            };

            await _paymentContext.Users.AddAsync(user, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            var mailBody = new StringBuilder();
            mailBody.AppendLine("Sisteme yeni kullanıcı olarak eklendiniz.");
            mailBody.AppendLine($"Kullanıcı adı: {user.Username}");
            mailBody.AppendLine($"Şifre: {user.Password}");
            mailBody.AppendLine($"Aşağıdaki linkten giriş yapabilirsiniz.");
            mailBody.AppendLine($"https://{_httpContextAccessor.HttpContext.Request.Host}/account/login");

            var mail = new MailDto
            {
                Subject = MailConstants.NEW_USER_ADDING_SUBJECT,
                Body = mailBody.ToString(),
                Recipients = new string[] { user.Email }
            };

            await _mailSenderService.SendAsync(mail);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
