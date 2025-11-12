namespace Zinq.Contexts;

public interface IContext : IReadOnlyContext
{
    void Set(string key, IResolver resolver);
}