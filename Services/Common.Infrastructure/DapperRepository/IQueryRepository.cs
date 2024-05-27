


//using Dapper;
//using CommonService.Domain.Common;
//using System.Data;
//using System.Data.Common;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

//public interface IQueryRepository<TEntity> where TEntity : class
//{
//    IEnumerable<TEntity> All(RecordStatus? s = null);
//    Paginated<TEntity> All(int page, int itemsPerPage, RecordStatus? s = null);
//    IEnumerable<TEntity> All(string whereClause, object parameters = null);
//    int CountAll();
//    bool Exists(object id);
//    bool Exists(string whereClause, object parameters = null);
//    TEntity Find(object id);
//    TEntity FirstOrDefault(string whereClause, object parameters = null);
//    IEnumerable<TEntity> Query(string query, object parameters);
//    int CountAll(RecordStatus? status = null);
//    public void GetPaginatedResponce(int page, int itemsPerPage, RecordStatus? status, ref Paginated<TEntity> paginatedResult);
//    //string GetQuery(object id, string[] includeProperties, string[] selectAttributes, Type[] type);
//}
//public class QueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class
//{
//    private readonly IDbConnection _dbConnection;
//    public QueryRepository(IDbConnection connection)
//    {
//        _dbConnection = connection;
//    }
//    public IEnumerable<TEntity> All(RecordStatus? s = null)
//    {
//        string query = $"SELECT * FROM {typeof(TEntity).Name}";
//        if (s.HasValue)
//            query += " WHERE RecordStatus = @Status";
//        else
//            query += " WHERE RecordStatus <> @DeletedStatus";
//        var parameters = new { Status = (int?)s, DeletedStatus = (int)RecordStatus.Deleted };
//        return _dbConnection.Query<TEntity>(query, parameters);
//    }
//    public Paginated<TEntity> All(int page, int itemsPerPage, RecordStatus? s = null)
//    {
//        var result = new Paginated<TEntity>();
//        int itemsToSkip = (page - 1) * itemsPerPage;
//        string query = $"SELECT * FROM {typeof(TEntity).Name}";
//        if (s.HasValue)
//            query += " WHERE RecordStatus = @Status";
//        else
//            query += " WHERE RecordStatus <> @DeletedStatus";
//        query += " ORDER BY Id OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
//        var parameters = new { Skip = itemsToSkip, Take = itemsPerPage, Status = (int?)s, DeletedStatus = (int)RecordStatus.Deleted };
//        result.Data = _dbConnection.Query<TEntity>(query, parameters);

//        int totalCount = 0;
//        if (s.HasValue)
//            totalCount = _dbConnection.Query<int>($"SELECT COUNT(*) FROM {typeof(TEntity).Name} WHERE RecordStatus = @Status", new { Status = (int?)s }).FirstOrDefault();
//        else
//            totalCount = _dbConnection.Query<int>($"SELECT COUNT(*) FROM {typeof(TEntity).Name} WHERE RecordStatus <> @DeletedStatus", new { DeletedStatus = (int)RecordStatus.Deleted }).FirstOrDefault();

//        result.TotalCount = totalCount;
//        result.TotalPage = (int)Math.Ceiling((double)totalCount / itemsPerPage) == 0 ? 1 : (int)Math.Ceiling((double)totalCount / itemsPerPage);
//        result.CurrentPage = page;
//        result.PageSize = result.Data.Count();
//        result.FirstPage = 1;
//        result.LastPage = result.TotalPage;
//        return result;
//    }
//    public IEnumerable<TEntity> All(string whereClause, object parameters = null)
//    {
//        return _dbConnection.Query<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE {whereClause}", parameters);
//    }
//    public int CountAll()
//    {
//        return _dbConnection.Query<int>($"SELECT COUNT(*) FROM {typeof(TEntity).Name}").FirstOrDefault();
//    }
//    public bool Exists(object id)
//    {
//        return _dbConnection.ExecuteScalar<int>($"SELECT COUNT(*) FROM {typeof(TEntity).Name} WHERE Id = @Id", new { Id = id }) > 0;
//    }
//    public bool Exists(string whereClause, object parameters = null)
//    {
//        return _dbConnection.ExecuteScalar<int>($"SELECT COUNT(*) FROM {typeof(TEntity).Name} WHERE {whereClause}", parameters) > 0;
//    }
//    public TEntity Find(object id)
//    {
//        return _dbConnection.Query<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE Id = @Id  AND RecordStatus <> @DeletedStatus ",
//            new
//            {
//                Id = id,
//                DeletedStatus = (int)RecordStatus.Deleted
//            }).FirstOrDefault();
//    }
//    public TEntity FirstOrDefault(string whereClause, object parameters = null)
//    {
//        return _dbConnection.Query<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE {whereClause} AND RecordStatus <> {(int)RecordStatus.Deleted} ", parameters).FirstOrDefault();
//    }
//    public IEnumerable<TEntity> Query(string query, object parameters)
//    {
//        return _dbConnection.Query<TEntity>(query, parameters);
//    }
//    public int CountAll(RecordStatus? status = null)
//    {
//        if (status.HasValue)
//            return _dbConnection.Query<int>($"SELECT COUNT(*) FROM {typeof(TEntity).Name} WHERE RecordStatus = @Status", new { Status = (int?)status }).FirstOrDefault();
//        else
//            return _dbConnection.Query<int>($"SELECT COUNT(*) FROM {typeof(TEntity).Name} WHERE RecordStatus <> @DeletedStatus", new { DeletedStatus = (int)RecordStatus.Deleted }).FirstOrDefault();
//    }

