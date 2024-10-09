using SwitchBack.Repositories;
using SwitchBack.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SwitchBack
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration _configuration)
        {
            string connectionString = "";
            connectionString = _configuration["ConnectionStrings:SQLConnectionStrings"];

            services.AddDbContext<SwitchContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IBarriosRepository, BarriosRepository>();
            services.AddScoped<IHabilidadesRepository, HabilidadesRepository>();
            services.AddScoped<IModalidadesRepository, ModalidadesRepository>();
            services.AddScoped<IPublHabiRepository, PublHabiRepository>();
            services.AddScoped<IPublicacionesRepository, PublicacionesRepository>();
            services.AddScoped<IPublModaRepository, PublModaRepository>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();



            return services;
        }
    }
}
