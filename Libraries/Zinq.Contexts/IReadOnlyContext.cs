namespace Zinq.Contexts;

public interface IReadOnlyContext : IDisposable, ICloneable
{
    IReadOnlyContext? Parent { get; }
    IServiceProvider Provider { get; }

    bool Has(string key);
    object? Get(string key);
    bool TryGet(string key, out object value);

    IReadOnlyContext With(string key, IResolver resolver);
}