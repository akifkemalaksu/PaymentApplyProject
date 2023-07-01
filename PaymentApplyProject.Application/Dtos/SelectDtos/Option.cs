namespace PaymentApplyProject.Application.Dtos.SelectDtos
{
    public class Option
    {
        public string Text { get; set; }
        public string Id { get; set; }
        public bool DefaultSelected { get; set; }
        public bool Selected { get; set; }
        public bool Disabled { get; set; }
    }
}
