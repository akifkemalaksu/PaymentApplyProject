using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Dtos.UserDtos;

namespace PaymentApplyProject.Application.Services.InfrastructureServices
{
    public interface IAuthenticatedUserService
    {
        UserDto GetUserInfo();
        int GetUserId();
        string GetUsername();
        string GetBearerToken();
    }
}
