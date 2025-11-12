namespace Zinq.Contexts.Extensions.DependencyInjection;

public class ProviderContext<TContext>(TContext context) : Context<TContext>(context), IProviderContext<TContext> where TContext : IContext
{
    public IServiceProvider Provider => Get(Keys.Provider);
}