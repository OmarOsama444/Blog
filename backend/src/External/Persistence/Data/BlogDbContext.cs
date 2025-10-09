using Microsoft.EntityFrameworkCore;
using Application.Abstractions;
using Domain.Entities.Outbox;
using Domain.Entities;

namespace Persistence.Data;

public class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options), IUnitOfWork
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<OutboxMessage> OutboxMessages { get; set; }
    public virtual DbSet<OutboxConsumerMessage> OutboxConsumerMessages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyRefrence.Assembly);
    }
}
