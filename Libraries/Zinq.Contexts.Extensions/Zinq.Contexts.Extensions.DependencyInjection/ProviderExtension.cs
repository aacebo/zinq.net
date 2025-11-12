namespace Zinq.Contexts.Extensions.DependencyInjection;

public class ProviderExtension<TContext>(IServiceProvider provider) : IContextExtension<TContext, IProviderContext<TContext>>
    where TContext : IContext
{
    public IContextBuilder<IProviderContext<TContext>> Extend(IContextBuilder<TContext> builder)
    {
        return new ProviderContextBuilder<TContext>(builder.With(Keys.Provider, provider));
    }
}