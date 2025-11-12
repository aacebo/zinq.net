namespace Zinq.Contexts.Tests;

public class ContextTests
{
    [Fact]
    public void Should_Copy()
    {
        var key = new Key<Guid>("user_id");
        var context = new Context();

        Assert.False(context.Has(key));

        var id = Guid.NewGuid();
        var newContext = context.With(key, id);

        Assert.True(context.Has(key));
        Assert.True(newContext.Has(key));
        Assert.Equal(newContext.Get(key), id);
    }

    [Fact]
    public void Should_Resolve_Factory()
    {
        var key = new Key<Guid>("user_id");
        var context = new Context();

        Assert.False(context.Has(key));

        var id = Guid.NewGuid();
        var newContext = context.With(key, _ => id);

        Assert.True(context.Has(key));
        Assert.True(newContext.Has(key));
        Assert.Equal(newContext.Get(key), id);
    }
}