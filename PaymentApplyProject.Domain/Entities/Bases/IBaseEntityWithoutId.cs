namespace PaymentApplyProject.Domain.Entities
{
    public interface IBaseEntityWithoutId
    {
        public bool Deleted { get; set; }
        public int AddedUserId { get; set; }
        public int EditedUserId { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime EditDate { get; set; }
    }

}
