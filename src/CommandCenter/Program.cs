using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.WindowsAzure.Storage;
using Serilog;
using Serilog.Events;

namespace CommandCenter
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost
                .CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
        }

        public static int Main(string[] args)
        {
            CloudStorageAccount storage = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=ercmktplc;AccountKey=9XPS5+eorM4eST0pp3XH98jtNwS5aomqYwU6w4hoTnJT89bYX9X3Tjgd4RrQ4TK8ktEByV4gPnIYT/l+O/yuVQ==;EndpointSuffix=core.windows.net");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.AzureTableStorage(storage)
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}