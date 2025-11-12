namespace Zinq.Contexts;

public interface IExtendedContext<TBaseContext> : IContext where TBaseContext : IReadOnlyContext;
public class ExtendedContext<TBaseContext>(TBaseContext context) : Context(context), IExtendedContext<TBaseContext> where TBaseContext : IReadOnlyContext;