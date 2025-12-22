using Elastic.Clients.Elasticsearch.Mapping;
using Infrastructure.Abstractions;
using Infrastructure.ElasticSearch.Docuemnts;

namespace Infrastructure.ElasticSearch.DocumentConfigs
{
    public class PostDocumentConfig : IDocumentTypeConfig<PostDocuemnt>
    {
        public int Version => 3;
        public string IndexName => "posts";
        public string IndexVersionedName => $"{IndexName}_v{Version}";
        public TypeMappingDescriptor<PostDocuemnt> ConfigureIndex(TypeMappingDescriptor<PostDocuemnt> map)
        {
            return map
                .Properties(ps => ps
                    .Keyword(k => k.Id)
                    .Keyword(k => k.UserId)
                    .Keyword(k => k.Slug)
                    .Text(s => s.Title, text => text.Analyzer("english"))
                    .Text(s => s.Content, text => text.Analyzer("english"))
                    .Text(k => k.Tags, text => text.Analyzer("english"))
                    .DenseVector(d => d.Embedding, denseVector => denseVector
                        .Dims(768)
                        .Similarity(DenseVectorSimilarity.Cosine)
                        .Index(true)
                    )
                    .FloatNumber(f => f.Rating)
                    .IntegerNumber(i => i.TotalUsersRated)
                    .Date(d => d.CreatedOnUtc)
                );
        }
    }
}