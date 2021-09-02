using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MvcIntro0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcIntro0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hst = CreateHostBuilder(args).Build();

            using (IServiceScope scpe = hst.Services.CreateScope())
            {
                var services = scpe.ServiceProvider;
            
                try
                {
                    TestData.Initialize(services.GetRequiredService<StoreContext>());
                }
                catch (Exception excptn)
                {
                    services.GetRequiredService<ILogger<Program>>()
                        .LogDebug(excptn, "Database seeding error");
                }
            }

            hst.Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>());
    }
}
