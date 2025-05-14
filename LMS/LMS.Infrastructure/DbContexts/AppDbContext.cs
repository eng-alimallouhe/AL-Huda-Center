using LMS.Domain.Entities.Users;
using LMS.Domain.Entities.HR;
using LMS.Domain.Entities.Stock;
using LMS.Domain.Entities.Stock.Authors;
using LMS.Domain.Entities.Stock.Genres;
using LMS.Domain.Entities.Stock.Products;
using LMS.Domain.Entities.Stock.Publishers;
using LMS.Domain.Entities.Orders;
using LMS.Domain.Entities.Financial;
using LMS.Domain.Entities.Financial.Levels;
using LMS.Infrastructure.Configurations.Users;
using LMS.Infrastructure.Configurations.HR;
using LMS.Infrastructure.Configurations.Stock;
using LMS.Infrastructure.Configurations.Stock.Authors;
using LMS.Infrastructure.Configurations.Stock.Genres;
using LMS.Infrastructure.Configurations.Stock.Products;
using LMS.Infrastructure.Configurations.Stock.Publishers;
using LMS.Infrastructure.Configurations.Orders;
using LMS.Infrastructure.Configurations.Financial;
using LMS.Infrastructure.Configurations.Financial.Levels;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.DbContexts
{
    public class AppDbContext : DbContext
    {
        // Users Namespace
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<OtpCode> OtpCodes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        //HR Namespace
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Incentive> Incentives { get; set; }
        public DbSet<Penalty> Penalties { get; set; }

        // Orders Namespace
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<BaseOrder> BaseOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PrintOrder> PrintOrders { get; set; }
        public DbSet<Shipment> DeliveryOrders { get; set; }

        // Stock Namespace
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GenreTranslation> GenreTranslations { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<PublisherTranslation> PublisherTranslations { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorTranslation> AuthorTranslations { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        //Financial Namespace:
        public DbSet<LoyaltyLevel> Levels { get; set; }
        public DbSet<LoyaltyLevelTransaltion> LevelTrnsaltions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<FinancialRevenue> FinancialRevenues { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Users Namespace: 
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new RoleConfigurations());
            modelBuilder.ApplyConfiguration(new EmployeeConfigurations());
            modelBuilder.ApplyConfiguration(new AddressConfigurations());
            modelBuilder.ApplyConfiguration(new DepartmentConfigurations());
            modelBuilder.ApplyConfiguration(new EmployeeDepartmentConfigurations());
            modelBuilder.ApplyConfiguration(new OtpConfigurations());
            modelBuilder.ApplyConfiguration(new NotificationConfigurations());


            //Stock Namespace: 
            modelBuilder.ApplyConfiguration(new SupplierConfigurations());
            modelBuilder.ApplyConfiguration(new PurchaseConfigurations());
            modelBuilder.ApplyConfiguration(new DiscountConfigurations());
            modelBuilder.ApplyConfiguration(new InventoryLogConfigurations());
            modelBuilder.ApplyConfiguration(new GenreConfigurations());
            modelBuilder.ApplyConfiguration(new PublisherConfigurations());
            modelBuilder.ApplyConfiguration(new ProductConfigurations());
            modelBuilder.ApplyConfiguration(new BookConfigurations());
            modelBuilder.ApplyConfiguration(new AuthorConfigurations());

            //Orders Namespace: 
            modelBuilder.ApplyConfiguration(new OrderConfigurations());
            modelBuilder.ApplyConfiguration(new PrintOrderConfigurations());
            modelBuilder.ApplyConfiguration(new OrderItemConfigurations());
            modelBuilder.ApplyConfiguration(new BaseOrderConfigurations());
            modelBuilder.ApplyConfiguration(new ShipmentConfigurations());
            modelBuilder.ApplyConfiguration(new CartItemConfigurations());
            modelBuilder.ApplyConfiguration(new CartConfigurations());

            //HR Namespace:
            modelBuilder.ApplyConfiguration(new SalaryConfigurations());
            modelBuilder.ApplyConfiguration(new PenaltyConfigurations());
            modelBuilder.ApplyConfiguration(new LeaveConfigurations());
            modelBuilder.ApplyConfiguration(new LeaveBalanceConfigurations());
            modelBuilder.ApplyConfiguration(new IncentiveConfigurations());
            modelBuilder.ApplyConfiguration(new AttendanceConfigurations());

            //Financial Namespace: 
            modelBuilder.ApplyConfiguration(new PaymentConfigurations());
            modelBuilder.ApplyConfiguration(new LevelConfigurations());
            modelBuilder.ApplyConfiguration(new FinancialRevenueConfigurations());
        }
    }
}
