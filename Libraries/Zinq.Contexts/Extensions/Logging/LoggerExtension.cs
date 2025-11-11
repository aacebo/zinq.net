using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Zinq.Contexts.Extensions.Logging;

public class LoggerExtension(IServiceProvider provider) : IContextExtension
{
    public void Apply(IContext context)
    {
        var logger = provider.GetRequiredService<ILoggerFactory>().CreateLogger<IContext>();
        context.Set(Keys.Logger, logger);
    }
}