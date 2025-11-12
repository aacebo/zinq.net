namespace Zinq.Contexts.Extensions.DependencyInjection;

public class ProviderContextBuilder<TContext>(IContextBuilder<TContext> builder) : IContextBuilder<IProviderContext<TContext>> where TContext : IContext
{
    public IContextBuilder<IProviderContext<TContext>> With(string key, IResolver resolver)
    {
        builder = builder.With(key, resolver);
        return this;
    }

    public IProviderContext<TContext> Build()
    {
        var context = builder.Build();
        return new ProviderContext<TContext>(context);
    }
}