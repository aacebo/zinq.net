using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Zinq.Contexts.Extensions.Tracing;

namespace Zinq.Contexts.AspNetCore;

public static partial class Extensions
{
    extension(Keys keys)
    {
        public static Key<HttpContext> Http => new("http");
    }

    extension(IReadOnlyContext context)
    {
        public HttpContext Http => context.Get(Keys.Http);
    }

    public static TContextBuilder WithHttp<TContextBuilder>(this TContextBuilder builder) where TContextBuilder : IContextBuilder
    {
        return (TContextBuilder)builder.WithExtension<HttpExtension>();
    }
}

public class HttpExtension : IContextExtension
{
    public void Apply(IContext context)
    {
        var httpContext = context.Provider.GetRequiredService<IHttpContextAccessor>().HttpContext;

        if (httpContext is not null)
        {
            context.Set(Keys.Http, httpContext);
            context.Set(Keys.TraceId, httpContext.TraceIdentifier);
        }
    }
}