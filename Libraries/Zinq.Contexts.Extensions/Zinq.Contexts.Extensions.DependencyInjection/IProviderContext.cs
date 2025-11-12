namespace Zinq.Contexts.Extensions.DependencyInjection;

public interface IProviderContext<TContext> : IContext<TContext> where TContext : IContext
{
    IServiceProvider Provider { get; }
}