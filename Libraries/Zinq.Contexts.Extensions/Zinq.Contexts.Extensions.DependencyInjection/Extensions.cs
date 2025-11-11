namespace Zinq.Contexts.Extensions.DependencyInjection;

public static partial class Extensions
{
    extension(Keys)
    {
        public static Key<IServiceProvider> Provider => new("provider");
    }

    extension(IReadOnlyContext context)
    {
        public IServiceProvider Provider => context.Get(Keys.Provider);
    }

    public static TContextBuilder WithProvider<TContextBuilder>(this TContextBuilder builder, IServiceProvider provider) where TContextBuilder : IContextBuilder
    {
        return builder.With(Keys.Provider, provider);
    }
}