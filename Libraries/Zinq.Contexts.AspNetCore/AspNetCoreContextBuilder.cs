namespace Zinq.Contexts.AspNetCore;

public class AspNetCoreContextBuilder<TContext>(IContextBuilder<TContext> builder) : IContextBuilder<IAspNetCoreContext<TContext>> where TContext : IContext
{
    public IContextBuilder<IAspNetCoreContext<TContext>> With(string key, IResolver resolver)
    {
        builder = builder.With(key, resolver);
        return this;
    }

    public IAspNetCoreContext<TContext> Build()
    {
        var context = builder.Build();
        return new AspNetCoreContext<TContext>(context);
    }
}