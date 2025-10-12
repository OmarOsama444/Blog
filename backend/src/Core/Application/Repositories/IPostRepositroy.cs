using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IPostRepositroy
    {
        public Task<ICollection<PostResponseDto>> SearchByFullText(string? Text, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    }
}