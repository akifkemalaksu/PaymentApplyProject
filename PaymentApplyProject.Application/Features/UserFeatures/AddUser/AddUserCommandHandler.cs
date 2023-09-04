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

namespace PaymentApplyProject.Application.Features.UserFeatures.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _customMapper;
        private readonly IMailSenderService _mailSenderService;

        public AddUserCommandHandler(IPaymentContext paymentContext, ICustomMapper customMapper, IMailSenderService mailSenderService)
        {
            _paymentContext = paymentContext;
            _customMapper = customMapper;
            _mailSenderService = mailSenderService;
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
            }};

            await _paymentContext.Users.AddAsync(user, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            var mailBody = string.Format(
            @"Sisteme yeni kullanıcı olarak eklendiniz.

              Kullanıcı adı: {0}

              Şifre: {1}

              Aşağıdaki linkten giriş yapabilirsiniz.

              https://www.kolayodemeonline.com/account/login", user.Username, user.Password);

            var mail = new MailDto
            {
                Subject = MailConstants.NEW_USER_ADDING_SUBJECT,
                Body = mailBody,
                Recipients = new string[] { user.Email }
            };

            await _mailSenderService.SendAsync(mail);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
