using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curso_asp_netcore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace curso_asp_netcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();

            //Modificamos la ejecucion del programa para asegurarnos que la BD sea creada.

            //construir el host
            var host = CreateHostBuilder(args).Build();

            //Acceder a los servicios para construir la Base de Datos
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                
                try{
                    //acceder al contexto de la BD
                    var context = services.GetRequiredService<EscuelaContext>();

                    //asegurar que la BD esta creada
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ocurrio un error");
                }
            }
            //ejecucion del host
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
