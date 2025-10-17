using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class CommentRepository(AppDbContext appDbContext) : ICommentRepository
    {
        public async Task<ICollection<Comment>> GetByParentCommentIdAsync(string CommentId)
        {
            return await appDbContext.Comments
                .Where(c => c.ParentId == CommentId)
                .ToListAsync();
        }

        public async Task<ICollection<Comment>> GetByPostIdAsync(Guid PostId)
        {
            return await appDbContext.Comments
                .Where(c => c.PostId == PostId)
                .ToListAsync();
        }
    }
}