using Microsoft.AspNetCore.Http;

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

    public static IContextBuilder<IAspNetCoreContext<TContext>> WithAspNetCore<TContext>(this IContextBuilder<TContext> builder, IServiceProvider provider)
        where TContext : IContext
    {
        var extension = new AspNetCoreExtension<TContext>(provider);
        return extension.Extend(builder);
    }
}