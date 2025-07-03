using Microsoft.Extensions.Time.Testing;
using UrlShortener.Api.Core.Tests.TestDoubles;
using UrlShortener.Core;
using UrlShortener.Core.Urls.Add;

namespace UrlShortener.Api.Core.Tests.Urls;

public class AddUrlTestCases
{
    private readonly AddUrlHandler _handler;
    private readonly InMemoryDataStore _urlDataStore = new();
    private readonly FakeTimeProvider _timeProvider;

    public AddUrlTestCases()
    {
        var tokenProvider = new TokenProvider();
        tokenProvider.AssignRange(1, 5);
        var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);
        _timeProvider = new FakeTimeProvider();
        _handler = new AddUrlHandler(shortUrlGenerator, _urlDataStore, _timeProvider);
    }
    [Fact]
    public async Task WhenRequestReceivedReturnShortenedUrl()
    {

        var request = CreateAddUrlRequest();

        var response = await _handler.HandleAsync(request, default);

        response.Value!.ShortUrl.Should().NotBeEmpty();
        response.Value!.ShortUrl.Should().Be("1");
    }

    [Fact]
    public async Task WhenRequestReceivedSaveShortenedUrl()
    {
        var request = CreateAddUrlRequest();

        var response = await _handler.HandleAsync(request, default);

        response.Succeeded.Should().BeTrue();
        _urlDataStore.Should().ContainKey(response.Value!.ShortUrl);
    }

    [Fact]
    public async Task WhenRequestReceivedShouldErrorIfCreatedByIsEmpty()
    {
        var request = CreateAddUrlRequest(createdBy: string.Empty);
        
        var response = await _handler.HandleAsync(request, default);

        response.Succeeded.Should().BeFalse();
        response.Error.Code.Should().Be("missing_value");
    }
    
    [Fact]
    public async Task WhenRequestReceivedSaveShortenedUrlWithCreatedByCreatedOnData()
    {
        var request = CreateAddUrlRequest();

        var response = await _handler.HandleAsync(request, default);

        response.Succeeded.Should().BeTrue();
        _urlDataStore.Should().ContainKey(response.Value!.ShortUrl);
        _urlDataStore[response.Value!.ShortUrl].CreatedBy.Should().Be(request.CreatedBy);
        _urlDataStore[response.Value!.ShortUrl].CreatedOn.Should().Be(_timeProvider.GetUtcNow());
    }

    private static AddUrlRequest CreateAddUrlRequest(string createdBy = "joe@joe.com")
    {
        return new AddUrlRequest(new Uri("https://dometrain.com"), createdBy);
    }
}
