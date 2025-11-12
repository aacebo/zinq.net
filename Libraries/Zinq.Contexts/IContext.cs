namespace Zinq.Contexts;

public interface IContext : IReadOnlyContext
{
    IContext Set(string key, IResolver resolver);
    IReadOnlyContext ToReadOnly();
}

public interface IContext<TParent> : IContext, IReadOnlyContext<TParent> where TParent : IContext;