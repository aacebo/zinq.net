using Zinq.Contexts.Resolvers;

namespace Zinq.Contexts;

public static partial class ContextExtensions
{
    public static TContext With<TContext>(this TContext context, string key, IResolver resolver)
        where TContext : IContext
    {
        context.Set(key, resolver);
        return context;
    }

    public static TContext With<TContext, T>(this TContext context, Key<T> key, T value)
        where TContext : IContext
        where T : notnull
    {
        context.Set(key.Name, new ValueResolver<T>(value));
        return context;
    }

    public static TContext With<TContext, T>(this TContext context, Key<T> key, Func<TContext, T> resolve)
        where TContext : IContext
        where T : notnull
    {
        context.Set(key.Name, new FactoryResolver<T>(ctx => resolve((TContext)ctx)));
        return context;
    }

    public static TContext With<TContext, T>(this TContext context, Key<T> key, Func<TContext, Task<T>> resolve)
        where TContext : IContext
        where T : notnull
    {
        context.Set(key.Name, new FactoryResolver<T>(ctx => resolve((TContext)ctx)));
        return context;
    }
}