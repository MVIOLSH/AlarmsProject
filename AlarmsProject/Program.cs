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
            ToRun run = new ToRun();
            run.RunIt();

        }



        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<AlarmsDbContext>(options =>
                    options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AlarmsDbContext).Assembly.FullName)).EnableSensitiveDataLogging()
                );
                    services.AddScoped<DbTagSaving>();
                    services.AddScoped<DbSavingMockData>();



                });
        }

    }
}




