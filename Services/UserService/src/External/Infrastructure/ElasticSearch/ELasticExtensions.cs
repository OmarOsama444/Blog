using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ElasticSearch
{
    public static class ELasticExtensions
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<ElasticSearchCredsOptions>(configuration.GetSection("ElasticSearchCreds"));
            var elasticSearchUrl = configuration.GetConnectionString("ElasticSearchUrl")!;
            var creds = configuration.GetSection("ElasticSearchCreds").Get<ElasticSearchCredsOptions>()!;
            var settings = new ElasticsearchClientSettings(new Uri(elasticSearchUrl))
                .DisableDirectStreaming()
                .PrettyJson()
                .Authentication(new BasicAuthentication(creds.Username, creds.Password))
                .ThrowExceptions()
                .ServerCertificateValidationCallback((o, c, ch, e) => true);
            services.AddSingleton<ElasticsearchClient>(new ElasticsearchClient(settings));
            return services;
        }
    }
}