using Microsoft.Extensions.DependencyInjection;

namespace Zinq.Contexts;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddContext(this IServiceCollection services)
    {
        return services
            .AddScoped<IContext, Context>()
            .AddScoped<IReadOnlyContext>(provider => provider.GetRequiredService<IContext>());
    }

    public static IServiceCollection AddContext<TContext>(this IServiceCollection services) where TContext : class, IContext
    {
        return services
            .AddScoped<IContext, TContext>()
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
        return services.AddSingleton<IContextAccessor, TContextAccessor>();
    }

    public static IServiceCollection AddMutableContextAccessor<TContextAccessor>(this IServiceCollection services)
        where TContextAccessor : class, IMutableContextAccessor
    {
        return services
            .AddSingleton<IMutableContextAccessor, TContextAccessor>()
            .AddSingleton<IContextAccessor>(provider => provider.GetRequiredService<IMutableContextAccessor>());
    }
}