//    public void GetPaginatedResponce(int page,int itemsPerPage,RecordStatus? status,ref Paginated<TEntity> paginatedResult)
//    {
//        int totalCount = 0;
//        if (status.HasValue)
//            totalCount = _dbConnection.Query<int>($"SELECT COUNT(*) FROM {typeof(TEntity).Name} WHERE RecordStatus = @Status", new { Status = (int?)status }).FirstOrDefault();
//        else
//            totalCount = _dbConnection.Query<int>($"SELECT COUNT(*) FROM {typeof(TEntity).Name} WHERE RecordStatus <> @DeletedStatus", new { DeletedStatus = (int)RecordStatus.Deleted }).FirstOrDefault();

//        paginatedResult.TotalCount = totalCount;
//        paginatedResult.TotalPage = (int)Math.Ceiling((double)totalCount / itemsPerPage) == 0 ? 1 : (int)Math.Ceiling((double)totalCount / itemsPerPage);
//        paginatedResult.CurrentPage = page;
//        paginatedResult.PageSize = paginatedResult.Data.Count();
//        paginatedResult.FirstPage = 1;
//        paginatedResult.LastPage = paginatedResult.TotalPage;
//    }

//    //public string GetQuery(object? id, string[] includeProperties, string[] selectAttributes, Type[] type)
//    //{
//    //    var joinClauses = includeProperties != null && includeProperties.Any()
//    //        ? string.Join(" ", includeProperties.Select(p => $"LEFT JOIN [{p}] ON [{typeof(TEntity).Name}].[{p}Id] = [{p}].[Id]"))
//    //    : string.Empty;

//    //    includeProperties.Append($"{typeof(TEntity).Name}");
//    //    string selectColumns = "";
//    //    if (selectAttributes != null && selectAttributes.Any())
//    //    {
//    //        selectColumns = AnyAttributeExists(type, selectAttributes);
//    //    }
//    //    else
//    //    {
//    //        selectColumns = includeProperties != null && includeProperties.Any()
//    //? $"[{typeof(TEntity).Name}].*, {string.Join(", ", includeProperties.Select(p => $"[{p}].*"))}"
//    //: $"[{typeof(TEntity).Name}].*";
//    //    }
//    //    var query = "";
//    //    query = $"SELECT {selectColumns} FROM {typeof(TEntity).Name} {joinClauses} ";

//    //    return query;
//    //}

//    //public string AnyAttributeExists(Type[] entityTypes, string[] attributeNames)
//    //{
//    //    var attributes = "";
//    //    foreach (var entityType in entityTypes)
//    //    {
//    //        var isExist = false;
//    //        foreach (var attributeName in attributeNames)
//    //        {
//    //            var propertyInfo = entityType.GetProperty(attributeName);

//    //            if (propertyInfo != null)
//    //            {
//    //                attributes += $"[{entityType.Name}].{propertyInfo.Name} AS {entityType.Name}_{propertyInfo.Name}, ";
//    //                isExist = true ;
//    //            }
//    //        }
//    //        if (!isExist)
//    //        {
//    //            attributes += $"[{entityType.Name}].*, ";
//    //        }
//    //    }

//    //    return attributes;
//    //}

    
//}

