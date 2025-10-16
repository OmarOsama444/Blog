using Elastic.Clients.Elasticsearch;
using Infrastructure.Abstractions;
using Infrastructure.DocumentConfigs;

namespace Infrastructure.Migrations;

public class ElasticMigrationManager(ElasticsearchClient client)
{
    public void Migrate()
    {
        InsureIndexCreatedAndUpdated(new PostDocumentConfig());
    }

    private void InsureIndexCreatedAndUpdated<T>(IDocumentTypeConfig<T> documentTypeConfig) where T : class
    {
        string alias = documentTypeConfig.IndexName;
        string indexName = documentTypeConfig.IndexVersionedName;
        var existsResponse = client.Indices.Exists(indexName);

        if (existsResponse.Exists)
            return;
        var createIndexResponse = client.Indices.Create(indexName, c => c.Mappings<T>(x => documentTypeConfig.ConfigureIndex(x)));

        if (!createIndexResponse.IsValidResponse)
        {
            System.Console.WriteLine(createIndexResponse.DebugInformation);
            throw new Exception(createIndexResponse.DebugInformation);
        }
        var aliasResponse = client.Indices.UpdateAliases(b => b
            .Actions(ac => ac
                .Add(a => a.Index(indexName).Alias(alias))
            )
        );
        if (!aliasResponse.IsValidResponse)
        {
            System.Console.WriteLine(aliasResponse.DebugInformation);
            throw new Exception(aliasResponse.DebugInformation);
        }
    }

}
