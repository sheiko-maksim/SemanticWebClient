using DataAccess.EF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.Realization;

namespace DataAccess.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddAppDbContext(
            this IServiceCollection services,
            string connectionString)
        {
            return services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(connectionString);
            });
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<UserRepository>();
            services.AddScoped<MovieRepository>();

            return services;
        }
    }
}
