using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICommentService
    {
        public Task<CommentResponseDto> CreateComment(Guid UserId, CreateCommentRequestDto request, CancellationToken cancellationToken);
        public Task<ICollection<CommentResponseDto>> GetCommentsByPostId(Guid PostId, CancellationToken cancellationToken);
        public Task<ICollection<CommentResponseDto>> GetRepliesByCommentId(string CommentId, CancellationToken cancellationToken);
    }
}