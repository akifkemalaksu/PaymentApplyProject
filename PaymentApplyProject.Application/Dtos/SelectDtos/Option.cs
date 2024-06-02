namespace PaymentApplyProject.Application.Dtos.SelectDtos
{
    public class Option
    {
        public required string Text { get; set; }
        public required string Id { get; set; }
        public bool DefaultSelected { get; set; }
        public bool Selected { get; set; }
        public bool Disabled { get; set; }
    }
}
