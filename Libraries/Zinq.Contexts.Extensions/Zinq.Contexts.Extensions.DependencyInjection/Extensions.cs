namespace Zinq.Contexts.Extensions.DependencyInjection;

public static partial class Extensions
{
    extension(Keys)
    {
        public static Key<IServiceProvider> Provider => new("provider");
    }

    public static IContextBuilder<IProviderContext<TContext>> WithProvider<TContext>(this IContextBuilder<TContext> builder, IServiceProvider provider)
        where TContext : IContext
    {
        var extension = new ProviderExtension<TContext>(provider);
        return extension.Extend(builder);
    }
}