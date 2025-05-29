using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using LMS.Application.Abstractions.Services.Authentication;
using LMS.Application.Abstractions.Services.EmailSender;
using LMS.Application.Abstractions.Services.EmailServices;
using LMS.Application.Abstractions.Services.Helpers;
using LMS.Application.Abstractions.Services.ImagesServices;
using LMS.Application.Features.Authentication.Register.Commands.CreateTempAccount;
using LMS.Application.Settings;
using LMS.Common.Settings;
using LMS.Domain.Abstractions;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Financial;
using LMS.Domain.Entities.Financial.Levels;
using LMS.Domain.Entities.HR;
using LMS.Domain.Entities.Orders;
using LMS.Domain.Entities.Stock;
using LMS.Domain.Entities.Stock.Authors;
using LMS.Domain.Entities.Stock.Genres;
using LMS.Domain.Entities.Stock.Products;
using LMS.Domain.Entities.Stock.Publishers;
using LMS.Domain.Entities.Users;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Financial;
using LMS.Infrastructure.Repositories.Financial.Levels;
using LMS.Infrastructure.Repositories.HR;
using LMS.Infrastructure.Repositories.OrderManagement;
using LMS.Infrastructure.Repositories.Orders;
using LMS.Infrastructure.Repositories.Stock;
using LMS.Infrastructure.Repositories.Stock.Authors;
using LMS.Infrastructure.Repositories.Stock.Genres;
using LMS.Infrastructure.Repositories.Stock.Products;
using LMS.Infrastructure.Repositories.Stock.Publishers;
using LMS.Infrastructure.Repositories.Users;
using LMS.Infrastructure.Services.Authentication.Token;
using LMS.Infrastructure.Services.Comunitcation;
using LMS.Infrastructure.Services.Helpers;
using LMS.Infrastructure.Services.Image;
using LMS.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text;
using LMS.Infrastructure.Services.Authentication;
using LMS.API.Middlewares;
using LMS.Application.Abstractions.Services.Admin;
using LMS.Infrastructure.Services.Admin;
using LMS.Domain.Entities.HttpEntities;
using LMS.API.Helpers;

internal class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Error()
            .WriteTo.File("Logs\\logger.txt", rollingInterval: RollingInterval.Month)
            .CreateLogger();


        var builder = WebApplication.CreateBuilder(args);

        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        
        builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,


                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)),
                ClockSkew = TimeSpan.Zero
            };
        });


        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddApplicationInsightsTelemetry();



        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });


        //Inject the DbContext:
        builder.Services.AddDbContext<LMSDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        // Inject the Repositories:
        // Users Repositories:
        builder.Services.AddScoped<IBaseRepository<ImgeURToken>, ImgeURTokenRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Role>, RoleRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<User>, UserRepositroy>();
        builder.Services.AddScoped<ISoftDeletableRepository<Department>, DepartmentRepository>();
        builder.Services.AddScoped<IBaseRepository<DepartmentResponsibility>, DepartmentResponsibilityRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Employee>, EmployeeRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<EmployeeDepartment>, EmployeeDepartmentRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Customer>, CustomerRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Address>, AddressRepository>();
        builder.Services.AddScoped<IBaseRepository<OtpCode>, OtpCodeRepository>();
        builder.Services.AddScoped<IBaseRepository<Notification>, NotificationRepository>();
        builder.Services.AddScoped<IBaseRepository<RefreshToken>, RefreshTokenRepository>();


        //Stock Repositories: 
        builder.Services.AddScoped<ISoftDeletableRepository<Supplier>, SupplierRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Purchase>, PurchaseRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Publisher>, PublisherRepository>();
        builder.Services.AddScoped<IBaseRepository<PublisherTranslation>, PublisherTranslationRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Product>, ProductRepository>();
        builder.Services.AddScoped<IBaseRepository<ProductTranslation>, ProductTranslationRepository>();
        builder.Services.AddScoped<IBaseRepository<InventoryLog>, InventoryLogRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Genre>, GenreRepository>();
        builder.Services.AddScoped<IBaseRepository<GenreTranslation>, GenreTranslationRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Book>, BookRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Author>, AuthorRepository>();
        builder.Services.AddScoped<IBaseRepository<AuthorTranslation>, AuthorTranslationRepository>();

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(CreateTempAccountCommand).Assembly);
        });

        //Orders Repositories: 
        builder.Services.AddScoped<ISoftDeletableRepository<Order>, OrderRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<BaseOrder>, BaseOrderRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Shipment>, ShipmentRepository>();
        builder.Services.AddScoped<IBaseRepository<CartItem>, CartItemRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Cart>, CartRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<OrderItem>, OrderItemRepository>();


        //HR Repositories:
        builder.Services.AddScoped<ISoftDeletableRepository<Salary>, SalaryRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Penalty>, PenaltyRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Leave>, LeaveRepository>();
        builder.Services.AddScoped<IBaseRepository<LeaveBalance>, LeaveBalanceRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Incentive>, IncentiveRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Attendance>, AttendanceRepository>();


        //Financial Repositories: 
        builder.Services.AddScoped<ISoftDeletableRepository<LoyaltyLevel>, LevelRepository>();
        builder.Services.AddScoped<IBaseRepository<LoyaltyLevelTransaltion>, LevelTranslationRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<FinancialRevenue>, FinancialRevenueRepository>();
        builder.Services.AddScoped<ISoftDeletableRepository<Payment>, PaymentRepository>();

        
        builder.Services.Configure<EmailSettings>(
            builder.Configuration.GetSection("EmailSettings"));

        
        builder.Services.Configure<TokenSettings>(
            builder.Configuration.GetSection("JwtSettings"));


        //Inject The services:
        builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
        builder.Services.AddScoped<IDepartmentHelper, DepartmentHelper>();
        builder.Services.AddScoped<IEmployeeHelper, EmployeeHelper>();
        builder.Services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        builder.Services.AddScoped<ITokenReaderService, TokenReaderService>();
        builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
        builder.Services.AddScoped<IEmailTemplateReaderService, EmailTemplateReaderService>();
        builder.Services.AddScoped<IRandomGeneratorService, RandomGeneratorService>();
        builder.Services.AddHttpClient<IImageUploader, ImageUploader>();
        builder.Services.AddHttpClient<IImageAuthService, ImageAuthService>();
        builder.Services.AddScoped<IApiImageUploadHelper, ApiImageUploadHelper>();

        //Domain Services:
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.MapControllers();

        app.Run();
    }
}