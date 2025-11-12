using Microsoft.Extensions.Logging;

namespace Zinq.Contexts.Extensions.Logging;

public class LoggerContext<TContext>(TContext context) : ExtendedContext<TContext>(context), ILoggerContext<TContext> where TContext : IContext
{
    public ILogger Logger => Get(Keys.Logger);
}