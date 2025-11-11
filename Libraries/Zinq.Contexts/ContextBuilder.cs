using Microsoft.Extensions.DependencyInjection;

namespace Zinq.Contexts;

public class ContextBuilder : IContextBuilder
{
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
    }

    public IContextBuilder WithParent(IReadOnlyContext parent)
    {
        _parent = parent;
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