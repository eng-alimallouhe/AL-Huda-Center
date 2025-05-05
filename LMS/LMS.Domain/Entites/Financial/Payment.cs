namespace LMS.Domain.Entities.Financial
{
    public class Payment
    {
        //primary key: 
        public Guid PaymentId { get; set; }


        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Reasone { get; set; }
        public string Details { get; set; }


        //soft delete: 
        public bool IsActive { get; set; }

        public Payment()
        {
            PaymentId = Guid.NewGuid();
            Date = DateTime.UtcNow;
            Reasone = string.Empty;
            Details = string.Empty;
            IsActive = true;
        }
    }
}
