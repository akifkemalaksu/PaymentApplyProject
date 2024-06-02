namespace PaymentApplyProject.Application.Dtos.SelectDtos
{
    public class SelectParameters
    {
        public string Search { get; set; }
        public int Page { get; set; }
        public int PageLength { get; set; }

        public SelectParameters()
        {
            Search ??= string.Empty;
        }
    }
}
