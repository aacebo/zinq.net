using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Zinq.Contexts.Extensions.DependencyInjection;
using Zinq.Contexts.Extensions.Logging;

namespace Zinq.Contexts.AspNetCore;

public class AspNetCoreKeys
{
    public readonly Key<HttpContext> Http = new("aspnetcore.http");
}

public static partial class Extensions
{
    extension(Keys)
    {
        public static AspNetCoreKeys AspNetCore => new();
    }

    extension<TContext>(IReadOnly<IAspNetCoreContext<TContext>> context) where TContext : IContext
    {
        public ILogger Logger => context.Get(Keys.Logger);
        public IServiceProvider Provider => context.Get(Keys.Provider);
        public HttpContext? Http
        {
            get
            {
                if (context.TryGet(Keys.AspNetCore.Http, out var output))
                {
                    return output;
                }

                return null;
            }
        }
    }

    public static IAspNetCoreContext<TContext> WithAspNetCore<TContext>(this TContext context, IServiceProvider provider)
        where TContext : IContext
    {
        var extension = new AspNetCoreExtension<TContext>(provider);
        return extension.Extend(context);
    }

    public static IContextBuilder<IAspNetCoreContext<TContext>> WithAspNetCore<TContext>(this IContextBuilder<TContext> builder, IServiceProvider provider)
        where TContext : IContext
    {
        var extension = new AspNetCoreExtension<TContext>(provider);
        return extension.Extend(builder);
    }
}