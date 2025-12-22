using Application.Abstractions;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class CommentService(IGenericRepository<Comment, string> CommentRepo, IGenericRepository<User, Guid> UserRepo, ICommentRepository commentRepository, IUnitOfWork unitOfWork) : ICommentService
    {
        public async Task<CommentResponseDto> CreateComment(Guid UserId, CreateCommentRequestDto request, CancellationToken cancellationToken)
        {
            var user = await UserRepo.GetById(UserId) ?? throw new NotFoundException("User.NotFound", UserId);
            if (request.ParentCommentId is not null)
            {
                var parentComment = await CommentRepo.GetById(request.ParentCommentId) ?? throw new NotFoundException("Comment.ParentCommentNotFound", request.ParentCommentId)
                    ?? throw new NotFoundException("Comment.Parent.NotFound", request.ParentCommentId);
                if (parentComment.PostId != request.PostId)
                {
                    throw new BadRequestException("Comment.Parent.Post.MisMatch", request.ParentCommentId);
                }
            }
            var comment = Comment.Create(
                request.Content,
                user,
                request.PostId,
                request.ParentCommentId
            );
            CommentRepo.Add(comment);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return comment.ToCommentResponseDto();
        }

        public async Task<ICollection<CommentResponseDto>> GetCommentsByPostId(Guid PostId, CancellationToken cancellationToken)
        {
            var comments = await commentRepository.GetByPostIdParentOnlyAsync(PostId);
            return [.. comments.Select(x => x.ToCommentResponseDto())];
        }

        public async Task<ICollection<CommentResponseDto>> GetRepliesByCommentId(string CommentId, CancellationToken cancellationToken)
        {
            var replies = await commentRepository.GetByParentCommentIdAsync(CommentId);
            return [.. replies.Select(x => x.ToCommentResponseDto())];
        }
    }
}