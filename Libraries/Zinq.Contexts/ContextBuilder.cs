using Microsoft.Extensions.DependencyInjection;

namespace Zinq.Contexts;

public class ContextBuilder : IContextBuilder
{
    private string? _traceId;
    private IReadOnlyContext? _parent;
    private readonly IServiceCollection _services = new ServiceCollection();
    private readonly Dictionary<string, IResolver> _values = [];

    public ContextBuilder()
    {

    }

    public ContextBuilder(IServiceProvider provider)
    {
        foreach (var service in provider.GetServices<IContextExtension>())
        {
            _services.AddSingleton(service);
        }
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
        _services.AddScoped<IContextExtension, TContextExtension>();
        return this;
    }

    public IContextBuilder With(string key, IResolver resolver)
    {
        _values.Add(key, resolver);
        return this;
    }

    public IContext Build()
    {
        var provider = _services.BuildServiceProvider();
        var context = new Context(provider)
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

        return context;
    }
}