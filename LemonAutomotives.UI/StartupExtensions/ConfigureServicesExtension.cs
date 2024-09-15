using LemonAutomotives.Core.Domain.RepositoryContracts;
using LemonAutomotives.Core.ServiceContracts;
using LemonAutomotives.Core.Services;
using LemonAutomotives.Infrastructure.DbContext;
using LemonAutomotives.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LemonAutomotives.UI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddControllersWithViews();
            services.AddScoped<ISalespersonRepository, SalespersonRepository>();
            services.AddScoped<ISalespersonService, SalespersonService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
