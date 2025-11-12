using Microsoft.Extensions.DependencyInjection;

namespace Zinq.Contexts.Extensions.DependencyInjection;

public class ContextAccessor(IServiceProvider provider) : IMutableContextAccessor
{
    protected static readonly AsyncLocal<IReadOnlyContext?> Async = new();

    public IReadOnlyContext Value
    {
        get
        {
            Async.Value ??= provider.GetRequiredService<IContext>();
            return Async.Value;
        }
        private set
        {
            Async.Value = value;
        }
    }

    public IContextSnapshot Snapshot()
    {
        return new ContextSnapshot(Async.Value ?? provider.GetRequiredService<IContext>());
    }

    public void Next(IReadOnlyContext context)
    {
        Async.Value = context;
    }
}