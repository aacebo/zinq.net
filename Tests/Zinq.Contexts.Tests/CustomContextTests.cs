using Zinq.Contexts.Resolvers;

namespace Zinq.Contexts.Tests;

public class UserContext : Context, IContext
{
    public Guid UserId
    {
        get => Get(new Key<Guid>("user_id"));
        set => Set("user_id", new ValueResolver(value));
    }
}

public class CustomContextTests
{
    [Fact]
    public void Should_SetUserId()
    {
        var context = new UserContext();
        var id = Guid.NewGuid();

        Assert.False(context.Has("user_id"));

        context.UserId = id;

        Assert.True(context.Has("user_id"));
        Assert.Equal(context.UserId, id);
    }
}