using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Zinq.Contexts.Generators.Components;

namespace Zinq.Contexts.Generators;

[Generator]
public sealed class ContextGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //
        // STEP 1: Find syntax that looks like builder.Build()
        //
        var invocations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: (node, cancellationToken) =>
                {
                    return node is InterfaceDeclarationSyntax declaration
                        && declaration.AttributeLists.Any();
                },
                transform: (ctx, cancellationToken) =>
                {
                    var declaration = (InterfaceDeclarationSyntax)ctx.Node;
                    var symbol = ctx.SemanticModel.GetDeclaredSymbol(declaration);
                    return symbol is not INamedTypeSymbol type ? default : type;
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
                var @class = new ContextComponent().Render(ctx, invocation.Type);
                ctx.AddSource($"{@class.Name}.g.cs", @class.Content.Render());
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