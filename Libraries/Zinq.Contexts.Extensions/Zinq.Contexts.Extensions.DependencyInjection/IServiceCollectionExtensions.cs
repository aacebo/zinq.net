using Microsoft.Extensions.DependencyInjection;

namespace Zinq.Contexts.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddContext(this IServiceCollection services)
    {
        return services
            .AddScoped(provider => Context.New().WithProvider(provider).Build())
            .AddScoped<IReadOnlyContext>(provider => provider.GetRequiredService<IContext>());
    }

    public static IServiceCollection AddContext<TContext>(this IServiceCollection services) where TContext : class, IContext
    {
        return services
            .AddScoped<TContext>()
            .AddScoped<IContext, TContext>(provider => provider.GetRequiredService<TContext>())
            .AddScoped<IReadOnlyContext>(provider => provider.GetRequiredService<IContext>());
    }

    public static IServiceCollection AddContext(this IServiceCollection services, Func<IServiceProvider, IContextBuilder, IContextBuilder> action)
    {
        return services
            .AddScoped<IContext, Context>(provider =>
            {
                var builder = new ContextBuilder().WithProvider(provider);
                return (Context)action(provider, builder).Build();
            })
            .AddScoped<IReadOnlyContext>(provider => provider.GetRequiredService<IContext>());
    }

    public static IServiceCollection AddContextAccessor(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMutableContextAccessor, ContextAccessor>()
            .AddSingleton<IContextAccessor>(provider => provider.GetRequiredService<IMutableContextAccessor>());
    }

    public static IServiceCollection AddContextAccessor<TContextAccessor>(this IServiceCollection services)
        where TContextAccessor : class, IContextAccessor
    {
        return services
            .AddSingleton<TContextAccessor>()
            .AddSingleton<IContextAccessor, TContextAccessor>(provider => provider.GetRequiredService<TContextAccessor>());
    }

    public static IServiceCollection AddMutableContextAccessor<TContextAccessor>(this IServiceCollection services)
        where TContextAccessor : class, IMutableContextAccessor
    {
        return services
            .AddSingleton<TContextAccessor>()
            .AddSingleton<IMutableContextAccessor, TContextAccessor>(provider => provider.GetRequiredService<TContextAccessor>())
            .AddSingleton<IContextAccessor>(provider => provider.GetRequiredService<IMutableContextAccessor>());
    }
}