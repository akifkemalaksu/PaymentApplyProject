namespace PaymentApplyProject.Application.Dtos.SelectDtos
{
    public class SelectResult
    {
        public int Count { get; set; }
        public IEnumerable<Option> Items { get; set; }
    }
}
