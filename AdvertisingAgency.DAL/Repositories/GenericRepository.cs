using System.Linq.Expressions;
using AdvertisingAgency.DAL.Entities;
using AdvertisingAgency.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.DAL.Repositories;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly DataContext _context;
    private DbSet<T> _entities;

    public GenericRepository(DataContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    private IQueryable<T> QueryWithNavigationFields()
    {
        var query = _context.Set<T>().AsQueryable();
        
        var navigations = _context.Model.FindEntityType(typeof(T))?
            .GetDerivedTypesInclusive()
            .SelectMany(t => t.GetNavigations())
            .Distinct();

        if (navigations != null)
        {
            query = navigations.Aggregate(query, (current, property) 
                => current.Include(property.Name));
        }

        return query;
    }
    
    public async Task<bool> InsertAsync(T entity)
    {
        await _entities.AddAsync(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<T> GetSingleByExpressionAsync(Expression<Func<T, bool>> expression) 
        => await QueryWithNavigationFields().AsNoTracking()
                                            .SingleOrDefaultAsync(expression);

    public async Task<List<T>> GetManyByExpressionAsync(Expression<Func<T, bool>> expression) 
        => await QueryWithNavigationFields().AsNoTracking()
                                            .Where(expression)
                                            .ToListAsync();

    public Task<T> GetByIdAsync(string id) => QueryWithNavigationFields().SingleOrDefaultAsync(t => t.Id == id);

    public Task<List<T>> GetAllAsync() => QueryWithNavigationFields().AsNoTracking()
                                                                     .ToListAsync();

    public async Task<bool> UpdateAsync(T entity)
    {
        var oldEntity = await QueryWithNavigationFields().SingleOrDefaultAsync(t => t.Id == entity.Id);
        _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _entities.Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}