using Microsoft.AspNetCore.Http;

using Zinq.Contexts.Extensions.DependencyInjection;
using Zinq.Contexts.Extensions.Logging;

namespace Zinq.Contexts.AspNetCore;

public interface IAspNetCoreContext<TContext> : ILoggerContext<TContext>, IProviderContext<TContext> where TContext : IContext
{
    public HttpContext? Http { get; }
}