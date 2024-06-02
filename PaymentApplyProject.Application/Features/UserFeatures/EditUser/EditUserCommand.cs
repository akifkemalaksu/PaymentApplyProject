using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.EditUser
{
    public class EditUserCommand : IRequest<Response<NoContent>>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Active { get; set; }
        public IEnumerable<short> Companies { get; set; }
        public short RoleId { get; set; }

        public EditUserCommand()
        {
            Companies = new List<short>();
        }
    }
}
