using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.UserFeatures.AuthenticateToken;
using PaymentApplyProject.Application.Dtos.UserDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Services
{
    public interface IJwtAuthService
    {
        public Response<AuthenticateTokenResult> CreateToken(UserDto kullaniciDto);
    }
}
