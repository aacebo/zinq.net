namespace Zinq.Contexts;

public interface IContextExtension<TContext, TExtension>
    where TContext : IContext
    where TExtension : IContext<TContext>
{
    TExtension Extend(TContext context);
    IContextBuilder<TExtension> Extend(IContextBuilder<TContext> builder);
}