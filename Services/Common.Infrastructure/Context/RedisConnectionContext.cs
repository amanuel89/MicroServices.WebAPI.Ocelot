using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;

namespace IdentityServer.Infrastructure.Context
{
    public class RedisConnectionContext
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public RedisConnectionContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {

                var redisConnectionString = _configuration.GetSection("RedisConnectionStrings")["RideCache"];

                if (!string.IsNullOrEmpty(redisConnectionString))
                    {
                        return ConnectionMultiplexer.Connect(redisConnectionString);
                    }
                    else
                    {
                        throw new Exception("Cache not found");
                    }        
            });
        }

        public ConnectionMultiplexer Connection => _lazyConnection.Value;
    }
}
