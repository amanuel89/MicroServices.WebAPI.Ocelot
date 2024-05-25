//using System;
//using System.Collections.Frozen;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using Dapper;
//using RideBackend.Domain.Common;

//public class DapperReadOnlyRepository<TEntity> : IDapperReadOnlyRepository<TEntity> where TEntity : class
//{
//    private readonly IDbConnection _connection;

//    public DapperReadOnlyRepository(IDbConnection connection)
//    {
//        _connection = connection;
//    }

//    public async Task<IEnumerable<TEntity>> GetAllAsync()
//    {
//        var enity = GetEntityName();
//        return await _connection.QueryAsync<TEntity>($"SELECT * FROM \"{enity}\"");
//    }


//    public async Task<TEntity> GetByIdAsync(long id)
//    {
//        var enity = GetEntityName();
//        return await _connection.QueryFirstOrDefaultAsync<TEntity>($"SELECT * FROM \"{enity}\" WHERE \"Id\" = @Id", new { Id = id });
//    }



//    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
//    {
//        var tableName = GetEntityName();
//        var sqlExpression = GetSqlExpression(predicate);
//        var query = $"SELECT * FROM \"{tableName}\" WHERE {sqlExpression}";
//        return await _connection.QueryAsync<TEntity>(query);
//    }


//    public async Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null)
//    {
//        return await _connection.QueryAsync<TEntity>(query, parameters);
//    }

//    private string GetEntityName()
//    {
//        return typeof(TEntity).Name;
//    }

//    private string GetSqlExpression(Expression<Func<TEntity, bool>> predicate)
//    {
//        return string.Join(" AND ", ((BinaryExpression)predicate.Body).Left, ((BinaryExpression)predicate.Body).Right);
//    }
//    public async Task<(IEnumerable<TEntity>, int)> GetPageAsync(int pageNumber, int pageSize, string orderBy, SortDirection sortDirection, string param, object parameters = null, List<string> includes = null)
//    {
//        var entity = GetEntityName();
//        var sql = $"SELECT * FROM {entity}";

//        // Add includes to the SQL query
//        if (includes != null && includes.Any())
//        {
//            int aliasCounter = 1; // Counter for generating correlation names
//            foreach (var include in includes)
//            {
//                // Generate correlation name for the included entity
//                var correlationName = $"{include}{aliasCounter}";
//                sql += $" LEFT JOIN {include} AS {correlationName} ON {entity}.ParentId = {correlationName}.Id";
//                aliasCounter++;
//            }
//        }

//        if (!string.IsNullOrEmpty(param))
//        {
//            sql += $" WHERE  {param.Normalize()}";
//        }

//        if (pageNumber <= 0 || pageSize <= 0)
//        {
//            var data = await _connection.QueryAsync<TEntity>(sql + $" ORDER BY {orderBy} {sortDirection}", parameters);

//            var count = await _connection.QuerySingleAsync<int>($"SELECT COUNT(*) FROM {entity}", parameters);

//            return (data, count);
//        }

//        int offset = (pageNumber - 1) * pageSize;
//        sql += $" ORDER BY {orderBy} {sortDirection} OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

//        var parametersDict = new System.Collections.Generic.Dictionary<string, object>();

//        // Add all parameters to the dictionary
//        if (parameters != null)
//        {
//            var properties = parameters.GetType().GetProperties();
//            foreach (var property in properties)
//            {
//                parametersDict[property.Name] = property.GetValue(parameters, null);
//            }
//        }

//        // Add Offset and PageSize to the parameters dictionary
//        parametersDict["Offset"] = offset;
//        parametersDict["PageSize"] = pageSize;
//        var pagedData = await _connection.QueryAsync<TEntity>(sql, parametersDict);

//        var countSql = $"SELECT COUNT(*) FROM {entity}";
//        if (!string.IsNullOrEmpty(param))
//        {
//            countSql += $" WHERE {param}";
//        }

//        var totalCount = await _connection.QuerySingleAsync<int>(countSql, parameters);
//        return (pagedData, totalCount);
//    }
//}
