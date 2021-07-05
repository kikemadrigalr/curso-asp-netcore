using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curso_asp_netcore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace curso_asp_netcore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

        // ***********************IN MEMORY DATA BASE***********************************
            //configuracion del servicio para el contexto de base de datos
            // services.AddDbContext<EscuelaContext>(
            //     options => options.UseInMemoryDatabase(databaseName: "TestDB")
            // );


            // **********************MY SQL DATA BASE**********************************
            string mySqlConnectionStr = ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnectionstring");
            services.AddDbContext<EscuelaContext>(
                options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr))
            );

            // *************************SQL SERVER DATA BASE***************************************
            // string connString = ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnectionstring");
            // services.AddDbContext<EscuelaContext>(
            //     options => options.UseSqlServer(connString))
            // );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Escuela}/{action=Index}/{id?}");
            });
        }
    }
}
