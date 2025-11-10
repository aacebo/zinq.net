namespace Zinq.Contexts;

public readonly record struct Key<T>(string Name) : IEquatable<Key<T>>
{
    public override string ToString() => Name;
}