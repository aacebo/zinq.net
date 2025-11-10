using Microsoft.AspNetCore.Http;

namespace Zinq.Contexts.AspNetCore;

public static partial class Extensions
{
    extension(Keys keys)
    {
        public static Key<HttpContext> Http => new("http");
    }
}

public class HttpContextExtension(IHttpContextAccessor httpContextAccessor) : IContextExtension
{
    public void Apply(IContext context)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is not null)
        {
            context.Set(Keys.Http, httpContext);
        }
    }
}