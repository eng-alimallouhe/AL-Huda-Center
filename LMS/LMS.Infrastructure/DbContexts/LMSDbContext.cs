using Microsoft.EntityFrameworkCore;

using LMS.Domain.Entites.Users;
using LMS.Domain.Entites.HR;
using LMS.Domain.Entites.Orders;
using LMS.Domain.Entites.Stock;
using LMS.Domain.Entities.Financial;
using System;
using LMS.Domain.Entites.Stock.Products;
using LMS.Domain.Entites.Stock.Authors;
using LMS.Domain.Entites.Stock.Genres;
using LMS.Domain.Entites.Stock.Publishers;
using LMS.Domain.Entites.Financial;
using LMS.Domain.Entites.Financial.Levels;
using LMS.Domain.Entites.Stock.Categories;

namespace LMS.Infrastructure.DbContexts
{
    public class LMSDbContext : DbContext
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
        public DbSet<Order> Orders { get; set; }
        public DbSet<BaseOrder> BaseOrders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PrintingOrder> PrintingOrders { get; set; }
        public DbSet<Shipment> Shipments { get; set; }



        // Stock Namespace
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
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
        public DbSet<Payment> Payments { get; set; }
        public DbSet<FinancialRevenue> FinancialRevenues { get; set; }


        public LMSDbContext(DbContextOptions<LMSDbContext> options) : base(options)
        { }
    }
}
