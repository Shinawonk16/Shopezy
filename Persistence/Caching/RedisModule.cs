// using System.Reflection;
using Autofac;

namespace Persistence.Caching;

public class RedisModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Replace "your_redis_connection_string" with your actual Redis connection string
        string connectionString = "server=localhost;user=root;port=3306;database=RenewedShope;password=1234";
        builder.RegisterType<RedisCacheService>()
            .AsSelf()
            .WithParameter("connectionString", connectionString)
            .SingleInstance();
    }
}
