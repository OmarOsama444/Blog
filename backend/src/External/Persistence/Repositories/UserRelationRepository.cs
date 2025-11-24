using Application.Repositories;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UserRelationRepository(AppDbContext context) : IUserRelationRepository
    {
        public async Task<int> CountMutualFriends(Guid userA, Guid userB)
        {
            var result = from realtion1 in context.UserRelations
                         join realtion2 in context.UserRelations
                         on realtion1.FromId equals realtion2.FromId
                         where realtion1.ToId == userA && realtion2.ToId == userB
                         select realtion1.FromId;
            return await result.CountAsync();
        }

        public async Task<UserRelation?> GetByFromIdAndToIdAndRelation(Guid FromId, Guid ToId, RelationType relationType)
        {
            return await context.UserRelations
            .Where(x => x.FromId == FromId && x.ToId == ToId && x.Relation == relationType)
            .FirstOrDefaultAsync();
        }

        public async Task<UserRelation?> GetByFromIdAndToIdAndRelationForUpdate(Guid FromId, Guid ToId, RelationType relationType)
        {
            var sql = @"
                SELECT * 
                FROM ""user_relations""
                WHERE ""from_id"" = {0} AND ""to_id"" = {1} AND ""relation"" = {2}
                FOR UPDATE";

            return await context.UserRelations
                .FromSqlRaw(sql, FromId, ToId, relationType)
                .FirstOrDefaultAsync();
        }
    }
}