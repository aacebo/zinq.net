using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Zinq.Contexts.Extensions;

namespace Zinq.Contexts.Tests;

public class ContextExtensionTests
{
    [Fact]
    public void Should_HaveLogger()
    {
        var provider = new ServiceCollection()
            .AddLogging(builder => builder.AddConsole())
            .AddContext()
            .AddLoggerContextExtension()
            .BuildServiceProvider();

        var context = provider.GetRequiredService<IReadOnlyContext>();

        Assert.True(context.Has(Keys.Logger));
    }
}