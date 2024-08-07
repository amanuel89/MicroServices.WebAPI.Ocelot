﻿
namespace RideBackend.Application.Services.GenericRepository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {

        bool Add(TEntity entity);
        Task<bool> AddAsync(TEntity entity);
        void AddAsyncTrans(TEntity entity);
        void AddRangeTrans(IEnumerable<TEntity> entities);
        bool AddRange(IEnumerable<TEntity> entities);
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);
        IQueryable<TEntity> All<TProperty>(params Expression<Func<TEntity, TProperty>>[] path);
        IQueryable<TEntity> All(params string[] path);
        long CountAll();
        Task<long> CountAllAsync();
        bool Exist(long id);
        Task<bool> ExistAsync(long id);
        bool ExistWhere(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ExistWhereAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(long id);
        Task<TEntity> FindAsync(long id);
        TEntity First<TProperty>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, TProperty>>[] path);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params string[] path);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params string[] path);
        bool Remove(TEntity entity);
        bool Update(TEntity entity);
         void UpdateTrans(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        IQueryable<TEntity> Where<TProperty>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, TProperty>>[] path);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, params string[] includes);
        IQueryable<TEntity> GetPagedQuerableAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate, params string[] includes);
        (IQueryable<TEntity> Data, int TotalCount) GetPagedDataWithTotalCount(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate, params string[] includes);

        IQueryable<TEntity> GetPagedOrderedQuerableAsync(  int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort, params string[] includes);
        List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        IQueryable<TEntity> Query();
        Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int pageNumber, int pageSize ,Expression<Func<TEntity, bool>> predicate, params string[] includes);
    }
}
