using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoTrader.Conntext;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AutoTrader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;

                try
                {
                    var context = service.GetRequiredService<AutoTraderContext>();
                    AutoTraderDbSeeder.Init(context);
                }
                catch(Exception e)
                {
                    var logger = service.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "Error : " + e.Message);
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {

            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();


            // Run migrations
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<AutoTraderContext>();
                db.Database.Migrate();
            }

            return host;
        }
    }
}
