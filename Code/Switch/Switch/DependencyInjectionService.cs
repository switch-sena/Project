using Switch.Repositories;
using Switch.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceCRUD
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration _configuration)
        {
            string connectionString = "";
            connectionString = _configuration["ConnectionStrings:SQLConnectionStrings"];

            services.AddDbContext<SwitchContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IBarriosRepository, BarriosRepository>();


            return services;
        }
    }
}
