using System.Net.Http.Headers;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Infrastructure.Docuemnts;
using Infrastructure.DocumentConfigs;
using Infrastructure.Mappers;

namespace Infrastructure.Services;

public class ElasticService(ElasticsearchClient client, IEmbeddingService embeddingService) : IELasticService
{
    public Task CreatePostAsync(Post post, CancellationToken cancellationToken = default)
    {
        var postDocuemnt = post.ToDocument();
        return client.IndexAsync<PostDocuemnt>(postDocuemnt,
            d => d
                .Id(postDocuemnt.Id)
                .Index(new PostDocumentConfig().IndexVersionedName)
            , cancellationToken);
    }
    public async Task<ICollection<PostResponseDto>> SearchPostSemantic(SearchPostRequestDto requestDto)
    {
        var searchRequest = new SearchRequestDescriptor<PostDocuemnt>(new PostDocumentConfig().IndexName);
        searchRequest = searchRequest
            .From((requestDto.Page - 1) * requestDto.PageSize)
            .Size(requestDto.PageSize)
            .Sort(s => s
                .Score(x => x.Order(SortOrder.Desc))
                .Field(x => x.CreatedOnUtc, SortOrder.Desc)
            );

        if (!string.IsNullOrWhiteSpace(requestDto.SearchTerm))
        {
            var embedding = await embeddingService.GenerateEmbedingFromText(requestDto.SearchTerm);
            searchRequest = searchRequest.Query(q => QuerySemanticSearch(q, requestDto.SearchTerm, [.. embedding]));
        }
        else
        {
            searchRequest.Query(q => q.MatchAll());
        }
        var documents = await client.SearchAsync<PostDocuemnt>(searchRequest);
        return [.. documents.Documents.Select(d => d.ToEntity().ToPostResponseDto())];
    }

    private QueryDescriptor<PostDocuemnt> QuerySemanticSearch(QueryDescriptor<PostDocuemnt> q, string query, float[] embedding)
    {
        return q.ScriptScore(sc => sc
            .Query(qq => qq
                .MultiMatch(m => m
                    .Fields(f => f.Title, f => f.Content, f => f.Tags)
                    .Query(query)
                    .Fuzziness("AUTO")
                )
            )
            .Script(ss => ss
                .Source("0.7 * cosineSimilarity(params.queryVector, 'embedding') + 0.3 * _score")
                .Params(p => p.Add("queryVector", embedding))
            )
        );
    }

}
