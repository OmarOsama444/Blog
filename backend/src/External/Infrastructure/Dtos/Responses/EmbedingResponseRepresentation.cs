using System.Text.Json.Serialization;

namespace Infrastructure.Dtos.Responses
{
    public class EmbedingResponseRepresentation
    {
        [JsonPropertyName("embedding")]
        public ICollection<float> Embedding { get; set; } = [];
    }
}