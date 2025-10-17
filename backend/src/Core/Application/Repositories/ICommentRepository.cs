using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repositories
{
    public interface ICommentRepository
    {
        public Task<ICollection<Comment>> GetByPostIdAsync(Guid PostId);
        public Task<ICollection<Comment>> GetByParentCommentIdAsync(string CommentId);
    }
}