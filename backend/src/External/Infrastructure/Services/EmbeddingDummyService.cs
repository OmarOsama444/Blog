using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Infrastructure.Services
{
    public class EmbeddingDummyService : IEmbeddingService
    {
        public async Task<ICollection<float>> GenerateEmbedingFromText(string text)
        {
            return [.. Enumerable.Repeat<float>(0.0f, 768)];
        }
    }
}