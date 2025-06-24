using DOS.Estoque.Data;
using DOS.Estoque.Domain;
using Microsoft.EntityFrameworkCore;

namespace DOS.Estoque.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IEstoqueRepository, EstoqueRepository>();
            


            var connectionString = configuration["DEFAULT_CONNECTION"];
            services.AddDbContext<EstoqueContext>(options =>
                        options.UseSqlServer(connectionString));

            return services;
        }
    }
}
