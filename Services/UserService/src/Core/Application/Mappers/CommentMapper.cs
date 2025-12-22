using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Mappers
{
    public static class CommentMapper
    {
        public static CommentResponseDto ToCommentResponseDto(this Comment comment)
        {
            return new CommentResponseDto
            {
                Id = comment.Id,
                Name = comment.UserFullName,
                ParentId = comment.ParentId,
                Content = comment.Content,
                UserId = comment.UserId,
                PostId = comment.PostId,
                CreatedOnUtc = comment.CreatedOnUtc
            };
        }
    }
}