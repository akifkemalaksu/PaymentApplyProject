using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.MailDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Helpers;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Services.InfrastructureServices;
using PaymentApplyProject.Domain.Constants;
using System.Text;

namespace PaymentApplyProject.Application.Features.UserFeatures.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IMailSenderService _mailSenderService;
        private readonly ICacheService _cacheService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ForgotPasswordCommandHandler(IPaymentContext paymentContext, IMailSenderService mailSenderService, ICacheService cacheService, IHttpContextAccessor httpContextAccessor)
        {
            _paymentContext = paymentContext;
            _mailSenderService = mailSenderService;
            _cacheService = cacheService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<NoContent>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _paymentContext.Users.FirstOrDefaultAsync(x =>
                x.Email == request.Email
                && !x.UserRoles
                    .Any(us =>
                    us.RoleId == RoleConstants.CUSTOMER_ID
                    && !us.Deleted)
                && !x.Deleted, cancellationToken);

            if (user == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.OK, Messages.KullaniciBulunamadi);

            if (!user.Active)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.OK, Messages.KullaniciAktifDegil);

            var now = DateTime.Now;
            var userString = $"({user.Id})-({user.Username})-({user.Email})-({now.ToString("o")})";
            var passwordResetToken = GeneratorHelper.GenerateSha256Key(userString);

            var resetPasswordMinutes = 15;

            _cacheService.Set(passwordResetToken, user.Id, TimeSpan.FromMinutes(resetPasswordMinutes));

            var mailBody = new StringBuilder();
            mailBody.AppendLine("Şifre sıfırlama ekranına aşağıdaki linkten ulaşabilirsiniz.");
            mailBody.AppendLine($"https://{_httpContextAccessor.HttpContext.Request.Host}/account/resetpassword/{passwordResetToken}");
            mailBody.AppendLine($"Uyarı! Şifrenizi sıfırlamak için {resetPasswordMinutes} dakikanız vardır.");

            var mail = new MailDto
            {
                Subject = MailConstants.RESET_PASSWORD_SUBJECT,
                Body = mailBody.ToString(),
                Recipients = new string[] { user.Email }
            };

            await _mailSenderService.SendAsync(mail);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
