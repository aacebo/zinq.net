namespace Zinq.Contexts;

public class ContextBuilder : IContextBuilder
{
    private IReadOnlyContext? _parent;
    private readonly IList<IContextExtension> _extensions = [];
    private readonly Dictionary<string, IResolver> _values = [];

    public ContextBuilder()
    {

    }

    public IContextBuilder WithParent(IReadOnlyContext parent)
    {
        _parent = parent;
        return this;
    }

    public IContextBuilder WithExtension(IContextExtension extension)
    {
        _extensions.Add(extension);
        return this;
    }

    public IContextBuilder With(string key, IResolver resolver)
    {
        _values.Add(key, resolver);
        return this;
    }

    public IContext Build()
    {
        var context = new Context()
        {
            Values = _values
        };

        if (_parent is not null)
        {
            context.Parent = _parent;
        }

        foreach (var extension in _extensions)
        {
            context.Extend(extension);
        }

        return context;
    }
}