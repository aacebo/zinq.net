using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Zinq.Contexts.Extensions.Logging;

public class LoggerExtension : IContextExtension
{
    public void Apply(IContext context)
    {
        var logger = context.Provider.GetRequiredService<ILoggerFactory>().CreateLogger<IContext>();
        context.Set(Keys.Logger, logger);
    }
}