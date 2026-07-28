using AlmacenEconomia.Application.Interfaces.Code;
using AlmacenEconomia.Application.Interfaces.Email;
using AlmacenEconomia.Application.Interfaces.Jwt;
using AlmacenEconomia.Application.Interfaces.Password;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Application.Interfaces.Repository.AdminDebt;
using AlmacenEconomia.Application.Interfaces.Repository.AdminSale;
using AlmacenEconomia.Application.Interfaces.Repository.Code;
using AlmacenEconomia.Application.Interfaces.Repository.Combo;
using AlmacenEconomia.Application.Interfaces.Repository.Customer;
using AlmacenEconomia.Application.Interfaces.Repository.HomeSaleRepository;
using AlmacenEconomia.Application.Interfaces.Repository.Offer;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Application.Interfaces.Repository.Worker;
using AlmacenEconomia.Application.Interfaces.Services.CountryValidator;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Infrastructure.Db;
using AlmacenEconomia.Infrastructure.Repository.Admin;
using AlmacenEconomia.Infrastructure.Repository.AdminDebt;
using AlmacenEconomia.Infrastructure.Repository.AdminSale;
using AlmacenEconomia.Infrastructure.Repository.Code;
using AlmacenEconomia.Infrastructure.Repository.Combo;
using AlmacenEconomia.Infrastructure.Repository.Generic;
using AlmacenEconomia.Infrastructure.Repository.HomeSaleRepository;
using AlmacenEconomia.Infrastructure.Repository.Offer;
using AlmacenEconomia.Infrastructure.Repository.Product;
using AlmacenEconomia.Infrastructure.Repository.ProductEnter;
using AlmacenEconomia.Infrastructure.Repository.Worker;
using AlmacenEconomia.Infrastructure.Security;
using AlmacenEconomia.Infrastructure.Services.CountryCode;
using AlmacenEconomia.Infrastructure.Services.Email;
using CountryData.Globalization.Hosting.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlmacenEconomia.Infrastructure.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddDbContext<EconomiaDbContext>(options =>
        options.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(EconomiaDbContext).Assembly.FullName)
        ));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IAdminRepository , AdminRepository>();
        services.AddScoped<IJwtGenerator , JwtGenerator>();
        services.AddScoped<IPasswordHashed , PasswordHashed>();
        services.AddCountryData();
        services.AddScoped<ICountryValidator , CountryCodeValidator>();
        services.AddScoped<ICustomerRepository , CustomerRepository>();
        services.Configure<EmailSetting>(configuration.GetSection("EmailSettings"));
        services.AddScoped<ISendEmailService , SendEmailService>();
        services.AddScoped<ICodeRepository , CodeRepository>();
        services.AddScoped<ICodeHash , CodeHash>();
        services.AddScoped<IWorkerRepository , WorkerRepository>();
        services.AddScoped<IProductRepository , ProductRepository>();
        services.AddScoped<IComboRepository , ComboRepository>();
        services.AddScoped<IOfferRepository , OfferRepository>();
        services.AddScoped<IProductEnterRepository , ProductEnterRepository>();
        services.AddScoped<IHomeSaleRepository ,HomeSaleRepository>();
        services.AddScoped<IAdminSaleRepository , AdminSaleRepository>();
        services.AddScoped<IAdminDebtRepository , AdminDebtRepository>();
        return services;
    }
}