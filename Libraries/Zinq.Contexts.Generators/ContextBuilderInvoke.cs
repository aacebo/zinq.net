using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Zinq.Contexts.Generators;

public sealed class ContextBuilderInvoke
{
    public string Name => Type.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
    public string SafeName
    {
        get
        {
            var name = Name
                .Replace(".", "_")
                .Replace("<", "_")
                .Replace(">", "_")
                .Replace(",", "_")
                .Replace(" ", "");

            if (name.Last() == '_')
            {
                name = name.Trim("_").ToString();
            }

            return name;
        }
    }

    public required InvocationExpressionSyntax Invoke { get; init; }
    public required INamedTypeSymbol Type { get; init; }
}