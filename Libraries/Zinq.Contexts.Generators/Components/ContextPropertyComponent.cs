using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Zinq.Analyze;

namespace Zinq.Contexts.Generators.Components;

public class ContextPropertyComponent : IPropertyComponent
{
    public bool Select(PropertyDeclarationSyntax syntax) => true;
    public ISource Build(IPropertySymbol symbol)
    {
        return new Source()
        {
            Name = symbol.FullName,
            Content = $$"""
            public {{symbol.Type.FullName}} {{symbol.FullName}}
            {
                {{(
                    symbol.GetMethod is not null
                        ? $"get => Get(Key.{symbol.FullName});\n"
                        : string.Empty
                )}}{{(
                    symbol.SetMethod is not null
                        ? $"set => Set(Key.{symbol.FullName}, value);"
                        : string.Empty
                )}}
            }
            """.Split('\n')
        };
    }
}