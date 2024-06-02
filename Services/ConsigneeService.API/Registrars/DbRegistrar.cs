//using Microsoft.Extensions.DependencyInjection;
//using System.Data;
//using ConsigneeService.Domain.Models;
//using System.Data.SqlClient;

//namespace ConsigneeService.API.Registrars
//{
//    public class DbRegistrar : IWebApplicationBuilderRegistrar
//    {
//        public void RegisterServices(WebApplicationBuilder builder)
//        {
//            var connectionString = Environment.GetEnvironmentVariable("PGSQL_DB_HOST");
//#if DEBUG
//            connectionString = string.Empty;
//#endif
//            if (string.IsNullOrEmpty(connectionString))
//                connectionString = builder.Configuration.GetConnectionString("Default");

//            // Register IDbConnection for dapper
//            //builder.Services.AddTransient<IDbConnection>(provider =>
//            //{
//            //    return new NpgsqlConnection(connectionString);
//            //});

//            //// Register ApplicationDbContext
//            //builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

//            builder.Services.AddTransient<IDbConnection>(provider =>
//            {
//                var connectionString = builder.Configuration.GetConnectionString("Default");
//                return new SqlConnection(connectionString);
//            });

//            // Register ApplicationDbContext
//            builder.Services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(connectionString)
//            );



//            // Register Dapper Repository for Read
//           // builder.Services.AddScoped<IDapperReadOnlyRepository<Address>, DapperReadOnlyRepository<Address>>();
//          //  builder.Services.AddScoped<IDapperReadOnlyRepository<Passenger>, DapperReadOnlyRepository<Passenger>>();

//            //    builder.Services.AddScoped<IQueryRepository<Address>, QueryRepository<Address>>();
//            //builder.Services.AddScoped<IQueryRepository<Passenger>, QueryRepository<Passenger>>();
//        }
//    }
//}
namespace ConsigneeService.API.Registrars
{
    public class DbRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("MSSQLDB_HOST");
#if DEBUG
            connectionString = string.Empty;
#endif
            if (string.IsNullOrEmpty(connectionString))
                connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
