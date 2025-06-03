namespace LMS.API.DTOs.Admin
{
    public class DashboardKpiDto
    {
        public int UsersCount { get; set; }
        public int EmployeesCount { get; set; }
        public int CustomersCount { get; set; }
        public int NewCustomersCount { get; set; }
        public int PendingOrdersCount { get; set; }
        public int BooksCount { get; set; }
        public int NewBooksCount { get; set; }
    }
}