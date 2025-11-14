using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Zinq.Contexts.Generators.Emitters;

namespace Zinq.Contexts.Generators;

[Generator]
public sealed class ContextGenerator : IIncrementalGenerator
{
    internal static IContextSourceEmitter ClassEmitter = new ContextClassEmitter();
    internal static IContextSourceEmitter BuilderExtensionEmitter = new ContextBuilderExtensionEmitter();

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //
        // STEP 1: Find syntax that looks like builder.Build()
        //
        var invocations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: (node, _) =>
                {
                    return node is InvocationExpressionSyntax inv &&
                        inv.Expression is MemberAccessExpressionSyntax maes &&
                        maes.Name.Identifier.Text == "Build";
                },
                transform: (ctx, ct) =>
                {
                    var invocation = (InvocationExpressionSyntax)ctx.Node;
                    var model = ctx.SemanticModel;

                    // Symbol of the invoked method
                    if (model.GetSymbolInfo(invocation, ct).Symbol is not IMethodSymbol symbol)
                        return default;

                    // Check method name
                    if (symbol.Name != "Build")
                        return default;

                    // Receiver: something.Build()
                    if ((symbol.ReceiverType ?? symbol.ContainingType) is not INamedTypeSymbol receiverType)
                        return default;

                    // Only interested in ContextBuilder<TContext>
                    if (receiverType.OriginalDefinition.ToDisplayString() != "Zinq.Contexts.IContextBuilder<TContext>")
                        return default;

                    // Extract the TContext type
                    if (receiverType.TypeArguments[0] is not INamedTypeSymbol contextType)
                        return default;

                    return new ContextBuilderInvoke
                    {
                        Invoke = invocation,
                        Type = contextType
                    };
                }
            )
            .Where(m => m is not null);

        //
        // STEP 2: Generate output for each invocation
        //
        context.RegisterSourceOutput(invocations, static (ctx, invocation) =>
        {
            try
            {
                if (invocation is null) throw new InvalidOperationException();
                ClassEmitter.Emit(ctx, invocation);
                BuilderExtensionEmitter.Emit(ctx, invocation);
            }
            catch (Exception ex)
            {
                ctx.ReportDiagnostic(Diagnostic.Create(
                    new DiagnosticDescriptor(
                        id: "ZCTXGEN001",
                        title: "Generator error",
                        messageFormat: "Context generator threw: {0}",
                        category: "Zinq.Contexts",
                        DiagnosticSeverity.Error,
                        isEnabledByDefault: true),
                    invocation?.Invoke.GetLocation(),
                    ex.ToString())
                );
            }
        });
    }
}