using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using StudentApplicationBackEnd.Models;
using System.Security.Authentication;

namespace StudentApplicationBackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        const string AllowAllOrigins = "AllowAllOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var DB_IP = Environment.GetEnvironmentVariable("DB_IP") ?? "127.0.0.1"; //throw new InvalidCredentialException("invalid value supplied for 'DB_IP'");
            var DB_Name = Environment.GetEnvironmentVariable("DB_Name") ?? "StudentDB";// throw new InvalidCredentialException("invalid value supplied for 'DB_Name'");
            var DB_User = Environment.GetEnvironmentVariable("DB_User");// throw new InvalidCredentialException("invalid value supplied for 'DB_User'");
            var DB_Password = Environment.GetEnvironmentVariable("DB_Password");// throw new InvalidCredentialException("invalid value supplied for 'DB_Password'");
            var DB_Port = int.TryParse((Environment.GetEnvironmentVariable("DB_Port") ?? "1433"), out var _DB_Port) ? _DB_Port : throw new InvalidCastException("invalid value supplied for 'DB_Port'");

            services.AddControllers();

            services.AddDbContext<Database>((provider, optionBuilder) =>
            {
                optionBuilder.UseSqlServer($"Server={DB_IP},{DB_Port};Database={DB_Name}; {((string.IsNullOrWhiteSpace(DB_User)|| string.IsNullOrWhiteSpace(DB_Password))? "Integrated Security=true;":($"User Id={DB_User};Password={DB_Password};"))} ", sqlOptions =>
                {
                    sqlOptions.CommandTimeout(300);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10);
                    //sqlOptions.MigrationsAssembly("StudentApplicationBackEnd"); 
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(AllowAllOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    //builder.WithOrigins("http://example.com","http://www.contoso.com");
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseCors(AllowAllOrigins);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
