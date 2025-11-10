namespace Zinq.Contexts;

public interface IContextAccessor
{
    IReadOnlyContext Value { get; }

    IContextSnapshot Snapshot();
}

public interface IMutableContextAccessor : IContextAccessor
{
    void Next(IReadOnlyContext context);
}

public interface ILinearContextAccessor : IMutableContextAccessor
{
    void Back(IReadOnlyContext context);
    IReadOnlyContext Back();
}