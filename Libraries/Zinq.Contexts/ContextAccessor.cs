namespace Zinq.Contexts;

public class ContextAccessor(IServiceProvider provider) : IMutableContextAccessor
{
    protected static readonly AsyncLocal<IReadOnlyContext?> Async = new();

    public IReadOnlyContext Value
    {
        get
        {
            Async.Value ??= new Context(provider);
            return Async.Value;
        }
        private set
        {
            Async.Value = value;
        }
    }

    public IContextSnapshot Snapshot()
    {
        return new ContextSnapshot(Async.Value ?? new Context(provider));
    }

    public void Next(IReadOnlyContext context)
    {
        Async.Value = context;
    }
}