namespace Zinq.Contexts;

public interface IContext<TBaseContext> : IContext where TBaseContext : IReadOnlyContext
{
}

public interface IReadOnly<TBaseContext> : IReadOnlyContext where TBaseContext : IContext
{
}

public class Context<TBaseContext>(TBaseContext context) : Context(context), IContext<TBaseContext> where TBaseContext : IReadOnlyContext
{
    public new TBaseContext Parent { get; } = context;
}

public class ReadOnly<TContext>(TContext context) : IReadOnly<TContext> where TContext : IContext
{
    public TContext Parent => context;

    public bool Has(string key) => context.Has(key);
    public object? Get(string key) => context.Get(key);
    public bool TryGet(string key, out object value) => context.TryGet(key, out value);
    public void Dispose() => context.Dispose();
    public object Clone() => context.Clone();
}

public static partial class ContextExtensions
{
    public static IReadOnly<TContext> ToReadOnly<TContext>(this TContext context) where TContext : IContext
    {
        return new ReadOnly<TContext>(context);
    }
}