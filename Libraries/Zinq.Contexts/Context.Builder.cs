namespace Zinq.Contexts;

public partial class Context
{
    public static Builder New(IServiceProvider provider) => new(provider);
    public static Builder New(IContext parent) => new Builder(parent.Provider).TraceId(parent.TraceId).Parent(parent);
    public class Builder(IServiceProvider provider)
    {
        private string? _traceId;
        private IContext? _parent;
        private readonly Dictionary<string, IResolver> _values = [];

        public Builder TraceId(string traceId)
        {
            _traceId = traceId;
            return this;
        }

        public Builder Parent(IContext parent)
        {
            _parent = parent;
            return this;
        }

        public Builder Set(string key, IResolver resolver)
        {
            _values.Add(key, resolver);
            return this;
        }

        public Context Build()
        {
            var context = new Context(provider);

            if (_traceId is not null)
            {
                context.TraceId = _traceId;
            }

            if (_parent is not null)
            {
                context.Parent = _parent;
            }

            context.Values = _values;
            return context;
        }
    }
}