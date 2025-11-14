using Microsoft.CodeAnalysis;

namespace Zinq.Analyze;

public class Module
{
    private readonly IList<IComponent> _components = [];

    public Module With<TSyntax>(IComponent component) where TSyntax : SyntaxNode
    {
        _components.Add(component);
        return this;
    }

    public void Invoke(IncrementalGeneratorInitializationContext context)
    {
        foreach (var component in _components)
        {
            var sources = context.SyntaxProvider
                    .CreateSyntaxProvider(
                        predicate: (node, cancellationToken) => component.Select(node),
                        transform: (syntaxContext, cancellationToken) =>
                        {
                            var symbol = syntaxContext.SemanticModel.GetDeclaredSymbol(syntaxContext.Node, cancellationToken) ?? throw new Exception("symbol not found");
                            var source = component switch
                            {
                                ITypeComponent type => type.Build((INamedTypeSymbol)symbol),
                                IInterfaceComponent iface => iface.Build((INamedTypeSymbol)symbol),
                                IClassComponent @class => @class.Build((INamedTypeSymbol)symbol),
                                IPropertyComponent property => property.Build((IPropertySymbol)symbol),
                                _ => throw new InvalidOperationException("component type not found")
                            };

                            return (symbol, syntaxContext.Node, source);
                        }
                    );

            context.RegisterSourceOutput(sources, static (ctx, value) =>
            {
                var (symbol, syntax, source) = value;

                try
                {
                    ctx.AddSource($"{source.Name}.g.cs", source.Content.Render());
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
                        syntax.GetLocation(),
                        ex.ToString())
                    );
                }
            });
        }
    }
}