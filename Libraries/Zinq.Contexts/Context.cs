namespace Zinq.Contexts;

public partial class Context : IContext
{
    public IReadOnlyContext? Parent { get; internal set; }

    internal IDictionary<string, IResolver> Values { get; init; } = new Dictionary<string, IResolver>();

    public Context()
    {

    }

    public bool Has(string key)
    {
        return Values.ContainsKey(key) || (Parent is not null && Parent.Has(key));
    }

    public bool Has<T>(Key<T> key) where T : notnull
    {
        return Has(key.Name);
    }

    public object? Get(string key)
    {
        return TryGet(key, out var value) ? value : throw new Exception($"'{key}' not found");
    }

    public T Get<T>(Key<T> key) where T : notnull
    {
        var value = Get(key.Name) ?? throw new Exception($"key '{key}' not found");

        if (value is not T casted)
        {
            throw new Exception($"'{key}' => expected type '{typeof(T)}', found '{value.GetType()}'");
        }

        return casted;
    }

    public bool TryGet(string key, out object value)
    {
        if (Values.TryGetValue(key, out var resolver))
        {
            value = resolver.Resolve(this);
            return true;
        }

        if (Parent is not null && Parent.TryGet(key, out value))
        {
            return true;
        }

        value = default!;
        return false;
    }

    public bool TryGet<T>(Key<T> key, out T value) where T : notnull
    {
        if (TryGet(key.Name, out var o) && o is T output)
        {
            value = output;
            return true;
        }

        value = default!;
        return false;
    }

    public IContext Set(string key, IResolver resolver)
    {
        Values.Add(key, resolver);
        return this;
    }

    public IContext Scope() => New().WithParent(this).Build();
    public IReadOnlyContext With(string key, IResolver resolver) => Parent is null ? New().With(key, resolver).Build() : New().WithParent(Parent).With(key, resolver).Build();
    public IReadOnlyContext ToReadOnly() => this;

    public static IContextBuilder New() => new ContextBuilder();
}