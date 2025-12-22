using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Infrastructure.ElasticSearch.Docuemnts;
using Infrastructure.ElasticSearch.DocumentConfigs;
using Infrastructure.Mappers;

namespace Infrastructure.Services;

public class ElasticService(ElasticsearchClient client, IEmbeddingService embeddingService) : IElasticService
{
    public Task UpsertPostAsync(Post post, CancellationToken cancellationToken = default)
    {
        var postDocuemnt = post.ToDocument();
        return client.IndexAsync<PostDocuemnt>(postDocuemnt,
            d => d
                .Id(postDocuemnt.Id)
                .Index(new PostDocumentConfig().IndexVersionedName)
            , cancellationToken);
    }

    public async Task<Post?> GetPostByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await client.GetAsync<PostDocuemnt>(id.ToString(), g => g.Index(new PostDocumentConfig().IndexName), cancellationToken);
        if (!response.Found || response.Source is null)
            return null;

        return response.Source.ToEntity();
    }

    public async Task<ICollection<PostResponseDto>> SearchPostSemantic(SearchPostRequestDto requestDto)
    {
        var searchRequest = new SearchRequestDescriptor<PostDocuemnt>(new PostDocumentConfig().IndexName);
        searchRequest = searchRequest
            .From((requestDto.Page - 1) * requestDto.PageSize)
            .Size(requestDto.PageSize)
            .Sort(s => s
                .Score(x => x.Order(SortOrder.Desc))
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
            .Script(ss => ss
                .Source("cosineSimilarity(params.queryVector, 'embedding') + 1.0")
                .Params(p => p.Add("queryVector", embedding))
            )
        );
    }

    public async Task<ICollection<PostResponseDto>> SearchPostByTextFuzzyAsync(SearchPostRequestDto requestDto)
    {
        var searchRequest = new SearchRequestDescriptor<PostDocuemnt>(new PostDocumentConfig().IndexName);
        searchRequest = searchRequest
            .From((requestDto.Page - 1) * requestDto.PageSize)
            .Size(requestDto.PageSize)
            .Sort(s => s
                .Score(x => x.Order(SortOrder.Desc))
            );

        if (!string.IsNullOrWhiteSpace(requestDto.SearchTerm))
        {
            searchRequest = searchRequest.Query(
                q => q.MultiMatch(mm => mm
                    .Fields(f => f.Title, f => f.Content, f => f.Tags)
                    .Query(requestDto.SearchTerm)
                    .Fuzziness("AUTO")
            ));
        }
        else
        {
            searchRequest.Query(q => q.MatchAll());
        }
        var documents = await client.SearchAsync<PostDocuemnt>(searchRequest);
        return [.. documents.Documents.Select(d => d.ToEntity().ToPostResponseDto())];
    }
}
