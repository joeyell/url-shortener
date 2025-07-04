using UrlShortener.Core;
using UrlShortener.Core.Urls.Add;

namespace UrlShortener.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUrlFeature(this IServiceCollection services)
    {
        services.AddScoped<AddUrlHandler>();
        services.AddScoped<ShortUrlGenerator>();
        
        services.AddSingleton<TokenProvider>(_ =>
        {
            var tokenProvider = new TokenProvider();
            tokenProvider.AssignRange(1, 1000);
            return tokenProvider;
        });
        
        return services;
    }
}

public class InMemoryUrlDataStore : Dictionary<string, ShortenedUrl>, IUrlDataStore
{
    public Task AddAsync(ShortenedUrl shortened, CancellationToken cancellationToken)
    {
        Add(shortened.ShortUrl, shortened);
        return Task.CompletedTask;
    }
}