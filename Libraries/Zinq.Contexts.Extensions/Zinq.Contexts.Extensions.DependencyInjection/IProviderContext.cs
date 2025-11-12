namespace Zinq.Contexts.Extensions.DependencyInjection;

public interface IProviderContext<TContext> : IExtendedContext<TContext> where TContext : IReadOnlyContext
{
    IServiceProvider Provider { get; }
}