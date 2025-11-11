namespace Zinq.Contexts;

public class ContextBuilder : IContextBuilder
{
    private IReadOnlyContext? _parent;
    private readonly Dictionary<string, IResolver> _values = [];

    public ContextBuilder()
    {

    }

    public IContextBuilder WithParent(IReadOnlyContext parent)
    {
        _parent = parent;
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

        return context;
    }
}