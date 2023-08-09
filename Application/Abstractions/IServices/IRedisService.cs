using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface IRedisService
{
    public string GetCachedDataAsync(string key);
}
