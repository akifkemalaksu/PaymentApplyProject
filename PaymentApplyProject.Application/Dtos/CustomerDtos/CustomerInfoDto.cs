namespace PaymentApplyProject.Application.Dtos.CustomerDtos
{
    public class CustomerInfoDto
    {
        public required string CustomerId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Username { get; set; }
    }
}
