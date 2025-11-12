namespace Zinq.Contexts;

public interface IContext : IReadOnlyContext
{
    IContext Set(string key, IResolver resolver);
    IReadOnlyContext ToReadOnly();
}

public interface IContext<TInnerContext> : IContext where TInnerContext : IContext;