using MediatR;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Application.Features.UserFeatures.Logout;

namespace PaymentApplyProject.Application.Features.UserFeatures.Logout
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
