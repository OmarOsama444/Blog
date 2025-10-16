using Elastic.Clients.Elasticsearch.Mapping;
using Infrastructure.Abstractions;
using Infrastructure.Docuemnts;

namespace Infrastructure.DocumentConfigs
{
    public class PostDocumentConfig : IDocumentTypeConfig<PostDocuemnt>
    {
        public int Version => 1;
        public string IndexName => "posts";
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
                        .Dims(384)
                        .Similarity(DenseVectorSimilarity.Cosine)
                        .Index(true)
                    )
                    .Date(d => d.CreatedOnUtc)
                );
        }
    }
}