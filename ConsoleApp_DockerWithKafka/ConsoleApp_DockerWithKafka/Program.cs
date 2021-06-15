using Confluent.Kafka;
using ConsoleApp_DockerWithKafka.Models;
using ConsoleApp_DockerWithKafka.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_DockerWithKafka
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            Console.ReadLine();
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
                BootstrapServers = "localhost:9092"
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        } //ConfigurationSettings.AppSettings["BootstrapServers"]
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
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
                var value = JsonConvert.SerializeObject(accountView);
                _logger.LogInformation(value);
                await _producer.ProduceAsync("RegisterUser", new Message<Null, string>()
                {
                    Value = value
                }, cancellationToken);
                bool insert = await _AccountServices.createUser(accountView);
                if (insert == false)
                {
                    return;
                }
                _producer.Flush(TimeSpan.FromSeconds(10));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _producer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
