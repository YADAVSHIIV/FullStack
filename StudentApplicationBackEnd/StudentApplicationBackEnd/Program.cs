using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using StudentApplicationBackEnd.Models;

namespace StudentApplicationBackEnd
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine($"Initializing On : {DateTime.UtcNow.ToString()}");
            Console.WriteLine($"---------------------------------------------------------------------");

            try
            {

                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    Console.WriteLine("Migration : applying");
                    try
                    {
                        var context = services.GetRequiredService<Database>();
                        context.Database.Migrate();

                        Console.WriteLine("Migration : applied");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Migration : failed : ex {ex.Message}");
                    }
                }
                host.Run();


                Console.WriteLine($"Live On: {DateTime.UtcNow.ToString()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Initialization Failed : ex {ex.Message}");
                throw;
            }

        }


        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.ConfigureKestrel(serverOptions =>
               {// Set properties and call methods on options
               }).UseStartup<Startup>().ConfigureLogging(logging =>
               {
                   logging.ClearProviders();
                   logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Error);

               }).UseKestrel().ConfigureKestrel((context, options) =>
               { 
                   options.Listen(IPAddress.Any, 5555); 
               });
           });

    }
}