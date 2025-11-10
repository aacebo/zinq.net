namespace Zinq.Contexts;

public partial class Context : IContext
{
    public string TraceId { get; internal set; }
    public IReadOnlyContext? Parent { get; internal set; }
    public IServiceProvider Provider { get; internal set; }

    protected IDictionary<string, IResolver> Values { get; private set; } = new Dictionary<string, IResolver>();

    public Context(IServiceProvider provider)
    {
        TraceId = Guid.NewGuid().ToString();
        Provider = provider;
    }

    public bool Has(string key)
    {
        return Values.ContainsKey(key) || (Parent is not null && Parent.Has(key));
    }

    public object? Get(string key)
    {
        return TryGet(key, out var value) ? value : throw new Exception($"'{key}' not found");
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

    public IContext Set(string key, IResolver resolver)
    {
        Values.Add(key, resolver);
        return this;
    }

    public IReadOnlyContext Fork(string key, IResolver resolver)
    {
        return New(this).Set(key, resolver).Build();
    }

    public IReadOnlyContext ToReadOnly()
    {
        return this;
    }
}