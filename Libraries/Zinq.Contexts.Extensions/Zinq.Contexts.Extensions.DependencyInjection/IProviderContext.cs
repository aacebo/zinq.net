namespace Zinq.Contexts.Extensions.DependencyInjection;

public interface IProviderContext : IContext
{
    IServiceProvider Provider { get; }
}