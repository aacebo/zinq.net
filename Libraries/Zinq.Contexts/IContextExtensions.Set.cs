using Zinq.Contexts.Resolvers;

namespace Zinq.Contexts;

public static partial class IContextExtensions
{
    public static IContext Set(this IContext context, string key, object value)
    {
        return context.Set(key, new ValueResolver(value));
    }

    public static IContext Set<T>(this IContext context, Key<T> key, T value) where T : notnull
    {
        return context.Set(key.Name, new ValueResolver<T>(value));
    }

    public static IContext Set<T>(this IContext context, Key<T> key, Func<IReadOnlyContext, T> resolve) where T : notnull
    {
        return context.Set(key.Name, new FactoryResolver<T>(resolve));
    }

    public static IContext Set<T>(this IContext context, Key<T> key, Func<IReadOnlyContext, Task<T>> resolve) where T : notnull
    {
        return context.Set(key.Name, new FactoryResolver<T>(resolve));
    }
}