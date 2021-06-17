using _swagger.ConnectKafka;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _swagger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureServices((context, collection) =>
                {
                    collection.AddHostedService<KafkaProcedurerHostedService>();
                });
        /*ConfigureServices((context, collection) =>
            {
                collection.AddHostedService<KafkaProcedurerHostedService>();
            }).*/
    }
}
