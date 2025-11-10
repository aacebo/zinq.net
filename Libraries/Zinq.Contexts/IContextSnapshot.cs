namespace Zinq.Contexts;

public interface IContextSnapshot : IDisposable, ICloneable
{
    IReadOnlyContext Value { get; }
    DateTimeOffset CreatedAt { get; }
}