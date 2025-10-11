using Application.Abstractions;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace Persistence.Repositories;

public class GenericRepository<T, TKey>(BlogDbContext context, ILogger<GenericRepository<T, TKey>> logger) : IGenericRepository<T, TKey> where T : class
{
    private readonly BlogDbContext _context = context;
    private readonly ILogger<GenericRepository<T, TKey>> _logger = logger;
    public async Task<T?> GetById(TKey id)
    {
        _logger.LogInformation("Fetching entity of type {EntityType} with Id {Id}", typeof(T).Name, id);
        var entity = await _context.Set<T>().FindAsync(id);
        _logger.LogInformation("Fetched entity of type {EntityType}: {@Result}", typeof(T).Name, entity);
        return entity;
    }
    public void Add(T entity)
    {
        _logger.LogInformation("Adding entity of type {EntityType}: {@Entity}", typeof(T).Name, entity);
        _context.Set<T>().Add(entity);
    }
    public void Remove(T entity)
    {
        _logger.LogInformation("Removing entity of type {EntityType}: {@Entity}", typeof(T).Name, entity);
        _context.Set<T>().Remove(entity);
    }
    public void Update(T entity)
    {
        _logger.LogInformation("Updating entity of type {EntityType}: {@Entity}", typeof(T).Name, entity);
        _context.Set<T>().Update(entity);
    }
}
