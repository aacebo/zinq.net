using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Zinq.Contexts.Extensions.Logging;

public class LoggerExtension<TContext>(IServiceProvider provider) : IContextExtension<TContext, ILoggerContext<TContext>>
    where TContext : IContext
{
    public IContextBuilder<ILoggerContext<TContext>> Extend(IContextBuilder<TContext> builder)
    {
        var logger = provider.GetRequiredService<ILoggerFactory>().CreateLogger<TContext>();
        return new LoggerContextBuilder<TContext>(builder.With(Keys.Logger, logger));
    }
}