using Zinq.Contexts.Resolvers;

namespace Zinq.Contexts;

public interface IContextBuilder<out TContext> where TContext : IContext
{
    IContextBuilder<TContext> WithParent(IReadOnlyContext parent);
    IContextBuilder<TContext> With(string key, IResolver resolver);
    TContext Build();
}

public static class ContextBuilderExtensions
{
    public static IContextBuilder<TContext> With<TContext, T>(this IContextBuilder<TContext> builder, Key<T> key, T value)
        where TContext : IContext
        where T : notnull
    {
        return builder.With(key.Name, new ValueResolver(value));
    }

    public static IContextBuilder<TContext> With<TContext, T>(this IContextBuilder<TContext> builder, Key<T> key, Func<IReadOnlyContext, T> resolve)
        where TContext : IContext
        where T : notnull
    {
        return builder.With(key.Name, new FactoryResolver<T>(resolve));
    }

    public static IContextBuilder<TContext> With<TContext, T>(this IContextBuilder<TContext> builder, Key<T> key, Func<IReadOnlyContext, Task<T>> resolve)
        where TContext : IContext
        where T : notnull
    {
        return builder.With(key.Name, new FactoryResolver<T>(resolve));
    }

    public static IContextBuilder<IContext<TContext>> WithExtension<TContext>(this TContext context) where TContext : IContext
    {
        return
    }
}