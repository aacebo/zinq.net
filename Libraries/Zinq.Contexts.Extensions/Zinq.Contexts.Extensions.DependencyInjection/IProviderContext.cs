namespace Zinq.Contexts.Extensions.DependencyInjection;

public interface IProviderContext<TContext> : IContext<TContext> where TContext : IReadOnlyContext
{
    IServiceProvider Provider { get; }
}