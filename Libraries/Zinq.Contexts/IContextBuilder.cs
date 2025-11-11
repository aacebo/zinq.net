using Zinq.Contexts.Resolvers;

namespace Zinq.Contexts;

public interface IContextBuilder : IContextBuilder<IContext>;
public interface IContextBuilder<out TContext> where TContext : IContext
{
    IContextBuilder WithParent(IReadOnlyContext parent);
    IContextBuilder With(string key, IResolver resolver);
    TContext Build();
}

public static class ContextBuilderExtensions
{
    public static TContextBuilder With<TContextBuilder, T>(this TContextBuilder builder, Key<T> key, T value)
        where TContextBuilder : IContextBuilder
        where T : notnull
    {
        return (TContextBuilder)builder.With(key.Name, new ValueResolver(value));
    }

    public static TContextBuilder With<TContextBuilder, T>(this TContextBuilder builder, Key<T> key, Func<IReadOnlyContext, T> resolve)
        where TContextBuilder : IContextBuilder
        where T : notnull
    {
        return (TContextBuilder)builder.With(key.Name, new FactoryResolver<T>(resolve));
    }

    public static TContextBuilder With<TContextBuilder, T>(this TContextBuilder builder, Key<T> key, Func<IReadOnlyContext, Task<T>> resolve)
        where TContextBuilder : IContextBuilder
        where T : notnull
    {
        return (TContextBuilder)builder.With(key.Name, new FactoryResolver<T>(resolve));
    }
}