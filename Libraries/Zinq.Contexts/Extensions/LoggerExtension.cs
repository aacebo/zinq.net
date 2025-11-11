using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Zinq.Contexts.Extensions;

public static partial class Extensions
{
    extension(Keys keys)
    {
        public static Key<ILogger> Logger => new("logger");
    }

    extension(IReadOnlyContext context)
    {
        public ILogger Logger
        {
            get => context.Get(Keys.Logger);
        }
    }

    extension(IContext context)
    {
        public ILogger Logger
        {
            get => context.Get(Keys.Logger);
            internal set => context.Set(Keys.Logger, value);
        }
    }

    extension(IServiceCollection services)
    {
        public IServiceCollection AddLoggerContextExtension()
        {
            return services.AddSingleton<IContextExtension, LoggerExtension>();
        }
    }
}

public class LoggerExtension(IServiceProvider provider) : IContextExtension
{
    public void Apply(IContext context)
    {
        context.Logger = provider.GetRequiredService<ILoggerFactory>().CreateLogger<IContext>();
    }
}