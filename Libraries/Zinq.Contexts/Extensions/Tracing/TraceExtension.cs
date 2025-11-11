namespace Zinq.Contexts.Extensions.Tracing;

public class TraceExtension : IContextExtension
{
    public void Apply(IContext context)
    {
        if (!context.Has(Keys.TraceId))
        {
            context.Set(Keys.TraceId, Guid.NewGuid().ToString());
        }
    }
}