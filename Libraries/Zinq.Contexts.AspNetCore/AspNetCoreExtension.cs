using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Zinq.Contexts.Extensions.DependencyInjection;
using Zinq.Contexts.Extensions.Logging;

namespace Zinq.Contexts.AspNetCore;

public class AspNetCoreExtension<TContext>(IServiceProvider provider) : IContextExtension<TContext, IAspNetCoreContext<TContext>>
    where TContext : IContext
{
    public IAspNetCoreContext<TContext> Extend(TContext context)
    {
        var accessor = provider.GetRequiredService<IHttpContextAccessor>();
        var logger = provider.GetRequiredService<ILoggerFactory>().CreateLogger<TContext>();

        if (accessor.HttpContext is not null)
        {
            context.Set(Keys.AspNetCore.Http, accessor.HttpContext);
        }

        return new AspNetCoreContext<TContext>(
            context
                .With(Keys.Provider, provider)
                .With(Keys.Logger, logger)
        );
    }

    public IContextBuilder<IAspNetCoreContext<TContext>> Extend(IContextBuilder<TContext> builder)
    {
        var accessor = provider.GetRequiredService<IHttpContextAccessor>();
        var logger = provider.GetRequiredService<ILoggerFactory>().CreateLogger<TContext>();

        if (accessor.HttpContext is not null)
        {
            builder = builder.With(Keys.AspNetCore.Http, accessor.HttpContext);
        }

        return new AspNetCoreContextBuilder<TContext>(
            builder
                .With(Keys.Provider, provider)
                .With(Keys.Logger, logger)
        );
    }
}