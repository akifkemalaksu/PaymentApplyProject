namespace PaymentApplyProject.Application.Dtos.SelectDtos
{
    public class SelectResult
    {
        public int Count { get; set; }
        public required IEnumerable<Option> Items { get; set; }
    }
}
