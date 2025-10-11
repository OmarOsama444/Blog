using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Infrastructure.Clients;
using Infrastructure.Dtos.Responses;

namespace Infrastructure.Services
{
    public class EmbeddingService(SemanticModelClient semanticModelClient) : IEmbeddingService
    {
        public async Task<ICollection<float>> GenerateEmbedingFromText(string text)
        {
            EmbedingResponseRepresentation response = await semanticModelClient.GenerateEmbeding(text);
            return response.Embedding;
        }
    }
}