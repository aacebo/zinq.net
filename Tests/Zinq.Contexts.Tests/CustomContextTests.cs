using Microsoft.Extensions.DependencyInjection;

using Zinq.Contexts.Annotations;
using Zinq.Contexts.Resolvers;

namespace Zinq.Contexts.Tests;

[Context]
public class UserContext : Context, IContext
{
    public Guid UserId
    {
        get => Get(new Key<Guid>("user_id"));
        set => Set("user_id", new ValueResolver(value));
    }

    public UserContext(IServiceProvider provider) : base(provider)
    {
    }
}

public class CustomContextTests
{
    [Fact]
    public void Should_SetUserId()
    {
        var provider = new ServiceCollection().AddContext<UserContext>().BuildServiceProvider();
        var context = provider.GetRequiredService<UserContext>();
        var id = Guid.NewGuid();

        Assert.False(context.Has("user_id"));

        context.UserId = id;

        Assert.True(context.Has("user_id"));
        Assert.Equal(context.UserId, id);
    }
}