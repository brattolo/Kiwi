
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO;

namespace Kiwi.API
{
    public class Program
    {
        private static string environmentName;
        private static string contentRootPath;

        public static int Main(string[] args)
        {
            IWebHost webHost = CreateWebHostBuilder(args);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(contentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables()
                .Build();

            var elasticUri = configuration["ElasticConfiguration:url"];

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .Enrich.FromLogContext()
               .WriteTo.Console()
               .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
               {
                   CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                   MinimumLogEventLevel = LogEventLevel.Verbose,
                   AutoRegisterTemplate = true
               })
               .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                Log.Information($"environmentName: {environmentName}");
                Log.Information($"contentRootPath: {contentRootPath}");
                webHost.Run();
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

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, config) =>
                {
                    contentRootPath = hostingContext.HostingEnvironment.ContentRootPath;
                    environmentName = hostingContext.HostingEnvironment.EnvironmentName;
                })
                .UseSerilog()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();
    }
}
