using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace _swagger.ConnectKafka
{
    public class KafkaProcedurerHostedService : IHostedService
    {
        private readonly ILogger<KafkaProcedurerHostedService> _logger;
        private IProducer<Null, string> _producer;
        private static ProducerConfig config = new ProducerConfig();
        //private AccountSevices _AccountServices;
        public KafkaProcedurerHostedService(ILogger<KafkaProcedurerHostedService> logger)
        {
            _logger = logger;
            config = new ProducerConfig()
            {
                BootstrapServers = ConfigurationSettings.AppSettings["BootstrapServers"]
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        } //
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                //var value = "hello world";
                //_logger.LogInformation(value);
                //await _producer.ProduceAsync("RegisterUser1", new Message<Null, string>()
                //{
                //    Value = value
                //}, cancellationToken);
                _producer.Flush(TimeSpan.FromSeconds(10));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static Object SendToKafka(string topic, string message)
        {
            using (var producer =
                 new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    return producer.ProduceAsync(topic, new Message<Null, string> { Value = message })
                        .GetAwaiter()
                        .GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e}");
                }
            }
            return null;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _producer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
