using UrlShortener.Core;

namespace UrlShortener.Api.Core.Tests;

public class TokenRangeTestCases
{
    [Fact]
    public void WhenStartTokenIsGreaterThanEndTokenExceptionIsThrown()
    {
        var action = () => new TokenRange(1000, 0);

        action.Should()
            .Throw<ArgumentException>().WithMessage("Token range End must be greater than Start");
    }
}
