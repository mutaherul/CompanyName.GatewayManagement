using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CompanyName.GatewayManagement.Data;

namespace CompanyName.GatewayManagement.Api
{
    public static class DatabaseProvider
    {
        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<GatewayDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
