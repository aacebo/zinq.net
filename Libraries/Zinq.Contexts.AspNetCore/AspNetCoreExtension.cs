using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Zinq.Contexts.AspNetCore;

public class AspNetCoreExtension<TContext>(IServiceProvider provider) : IContextExtension<TContext, IAspNetCoreContext<TContext>>
    where TContext : IContext
{
    public IContextBuilder<IAspNetCoreContext<TContext>> Extend(IContextBuilder<TContext> builder)
    {
        var accessor = provider.GetRequiredService<IHttpContextAccessor>();

        if (accessor.HttpContext is not null)
        {
            builder = builder.With(Keys.AspNetCore.Http, accessor.HttpContext);
        }

        return new AspNetCoreContextBuilder<TContext>(builder);
    }
}