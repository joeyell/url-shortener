using UrlShortener.Core;

namespace UrlShortener.Api.Core.Tests;

public class Base62EncodingTestCases
{
    [Theory]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(20, "K")]
    [InlineData(8317141, "YtfR")]
    [InlineData(421124411, "SUzhD")]
    public void WhenNumberGivenEncodeToBase62(long number, string expected)
    {
        number.EncodeToBase62()
            .Should()
            .Be(expected);
    }
}
