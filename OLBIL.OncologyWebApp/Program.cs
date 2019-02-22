using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace OLBIL.OncologyWebApp
{
    public class Program
    {
         public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseConfiguration(new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true)
                                .AddJsonFile("hosting.json", optional: true)
                                .Build())
                .UseContentRoot(Directory.GetCurrentDirectory())
                                .UseStartup<Startup>();

        public static void Main(string[] args)
        {
            try
            {
               CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed miserably with " + ex.Message + " " + ex.InnerException?.Message);
                throw;
            }
        }
    }
}
