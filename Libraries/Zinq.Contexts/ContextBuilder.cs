using Microsoft.Extensions.DependencyInjection;

namespace Zinq.Contexts;

public class ContextBuilder : IContextBuilder
{
    private string? _traceId;
    private IReadOnlyContext? _parent;
    private readonly IServiceProvider _provider;
    private readonly IServiceCollection _extensions = new ServiceCollection();
    private readonly Dictionary<string, IResolver> _values = [];

    public ContextBuilder()
    {
        _provider = new ServiceCollection().BuildServiceProvider();
    }

    public ContextBuilder(IServiceProvider provider)
    {
        _provider = provider;
    }

    public ContextBuilder(IReadOnlyContext parent) : this(parent.Provider)
    {
        _parent = parent;
        _traceId = parent.TraceId;
    }

    public IContextBuilder WithTraceId(string traceId)
    {
        _traceId = traceId;
        return this;
    }

    public IContextBuilder WithParent(IReadOnlyContext parent)
    {
        _parent = parent;
        _traceId = parent.TraceId;
        return this;
    }

    public IContextBuilder WithExtension<TContextExtension>() where TContextExtension : class, IContextExtension
    {
        _extensions.AddSingleton<IContextExtension, TContextExtension>();
        return this;
    }

    public IContextBuilder With(string key, IResolver resolver)
    {
        _values.Add(key, resolver);
        return this;
    }

    public IContext Build()
    {
        var context = new Context(_provider)
        {
            Values = _values
        };

        if (_traceId is not null)
        {
            context.TraceId = _traceId;
        }

        if (_parent is not null)
        {
            context.Parent = _parent;
        }

        foreach (var extension in _extensions.BuildServiceProvider().GetServices<IContextExtension>())
        {
            context.Extend(extension);
        }

        return context;
    }
}