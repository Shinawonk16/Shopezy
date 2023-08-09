using StackExchange.Redis;

namespace Persistence.Caching;

public class RedisCacheService
{
    private readonly ConnectionMultiplexer _connection;

        public RedisCacheService(string connectionString)
        {
            _connection = ConnectionMultiplexer.Connect(connectionString);
        }

        public void Set(string key, string value, TimeSpan expiry)
        {
            IDatabase redisDb = _connection.GetDatabase();
            redisDb.StringSet(key, value, expiry);
        }

        public string Get(string key)
        {
            IDatabase redisDb = _connection.GetDatabase();
            return redisDb.StringGet(key);
        }
}
