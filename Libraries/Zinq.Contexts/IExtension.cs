namespace Zinq.Contexts;

public interface IExtension<TProperty>
{
    void Extend<TContext>(TContext context) where TContext : IContext;
}