namespace Zinq.Contexts.Resolvers;

public class ValueResolver(object value) : ValueResolver<object>(value);
public class ValueResolver<T>(T value) : IResolver, IAsyncResolver where T : notnull
{
    public object Resolve(IReadOnlyContext context)
    {
        return value;
    }

    public Task<object> ResolveAsync(IReadOnlyContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<object>(value);
    }
}