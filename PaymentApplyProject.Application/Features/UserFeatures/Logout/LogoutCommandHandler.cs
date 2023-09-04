using MediatR;
using PaymentApplyProject.Application.Features.UserFeatures.Logout;
using PaymentApplyProject.Application.Services.InfrastructureServices;

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
