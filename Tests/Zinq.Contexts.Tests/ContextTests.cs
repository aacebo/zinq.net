using Microsoft.Extensions.DependencyInjection;

using Zinq.Contexts.Extensions;

namespace Zinq.Contexts.Tests;

public class ContextTests
{
    [Fact]
    public void Should_Fork()
    {
        var key = new Key<Guid>("user_id");
        var provider = new ServiceCollection().AddContext().BuildServiceProvider();
        var context = provider.GetRequiredService<IReadOnlyContext>();

        Assert.False(context.Has(key));

        var id = Guid.NewGuid();
        var newContext = context.Fork(key, id);

        Assert.False(context.Has(key));
        Assert.True(newContext.Has(key));
        Assert.Equal(newContext.Get(key), id);
    }

    [Fact]
    public void Should_Resolve_Factory()
    {
        var key = new Key<Guid>("user_id");
        var provider = new ServiceCollection().AddContext().BuildServiceProvider();
        var context = provider.GetRequiredService<IReadOnlyContext>();

        Assert.False(context.Has(key));

        var id = Guid.NewGuid();
        var newContext = context.Fork(key, _ => id);

        Assert.False(context.Has(key));
        Assert.True(newContext.Has(key));
        Assert.Equal(newContext.Get(key), id);
    }
}