namespace Zinq.Contexts;

public interface IReadOnlyContext : IDisposable, ICloneable
{
    IReadOnlyContext? Parent { get; }

    bool Has(string key);
    object? Get(string key);
    bool TryGet(string key, out object value);

    IContext Scope();
    IReadOnlyContext With(string key, IResolver resolver);
}

public interface IReadOnlyContext<TParent> : IReadOnlyContext, ICloneable where TParent : IReadOnlyContext
{
    new TParent? Parent { get; }

    new IReadOnlyContext<TParent> With(string key, IResolver resolver);
}