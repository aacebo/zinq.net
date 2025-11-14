namespace Zinq.Contexts.Extensions.DependencyInjection;

public static partial class Extensions
{
    extension(Key)
    {
        public static Key<IServiceProvider> Provider => new("microsoft.extensions.provider");
    }

    extension<TContext>(IReadOnly<IProviderContext<TContext>> context) where TContext : IContext
    {
        public IServiceProvider Provider => context.Get(Key.Provider);
    }

    public static IProviderContext<TContext> WithProvider<TContext>(this TContext context, IServiceProvider provider)
        where TContext : IContext
    {
        var extension = new ProviderExtension<TContext>(provider);
        return extension.Extend(context);
    }

    public static IContextBuilder<IProviderContext<TContext>> WithProvider<TContext>(this IContextBuilder<TContext> builder, IServiceProvider provider)
        where TContext : IContext
    {
        var extension = new ProviderExtension<TContext>(provider);
        return extension.Extend(builder);
    }
}