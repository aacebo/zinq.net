namespace Zinq.Contexts.Extensions.DependencyInjection;

public class ProviderExtension<TContext>(IServiceProvider provider) : IContextExtension<TContext, IProviderContext<TContext>>
    where TContext : IContext
{
    public IProviderContext<TContext> Extend(TContext context)
    {
        return new ProviderContext<TContext>(context.With(Key.Provider, provider));
    }

    public IContextBuilder<IProviderContext<TContext>> Extend(IContextBuilder<TContext> builder)
    {
        return new ProviderContextBuilder<TContext>(builder.With(Key.Provider, provider));
    }
}