namespace Zinq.Contexts;

public interface IContextExtension<TContext, TExtension>
    where TContext : IContext
    where TExtension : IContext<TContext>
{
    IContextBuilder<TExtension> Extend(IContextBuilder<TContext> builder);
}