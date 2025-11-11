using Microsoft.Extensions.DependencyInjection;

using Zinq.Contexts.Extensions.Tracing;

namespace Zinq.Contexts.Tests;

public class TraceExtensionTests
{
    [Fact]
    public void Should_HaveTraceId()
    {
        var provider = new ServiceCollection()
            .AddContext(builder => builder.WithTracing())
            .BuildServiceProvider();

        var context = provider.GetRequiredService<IReadOnlyContext>();

        Assert.True(context.Has(Keys.TraceId));
        Assert.IsAssignableFrom<string>(context.TraceId);
    }
}