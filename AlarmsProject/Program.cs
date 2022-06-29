using Alarms.Db.Entities;
using Alarms.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Alarms.GeneratorApp
{
    internal class Program
    {



        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services
                    .AddDbContext<AlarmsDbContext>(options =>
                        options
                        .UseSqlServer(
                            hostContext.Configuration.GetConnectionString("DefaultConnection"),
                            b => b.MigrationsAssembly(typeof(AlarmsDbContext).Assembly.FullName)
                            )
                        .EnableSensitiveDataLogging()
                    )
                    .AddScoped<DbTagSaving>()
                    .AddScoped<DbSavingMockData>()
                    .AddScoped<ToRun>();
                })
                .Build();

            var scope = host.Services.CreateScope();
            var provider = scope.ServiceProvider;

            var toRun = provider.GetRequiredService<ToRun>();
            toRun.RunIt();
        }


    }
}




