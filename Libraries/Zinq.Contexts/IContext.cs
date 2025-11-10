using Zinq.Contexts.Resolvers;

namespace Zinq.Contexts;

public interface IReadOnlyContext : IDisposable, ICloneable
{
    string TraceId { get; }
    IReadOnlyContext? Parent { get; }
    IServiceProvider Provider { get; }

    bool Has(string key);

    object? Get(string key);
    bool TryGet(string key, out object value);

    IReadOnlyContext Fork(string key, IResolver resolver);
}

public interface IContext : IReadOnlyContext
{
    IContext Set(string key, IResolver resolver);

    IReadOnlyContext ToReadOnly();
}

public static class IReadOnlyContextExtensions
{
    public static bool Has<T>(this IReadOnlyContext context, Key<T> key) where T : notnull
    {
        return context.Has(key.Name);
    }

    public static T Get<T>(this IReadOnlyContext context, Key<T> key) where T : notnull
    {
        var value = context.Get(key.Name) ?? throw new Exception($"key '{key}' not found");

        if (value is not T casted)
        {
            throw new Exception($"'{key}' => expected type '{typeof(T)}', found '{value.GetType()}'");
        }

        return casted;
    }

    public static bool TryGet<T>(this IReadOnlyContext context, Key<T> key, out T value) where T : notnull
    {
        if (context.TryGet(key.Name, out var o) && o is T output)
        {
            value = output;
            return true;
        }

        value = default!;
        return false;
    }

    public static IReadOnlyContext Fork(this IReadOnlyContext context, string key, object value)
    {
        return context.Fork(key, new ValueResolver(value));
    }

    public static IReadOnlyContext Fork<T>(this IReadOnlyContext context, Key<T> key, T value) where T : notnull
    {
        return context.Fork(key.Name, new ValueResolver<T>(value));
    }

    public static IReadOnlyContext Fork<T>(this IReadOnlyContext context, Key<T> key, Func<IReadOnlyContext, T> resolve) where T : notnull
    {
        return context.Fork(key.Name, new FactoryResolver<T>(resolve));
    }

    public static IReadOnlyContext Fork<T>(this IReadOnlyContext context, Key<T> key, Func<IReadOnlyContext, Task<T>> resolve) where T : notnull
    {
        return context.Fork(key.Name, new FactoryResolver<T>(resolve));
    }
}

public static class IContextExtensions
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