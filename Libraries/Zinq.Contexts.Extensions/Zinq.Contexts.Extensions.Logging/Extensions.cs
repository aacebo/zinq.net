using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Zinq.Contexts.Extensions.Logging;

public static partial class Extensions
{
    extension(Keys)
    {
        public static Key<ILogger> Logger => new("logger");
    }

    extension(IReadOnlyContext context)
    {
        public ILogger Logger => context.Get(Keys.Logger);
    }

    public static TContextBuilder WithLogger<TContextBuilder>(this TContextBuilder builder, ILogger logger) where TContextBuilder : IContextBuilder
    {
        return builder.With(Keys.Logger, logger);
    }

    public static TContextBuilder WithLogger<TContextBuilder>(this TContextBuilder builder, IServiceProvider provider) where TContextBuilder : IContextBuilder
    {
        var logger = provider.GetRequiredService<ILoggerFactory>().CreateLogger<IContext>();
        return builder.With(Keys.Logger, logger);
    }
}