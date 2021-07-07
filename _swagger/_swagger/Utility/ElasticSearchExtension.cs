using _swagger.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace _swagger.Utility
{
    public static class ElasticSearchExtension
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                var url = configuration["elasticsearch:url"];
                var defaultIndex = configuration["elasticsearch:index"];
                var settings = new ConnectionSettings(new Uri(url));
                AddDefaulMappings(settings);
                var client = new ElasticClient(settings);
                services.AddSingleton<IElasticClient>(client);
                CreateIndex(client, defaultIndex);
            }
            catch
            {

            }
        }

        private static void CreateIndex(ElasticClient client, string defaultIndex)
        {
            var createIndexResponse = client.Indices.Create(defaultIndex, index => index.Map<Account>(x => x.AutoMap()));
        }

        private static void AddDefaulMappings(ConnectionSettings settings)
        {
            settings.
                DefaultMappingFor<Account>(m => m
                .Ignore(a => a.UserName)
                .Ignore(a => a.Password)
                );
        }
    }
}
