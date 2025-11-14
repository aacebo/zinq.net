using Microsoft.Extensions.Logging;

using Zinq.Contexts.Annotations;

namespace Zinq.Contexts.Extensions.Logging;

[Context("Logging", typeof(Logger))]
public interface ILoggerContext<TContext> : IContext<TContext> where TContext : IContext
{
    public ILogger Logger { get; }
}