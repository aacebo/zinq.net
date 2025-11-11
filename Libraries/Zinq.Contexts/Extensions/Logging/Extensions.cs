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

    public static IServiceCollection AddLoggerContext(this IServiceCollection services)
    {
        return services.AddScoped<IContextExtension, LoggerExtension>();
    }

    public static TContextBuilder WithLogger<TContextBuilder>(this TContextBuilder builder) where TContextBuilder : IContextBuilder
    {
        return (TContextBuilder)builder.WithExtension<LoggerExtension>();
    }
}