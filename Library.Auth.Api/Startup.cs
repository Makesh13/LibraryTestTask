using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.Auth.Api.Domain;
using Microsoft.Extensions.Configuration;
using Common; 

namespace Library.Auth.Api
{
    public class Startup
    {
        private IConfiguration Config { get; }
        public Startup(IConfiguration config)
        {
            Config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var authOptionsConfig = Config.GetSection("Auth");
            services.Configure<AuthOptions>(authOptionsConfig);

            services.AddDbContext<AuthContext>(options=>options.UseNpgsql(this.Config.GetSection("Project").
                GetSection("ConnectionString").Value));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints=>endpoints.MapControllers());
        }
    }
}
