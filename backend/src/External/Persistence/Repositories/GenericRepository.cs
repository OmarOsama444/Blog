using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class GenericRepository<T, TKey>(ChatDbContext context) : IGenericRepository<T, TKey> where T : class
{
    private readonly ChatDbContext _context = context;
    public async Task<T?> GetById(TKey id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }
    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
}
