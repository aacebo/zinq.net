using Microsoft.Extensions.Logging;

namespace Zinq.Contexts.Extensions.Logging;

public static partial class Extensions
{
    extension(Keys)
    {
        public static Key<ILogger> Logger => new("microsoft.extensions.logger");
    }

    extension<TContext>(IReadOnly<ILoggerContext<TContext>> context) where TContext : IContext
    {
        public ILogger Logger => context.Get(Keys.Logger);
    }

    public static IContextBuilder<ILoggerContext<TContext>> WithLogger<TContext>(this IContextBuilder<TContext> builder, IServiceProvider provider)
        where TContext : IContext
    {
        var extension = new LoggerExtension<TContext>(provider);
        return extension.Extend(builder);
    }
}