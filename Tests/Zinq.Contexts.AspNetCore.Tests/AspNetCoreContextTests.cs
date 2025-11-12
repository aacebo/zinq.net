using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Zinq.Contexts.Extensions.DependencyInjection;
using Zinq.Contexts.Extensions.Logging;

namespace Zinq.Contexts.AspNetCore.Tests;

public class AspNetCoreContextTests
{
    [Fact]
    public void Should_HaveHttpContext()
    {
        var provider = new ServiceCollection()
            .AddHttpContextAccessor()
            .AddLogging(builder => builder.AddConsole())
            .BuildServiceProvider();

        var context = new ContextBuilder()
            .WithAspNetCore(provider)
            .Build()
            .ToReadOnly();

        Assert.Null(context.Http);
        Assert.NotNull(context.Logger);
        Assert.NotNull(context.Provider);
    }
}