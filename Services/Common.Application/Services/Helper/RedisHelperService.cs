using IdentityServer.Infrastructure.Context;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace IdentityServer.Application.Services
{
    public interface IRedisHelper
    {
        string? ReadTokenFromCache(string key);
        string WriteTokenToCache(string value,DateTime expiryTime);
        string WriteToCache(string key, string value);
        bool DeleteTokenFromCache(string key);
        List<string> GetAllFromRedis();
       void SetExpiration(string key, TimeSpan expiration);
    }

    public class RedisHelper : IRedisHelper
    {
        private readonly RedisConnectionContext _redisConnectionContext;

        public RedisHelper(RedisConnectionContext redisConnectionContext)
        {
            _redisConnectionContext = redisConnectionContext;
        }

        private IDatabase RedisDatabase => _redisConnectionContext.Connection.GetDatabase();

        public string ReadTokenFromCache(string key)
        {
            RedisValue value = RedisDatabase.StringGet(key);
            return value.HasValue ? value.ToString() : string.Empty;
        }

        public List<string> GetAllFromRedis()
        {
            var allKeys = RedisDatabase.Multiplexer.GetServer(RedisDatabase.Multiplexer.GetEndPoints()[0]).Keys();
            var result = new List<string>();

            foreach (var key in allKeys)
            {
                if (key.ToString().Contains("DRIVER-LOCATION_"))
                {
                    RedisValue value = RedisDatabase.StringGet(key);
                    if (!value.IsNull)
                    {
                        result.Add(value.ToString());
                    }
                }
            }

            return result;
        }


        public string WriteTokenToCache(string value, DateTime expiryTime)
        {
            string key = GenerateReferenceKey();
            RedisDatabase.StringSet(key, value);
            TimeSpan timeUntilExpiry = expiryTime - DateTime.UtcNow;

            if (timeUntilExpiry.TotalSeconds > 0)
                RedisDatabase.KeyExpire(key, timeUntilExpiry);
           

            return key;
        }

        public string WriteToCache(string key, string value )
        {
          
                RedisDatabase.StringSet(key, value);
         

            return value;
        }
        public void SetExpiration(string key, TimeSpan expiration)
        {
            RedisDatabase.KeyExpire(key, expiration, CommandFlags.None);
        }

        public bool DeleteTokenFromCache(string key)
        {
            return RedisDatabase.KeyDelete(key);
        }

        private string GenerateReferenceKey()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
