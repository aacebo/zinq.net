using Zinq.Contexts.Resolvers;

namespace Zinq.Contexts;

public static partial class ContextExtensions
{
    public static TContext Set<TContext>(this TContext context, string key, object value) where TContext : IContext
    {
        return (TContext)context.Set(key, new ValueResolver(value));
    }

    public static TContext Set<TContext, T>(this TContext context, Key<T> key, T value)
        where TContext : IContext
        where T : notnull
    {
        return (TContext)context.Set(key.Name, new ValueResolver<T>(value));
    }

    public static TContext Set<TContext, T>(this TContext context, Key<T> key, Func<IReadOnlyContext, T> resolve)
        where TContext : IContext
        where T : notnull
    {
        return (TContext)context.Set(key.Name, new FactoryResolver<T>(resolve));
    }

    public static TContext Set<TContext, T>(this TContext context, Key<T> key, Func<IReadOnlyContext, Task<T>> resolve)
        where TContext : IContext
        where T : notnull
    {
        return (TContext)context.Set(key.Name, new FactoryResolver<T>(resolve));
    }
}