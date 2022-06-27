using Alarms.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alarms.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection ImplementPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AlarmsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(
                "DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AlarmsDbContext).Assembly.FullName)), ServiceLifetime.Transient);

            //services.AddScoped<AlarmsDbContext>(provider => provider.GetService<AlarmsDbContext>());

            services.AddScoped<DbSavingMockData>();
            services.AddScoped<DataHandler>();

            return services;
        }

    }
}
