namespace Zinq.Contexts.Resolvers;

public class FactoryResolver : FactoryResolver<object>
{
    public FactoryResolver(Func<IReadOnlyContext, object> handler) : base(handler)
    {
    }

    public FactoryResolver(Func<IReadOnlyContext, Task<object>> handler) : base(handler)
    {
    }
}

public class FactoryResolver<T> : IResolver, IAsyncResolver where T : notnull
{
    protected Func<IReadOnlyContext, T> Handler { get; }

    public FactoryResolver(Func<IReadOnlyContext, T> handler)
    {
        Handler = handler;
    }

    public FactoryResolver(Func<IReadOnlyContext, Task<T>> handler)
    {
        Handler = context => handler(context).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    public object Resolve(IReadOnlyContext context)
    {
        return Handler(context);
    }

    public Task<object> ResolveAsync(IReadOnlyContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<object>(Handler(context));
    }
}