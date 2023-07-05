using MediatR;
using PaymentApplyProject.Application.Services;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly ICookieAuthService _cookieAuthService;

        public LogoutCommandHandler(ICookieAuthService cookieAuthService)
        {
            _cookieAuthService = cookieAuthService;
        }

        public Task Handle(LogoutCommand request, CancellationToken cancellationToken) => _cookieAuthService.SignOutAsync();
    }
}
