using Confluent.Kafka;
using ConsoleApp_DockerWithKafka.Models;
using ConsoleApp_DockerWithKafka.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_DockerWithKafka
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, collection) =>
            {
                collection.AddHostedService<KafkaProcedurerHostedService>();
            });
    }
    public class KafkaProcedurerHostedService : IHostedService
    {
        private readonly ILogger<KafkaProcedurerHostedService> _logger;
        private IProducer<Null, string> _producer;
        private AccountsServices _AccountServices = new AccountsServices();
        public KafkaProcedurerHostedService(ILogger<KafkaProcedurerHostedService> logger)
        {
            _logger = logger;
            var config = new ProducerConfig()
            {
                BootstrapServers = ConfigurationSettings.AppSettings["BootstrapServers"]
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //var value = $"Hello world{i}";
            var accountView = new AccountViewModel()
            {
                FullName = "Tai Thanh Tuan",
                DateOfBirth = new DateTime(2000, 03, 26),
                Address = "128 LTT",
                Phone = "0192912912",
                Telephone = "0292930112",
                Email = "ttt219@gmail.com",
                Faceboook = "hhh",
                UserName = "taikhoantest",
                Password = "Pass@123",
                ConfirmPassword = "Pass@123"
            };
            bool insert = await _AccountServices.createUser(accountView);
            if (insert == false)
            {
                return;
            }
            var value = JsonConvert.SerializeObject(accountView);
            _logger.LogInformation(value);
            await _producer.ProduceAsync("RegisterUser", new Message<Null, string>()
            {
                Value = value
            }, cancellationToken);
            _producer.Flush(TimeSpan.FromSeconds(10));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _producer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
