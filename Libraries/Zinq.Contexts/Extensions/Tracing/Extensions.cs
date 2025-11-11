namespace Zinq.Contexts.Extensions.Tracing;

public static partial class Extensions
{
    extension(Keys)
    {
        public static Key<string> TraceId => new("trace_id");
    }

    extension(IReadOnlyContext context)
    {
        public string TraceId => context.Get(Keys.TraceId);
    }

    public static TContextBuilder WithTracing<TContextBuilder>(this TContextBuilder builder) where TContextBuilder : IContextBuilder
    {
        return (TContextBuilder)builder.WithExtension<TraceExtension>();
    }
}