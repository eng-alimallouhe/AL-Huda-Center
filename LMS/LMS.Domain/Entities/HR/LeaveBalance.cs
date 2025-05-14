using LMS.Domain.Entities.Users;

namespace LMS.Domain.Entities.HR
{
    public class LeaveBalance
    {
        //primary key
        public Guid LeaveBalanceId { get; set; }

        
        //Foreign Key: EmployeeId ==> one(employee)-to-one(leavebalance) relationship
        public Guid EmployeeId { get; set; }


        public int RemainBalance { get; set; }
        public int BaseBalance { get; set; }
        public int TotalBalance { get; set; }
        public int RoundedBalance { get; set; }
        public int Year { get; set; }

        
        //Navigation Property:
        public Employee Employee { get; set; }

        
        public LeaveBalance()
        {
            LeaveBalanceId = Guid.NewGuid();
            Employee = null!;
        }
    }
}
