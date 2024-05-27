using ConsigneeService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

public interface IDapperReadOnlyRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(long id);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null);
    Task<(IEnumerable<TEntity>, int)> GetPageAsync(int pageNumber, int pageSize, string orderBy, SortDirection sortDirection, string param, object parameters = null, List<string> includes = null);

}
