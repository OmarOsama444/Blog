namespace Infrastructure.ElasticSearch.Docuemnts;

public class PostDocuemnt
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string[] Tags { get; set; } = [];
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOnUtc { get; set; }
    public float[] Embedding { get; set; } = [];
    public float Rating { get; set; }
    public uint TotalUsersRated { get; set; }
}
