namespace Zinq.Contexts.Resolvers;

public class SingletonResolver : IResolver, IAsyncResolver
{
    protected IResolver Resolver { get; }
    protected object? Value { get; private set; }

    public SingletonResolver(IResolver resolver)
    {
        Resolver = resolver;
    }

    public SingletonResolver(IAsyncResolver resolver)
    {
        Resolver = new AsyncResolver(resolver);
    }

    public object Resolve(IReadOnlyContext context)
    {
        Value ??= Resolver.Resolve(context);
        return Value;
    }

    public Task<object> ResolveAsync(IReadOnlyContext context, CancellationToken cancellationToken = default)
    {
        Value ??= Resolver.Resolve(context);
        return Task.FromResult(Value);
    }
}

public static partial class ResolverExtensions
{
    public static IResolver ToSingleton(this IResolver resolver) => new SingletonResolver(resolver);
    public static IAsyncResolver ToSingleton(this IAsyncResolver resolver) => new SingletonResolver(resolver);
}