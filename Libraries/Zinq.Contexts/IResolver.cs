namespace Zinq.Contexts;

public interface IResolver
{
    object Resolve(IReadOnlyContext context);
}

public interface IAsyncResolver
{
    Task<object> ResolveAsync(IReadOnlyContext context, CancellationToken cancellationToken = default);
}