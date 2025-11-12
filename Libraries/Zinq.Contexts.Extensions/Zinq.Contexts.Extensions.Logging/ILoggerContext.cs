using Microsoft.Extensions.Logging;

namespace Zinq.Contexts.Extensions.Logging;

public interface ILoggerContext<TContext> : IContext<TContext> where TContext : IContext
{
    public ILogger Logger { get; }
}