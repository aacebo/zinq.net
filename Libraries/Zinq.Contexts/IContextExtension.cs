namespace Zinq.Contexts;

public interface IContextExtension<TContext, TExtension>
    where TContext : IContext
    where TExtension : IExtendedContext<TContext>
{
    IContextBuilder<TExtension> Extend(IContextBuilder<TContext> builder);
}