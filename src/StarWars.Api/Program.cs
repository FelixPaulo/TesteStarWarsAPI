using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace StarWars.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            try
            {
                Log.Logger = new LoggerConfiguration().WriteTo.File("wwwroot/Logs/log.txt", rollingInterval: RollingInterval.Day).CreateLogger();
                var hostBuilder = CreateHostBuilder(args).Build();
                Log.Information("Iniciando a API");
                hostBuilder.Run();
            }
            catch (System.Exception ex)
            {
                Log.Fatal(ex, "Host encerrado inesperadamente");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
