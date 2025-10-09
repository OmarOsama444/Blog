using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Responses;

namespace Application.Interfaces
{
    public interface IAiService
    {
        public Task<AiChatHistoryResponse> SendToAi(Guid FromId, Guid? AiChatId, string Message, CancellationToken token = default);
        public Task<ICollection<AiChatHistoryResponse>> Fetch(Guid FromId, Guid AiChatId, int? Total, string? LastId = null, CancellationToken token = default);
    }
}