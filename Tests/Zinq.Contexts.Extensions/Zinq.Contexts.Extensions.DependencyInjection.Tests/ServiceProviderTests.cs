using Microsoft.Extensions.DependencyInjection;

using Zinq.Contexts.Extensions.DependencyInjection;

namespace Zinq.Contexts.Extensions.DependencyInjection.Tests;

public class ServiceProviderTests
{
    [Fact]
    public void Should_HaveProvider()
    {
        var provider = new ServiceCollection().BuildServiceProvider();
        var context = new ContextBuilder().WithProvider(provider).Build();

        Assert.True(context.Has(Keys.Provider));
        Assert.IsAssignableFrom<IServiceProvider>(context.Provider);
    }
}