namespace Zinq.Contexts.Extensions.DependencyInjection;

public class ProviderContext<TInnerContext> : Context, IProviderContext where TInnerContext : IContext
{
    public IServiceProvider Provider { get; }

    protected TInnerContext Inner { get; }

    public ProviderContext(IServiceProvider provider, TInnerContext context) : base()
    {
        Provider = provider;
        Inner = context;
    }
}