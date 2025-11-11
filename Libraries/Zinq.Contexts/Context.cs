using Microsoft.Extensions.DependencyInjection;

namespace Zinq.Contexts;

public partial class Context : IContext
{
    public string TraceId { get; internal set; }
    public IReadOnlyContext? Parent { get; internal set; }
    public IServiceProvider Provider { get; internal set; }

    internal IDictionary<string, IResolver> Values { get; init; } = new Dictionary<string, IResolver>();

    public Context(IServiceProvider provider)
    {
        TraceId = Guid.NewGuid().ToString();
        Provider = provider;

        foreach (var extension in provider.GetServices<IContextExtension>())
        {
            Extend(extension);
        }
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

    public IContext Extend(IContextExtension extension)
    {
        extension.Apply(this);
        return this;
    }

    public IReadOnlyContext With(string key, IResolver resolver)
    {
        return New(this).With(key, resolver).Build();
    }

    public IReadOnlyContext ToReadOnly()
    {
        return this;
    }

    public static IContextBuilder New() => new ContextBuilder();
    public static IContextBuilder New(IServiceProvider provider) => new ContextBuilder(provider);
    public static IContextBuilder New(IReadOnlyContext parent) => new ContextBuilder(parent);
}