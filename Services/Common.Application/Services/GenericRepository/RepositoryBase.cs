﻿namespace RideBackend.Application.Services.GenericRepository;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    ApplicationDbContext _context;
    public RepositoryBase(ApplicationDbContext context) => _context = context;
    public bool Add(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool AddRange(IEnumerable<TEntity> entities)
    {
        try
        {
            _context.Set<TEntity>().AddRange(entities);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
        if (await _context.SaveChangesAsync() > 0)
            return true;
        else
            return false;
    }
    public async Task<bool> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        if (await _context.SaveChangesAsync() > 0)
            return true;
        else
            return false;
    }
    public void AddAsyncTrans(TEntity entity)
    {
       _context.Set<TEntity>().AddAsync(entity);
    }
    public void AddRangeTrans(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
    }
    public void UpdateTrans(TEntity entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public bool Update(TEntity entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        if (_context.SaveChanges() > 0)
            return true;
        else
            return false;

    }
    public async Task<bool> UpdateAsync(TEntity entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        if (await _context.SaveChangesAsync() > 0)
            return true;
        else
            return false;
    }

    public bool Remove(TEntity entity)
    {
        _context.Remove(entity);
        if (_context.SaveChanges() > 0)
            return true;
        else
            return false;
    }

    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params string[] path)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (path != null)
        {
            foreach (var p in path)
            {
                query = query.Include(p);
            }
        }

        return query.FirstOrDefault(predicate);
    }

    public TEntity First<TProperty>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, TProperty>>[] path)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        foreach (var p in path)
        {
            query = query.Include(p);
        }

        return query.First(predicate);
    }

    public IQueryable<TEntity> Where<TProperty>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, TProperty>>[] path)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        foreach (var p in path)
        {
            query = query.Include(p);
        }
        return query.Where(predicate);
    }

    public IQueryable<TEntity> GetPagedQuerableAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate, params string[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        foreach (var i in includes)
        {
            query = query.Include(i);
        }
        if (pageSize == 0 && pageNumber == 0)
        {
            return  query.Where(predicate);
        }
        return  query.Where(predicate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking();
    }

    public (IQueryable<TEntity> Data, int TotalCount) GetPagedDataWithTotalCount(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate, params string[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        foreach (var i in includes)
        {
            query = query.Include(i);
        }

        var totalRecordCount = query.Count();  // Calculate the total record count before applying pagination

        if (pageSize == 0 && pageNumber == 0)
        {
            return (query.Where(predicate), totalRecordCount);  // Return all records without pagination
        }

        var pagedData = query.Where(predicate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking();

        return (pagedData, totalRecordCount);
    }


    public IQueryable<TEntity> GetPagedOrderedQuerableAsync(
    int pageNumber,
    int pageSize,
    Expression<Func<TEntity, bool>> predicate,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
    params string[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        foreach (var i in includes)
        {
            query = query.Include(i);
        }

        if (pageSize == 0 && pageNumber == 0)
        {
            return query.Where(predicate);
        }

        var sortedQuery = sort(query);

        return sortedQuery
            .Where(predicate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking();
    }


    public async Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate, params string[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        foreach (var i in includes)
        {
            query = query.Include(i);
        }
        if(pageSize==0 && pageNumber == 0)
        {
            return await query.Where(predicate)
                .ToListAsync();
        }
        return await query.Where(predicate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, params string[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        foreach (var i in includes)
        {
            query = query.Include(i);
        }
        return query.Where(predicate);
    }


    public IQueryable<TEntity> All<TProperty>(params Expression<Func<TEntity, TProperty>>[] path)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (path != null)
        {
            foreach (var p in path)
            {
                query = query.Include(p);
            }
        }
        return query;
    }

    public IQueryable<TEntity> All(params string[] path)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (path != null)
        {
            foreach (var p in path)
            {
                query = query.Include(p);
            }
        }
        return query;
    }

    public TEntity Find(long id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public async Task<TEntity> FindAsync(long id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params string[] path)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (path != null)
        {
            foreach (var p in path)
            {
                query = query.Include(p);
            }
        }
        return await query.FirstOrDefaultAsync(predicate);
    }

    public bool Exist(long id)
    {
        return (_context.Set<TEntity>().Find(id) != null ? true : false);
    }

    public async Task<bool> ExistAsync(long id)
    {
        return (await _context.Set<TEntity>().FindAsync(id) != null ? true : false);
    }

    public long CountAll()
    {
        return _context.Set<TEntity>().Count();
    }

    public async Task<long> CountAllAsync()
    {
        return await _context.Set<TEntity>().CountAsync();
    }

    public bool ExistWhere(Expression<Func<TEntity, bool>> predicate)
    {
        return (_context.Set<TEntity>().Where(predicate).FirstOrDefault() != null ? true : false);
    }

    public async Task<bool> ExistWhereAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return (await _context.Set<TEntity>().Where(predicate).FirstOrDefaultAsync() != null ? true : false);
    }


    public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        foreach (Expression<Func<TEntity, object>> include in includes)
            query = query.Include(include);

        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = orderBy(query);

        return query.ToList();
    }

    public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = orderBy(query);

        return query;
    }

    public IQueryable<TEntity> Query()
    {
        return _context.Set<TEntity>();
    }


}
