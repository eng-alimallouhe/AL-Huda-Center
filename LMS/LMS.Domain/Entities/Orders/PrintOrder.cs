namespace LMS.Domain.Entities.Orders
{
    public class PrintOrder : BaseOrder
    {
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int CopiesCount { get; set; }
        public decimal CopyCost { get; set; }
        public string FileUrl { get; set; } 
        public string FileName { get; set; }


        public PrintOrder()
        {
            CopiesCount = 1;
            FileUrl = string.Empty;
            FileName = string.Empty;
        }
    }
}
