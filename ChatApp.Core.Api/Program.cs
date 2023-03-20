using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Core.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //Log.Information("Starting service...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Host terminated unexpectedly.");
                // Log.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                // Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>().UseUrls("http://0.0.0.0:5000");
            });
    }
}
