using Domain.Entities;
using Infrastructure.ElasticSearch.Docuemnts;

namespace Infrastructure.Mappers;

public static class PostMapper
{
    public static PostDocuemnt ToDocument(this Post post)
    {
        return new PostDocuemnt()
        {
            Id = post.Id.ToString(),
            Title = post.Title,
            UserId = post.UserId.ToString(),
            Content = post.Content,
            CreatedOnUtc = post.CreatedOnUtc,
            Slug = post.Slug,
            Tags = [.. post.Tags],
            Embedding = [.. post.Embeddings]
        };
    }

    public static Post ToEntity(this PostDocuemnt document)
    {
        return new Post
        {
            Id = Guid.Parse(document.Id),
            Title = document.Title,
            UserId = Guid.Parse(document.UserId),
            Content = document.Content,
            CreatedOnUtc = document.CreatedOnUtc,
            Slug = document.Slug,
            Tags = [.. document.Tags],
            Embeddings = [.. document.Embedding]
        };
    }
}
