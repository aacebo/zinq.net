namespace Zinq.Contexts.Resolvers;

public class AsyncResolver(IAsyncResolver resolver) : IResolver, IAsyncResolver
{
    public object Resolve(IReadOnlyContext context)
    {
        return resolver.ResolveAsync(context);
    }

    public async Task<object> ResolveAsync(IReadOnlyContext context, CancellationToken cancellationToken = default)
    {
        return await resolver.ResolveAsync(context, cancellationToken);
    }
}