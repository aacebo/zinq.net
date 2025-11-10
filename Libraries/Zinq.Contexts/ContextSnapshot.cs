
namespace Zinq.Contexts;

public class ContextSnapshot(IReadOnlyContext context) : IContextSnapshot
{
    public IReadOnlyContext Value { get; } = context;
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}