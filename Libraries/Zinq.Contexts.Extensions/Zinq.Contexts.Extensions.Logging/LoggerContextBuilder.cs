// namespace Zinq.Contexts.Extensions.Logging;

// public class LoggerContextBuilder<TContext>(IContextBuilder<TContext> builder) : IContextBuilder<ILoggerContext<TContext>> where TContext : IContext
// {
//     public IContextBuilder<ILoggerContext<TContext>> With(string key, IResolver resolver)
//     {
//         builder = builder.With(key, resolver);
//         return this;
//     }

//     public ILoggerContext<TContext> Build()
//     {
//         var context = builder.Build();
//         return new LoggerContext<TContext>(context);
//     }
// }