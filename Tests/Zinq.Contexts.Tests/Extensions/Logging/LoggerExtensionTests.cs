using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Zinq.Contexts.Extensions.Logging;

namespace Zinq.Contexts.Tests;

public class LoggerExtensionTests
{
    [Fact]
    public void Should_HaveLogger()
    {
        var provider = new ServiceCollection()
            .AddLogging(builder => builder.AddConsole())
            .AddContext(builder => builder.WithLogger())
            .BuildServiceProvider();

        var context = provider.GetRequiredService<IReadOnlyContext>();

        Assert.True(context.Has(Keys.Logger));
        Assert.IsAssignableFrom<ILogger>(context.Logger);
    }
}