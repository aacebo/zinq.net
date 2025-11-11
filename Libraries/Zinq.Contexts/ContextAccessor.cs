namespace Zinq.Contexts;

public class ContextAccessor(IServiceProvider provider) : IMutableContextAccessor
{
    protected static readonly AsyncLocal<IReadOnlyContext?> Async = new();

    public IReadOnlyContext Value
    {
        get
        {
            Async.Value ??= (IContext?)provider.GetService(typeof(IContext)) ?? throw new Exception("IContext not found");
            return Async.Value;
        }
        private set
        {
            Async.Value = value;
        }
    }

    public IContextSnapshot Snapshot()
    {
        return new ContextSnapshot(Async.Value ?? (IContext?)provider.GetService(typeof(IContext)) ?? throw new Exception("IContext not found"));
    }

    public void Next(IReadOnlyContext context)
    {
        Async.Value = context;
    }
}