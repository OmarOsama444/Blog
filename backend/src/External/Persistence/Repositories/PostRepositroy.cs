using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Responses;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class PostRepositroy(AppDbContext context) : IPostRepositroy
    {
        public async Task<Post?> GetBySlug(string slug, CancellationToken cancellationToken = default)
        {
            return await context.Posts.FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
        }

        public async Task<ICollection<PostResponseDto>> SearchByFullText(string? Text, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            IQueryable<Post> query = context
                .Posts;
            if (!string.IsNullOrEmpty(Text))
            {
                query = query.Where(x =>
                    EF.Functions.ToTsVector("english", x.Title + " " + x.Content)
                        .Matches(
                            EF.Functions.PhraseToTsQuery("english", Text)
                        )
                    );
            }
            return await query
                .Select(x => new PostResponseDto
                {
                    Id = x.Id,
                    Slug = x.Slug,
                    Title = x.Title,
                    CreatedOnUtc = x.CreatedOnUtc,
                    Tags = x.Tags
                })
                .Take(pageSize)
                .Skip((pageNumber - 1) * pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}