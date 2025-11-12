namespace Zinq.Contexts;

public class ContextBuilder : IContextBuilder<IContext>
{
    private readonly Dictionary<string, IResolver> _values = [];

    public ContextBuilder()
    {

    }

    public IContextBuilder<IContext> With(string key, IResolver resolver)
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

        return context;
    }
}