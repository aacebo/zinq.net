using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Zinq.Contexts.Extensions.DependencyInjection;
using Zinq.Contexts.Extensions.Logging;

namespace Zinq.Contexts.Extensions.Logging.Tests;

public class LoggingTests
{
    [Fact]
    public void Should_HaveLogger()
    {
        var provider = new ServiceCollection()
            .AddLogging(builder => builder.AddConsole())
            .AddContext((provider, builder) => builder.WithLogger(provider))
            .BuildServiceProvider();

        var context = provider.GetRequiredService<IReadOnlyContext>();

        Assert.True(context.Has(Keys.Logger));
        Assert.IsAssignableFrom<ILogger>(context.Logger);
    }
}