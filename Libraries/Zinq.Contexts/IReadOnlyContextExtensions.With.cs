using Zinq.Contexts.Resolvers;

namespace Zinq.Contexts;

public static partial class IReadOnlyContextExtensions
{
    public static IReadOnlyContext With(this IReadOnlyContext context, string key, object value)
    {
        return context.With(key, new ValueResolver(value));
    }

    public static IReadOnlyContext With<T>(this IReadOnlyContext context, Key<T> key, T value) where T : notnull
    {
        return context.With(key.Name, new ValueResolver<T>(value));
    }

    public static IReadOnlyContext With<T>(this IReadOnlyContext context, Key<T> key, Func<IReadOnlyContext, T> resolve) where T : notnull
    {
        return context.With(key.Name, new FactoryResolver<T>(resolve));
    }

    public static IReadOnlyContext With<T>(this IReadOnlyContext context, Key<T> key, Func<IReadOnlyContext, Task<T>> resolve) where T : notnull
    {
        return context.With(key.Name, new FactoryResolver<T>(resolve));
    }
}