namespace Zinq.Contexts;

public interface IContext : IReadOnlyContext
{
    IContext Set(string key, IResolver resolver);
    IContext Extend(IContextExtension extension);
    IReadOnlyContext ToReadOnly();
}