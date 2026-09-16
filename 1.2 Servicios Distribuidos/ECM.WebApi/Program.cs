using System;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace ECM.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // El log se levanta antes que el host para que cualquier fallo de arranque
            // quede registrado en el archivo y no solo en la consola.
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();

            try
            {
                Log.Information("Iniciando ECM.WebApi");

                var startup = new Startup(builder.Configuration);

                startup.ConfigureServices(builder.Services);

                var app = builder.Build();

                startup.Configure(app, app.Environment);

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
