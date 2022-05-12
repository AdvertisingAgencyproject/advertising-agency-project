using System.Linq.Expressions;
using AdvertisingAgency.DAL.Entities;

namespace AdvertisingAgency.DAL.Interfaces;

public interface IRepository<TModel> where TModel : BaseEntity
{
    Task<bool> InsertAsync(TModel entity);
    Task<TModel> GetSingleByExpressionAsync(Expression<Func<TModel, bool>> expression);
    Task<List<TModel>> GetManyByExpressionAsync(Expression<Func<TModel, bool>> expression);
    Task<TModel> GetByIdAsync(string id);
    Task<List<TModel>> GetAllAsync();
    Task<bool> UpdateAsync(TModel entity);
    Task<bool> DeleteAsync(TModel entity);
}