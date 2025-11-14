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
            .BuildServiceProvider();

        var context = new ContextBuilder()
            .WithProvider(provider)
            // .WithLogger(provider)
            .Build()
            .withLog

        Assert.True(context.Has(Key.Logger));
        Assert.IsAssignableFrom<ILogger>(context.Logger);
    }
}