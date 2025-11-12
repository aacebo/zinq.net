namespace Zinq.Contexts;

public interface IReadOnlyContext : IDisposable, ICloneable
{
    bool Has(string key);
    object? Get(string key);
    bool TryGet(string key, out object value);
}