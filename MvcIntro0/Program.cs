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
            IHost hst = CreateHostBuilder(args).Build();
            using (IServiceScope scpe = hst.Services.CreateScope())
            {
                IServiceProvider services = scpe.ServiceProvider;
                try
                {
                }
                catch (Exception excptn)
                {
                    //services.GetRequiredService<ILogger<Program>>()
                    //    .LogDebug(excptn, "Database seeding error");
                }
                TestData.Initialize(services.GetRequiredService<StoreContext>());
            }
            hst.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>());
    }
}
