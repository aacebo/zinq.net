using Microsoft.Extensions.Logging;

using Zinq.Contexts.Extensions.DependencyInjection;
using Zinq.Contexts.Extensions.Logging;

namespace Zinq.Contexts.AspNetCore;

public class AspNetCoreContext<TContext>(TContext context) : Context<TContext>(context), IAspNetCoreContext<TContext> where TContext : IContext
{
    public Microsoft.AspNetCore.Http.HttpContext? Http => Get(Key.AspNetCore.Http);
    public ILogger Logger => Get(Key.Logger);
    public IServiceProvider Provider => Get(Key.Provider);
}