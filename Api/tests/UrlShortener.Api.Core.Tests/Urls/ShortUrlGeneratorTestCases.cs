using UrlShortener.Core.Urls.Add;
using UrlShortener.Core;

namespace UrlShortener.Api.Core.Tests.Urls;

public class ShortUrlGeneratorTestCases
{
    [Fact]
    public void When1001GivenShouldReturnShortUrl()
    {
        var tokenProvider = new TokenProvider();
        tokenProvider.AssignRange(10001, 20000);
        
        var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);
        var shortUrl = shortUrlGenerator.GenerateUniqueUrl();

        shortUrl.Should().Be("2bJ");
    }
    [Fact]
    public void When0GivenShouldReturnShortUrl()
    {
        var tokenProvider = new TokenProvider();
        tokenProvider.AssignRange(0, 10);
        
        var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);
        var shortUrl = shortUrlGenerator.GenerateUniqueUrl();

        shortUrl.Should().Be("0");
    }
}
