using Lilab.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lilab.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IHistoricTransactionRepository, HistoricTransactionRepository>();
            services.Configure<ConnectionStringConfig>(configuration.GetSection("ConnectionStrings"));
            return services;
        }
    }
}
