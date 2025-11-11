using Zinq.Contexts.Resolvers;

namespace Zinq.Contexts;

public static partial class ReadOnlyContextExtensions
{
    public static TContext With<TContext>(this TContext context, string key, object value) where TContext : IReadOnlyContext
    {
        return (TContext)context.With(key, new ValueResolver(value));
    }

    public static TContext With<TContext, T>(this TContext context, Key<T> key, T value)
        where TContext : IReadOnlyContext
        where T : notnull
    {
        return (TContext)context.With(key.Name, new ValueResolver<T>(value));
    }

    public static TContext With<TContext, T>(this TContext context, Key<T> key, Func<TContext, T> resolve)
        where TContext : IReadOnlyContext
        where T : notnull
    {
        return (TContext)context.With(key.Name, new FactoryResolver<T>(ctx => resolve((TContext)ctx)));
    }

    public static TContext With<TContext, T>(this TContext context, Key<T> key, Func<TContext, Task<T>> resolve)
        where TContext : IReadOnlyContext
        where T : notnull
    {
        return (TContext)context.With(key.Name, new FactoryResolver<T>(ctx => resolve((TContext)ctx)));
    }
}