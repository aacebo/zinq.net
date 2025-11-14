using Microsoft.CodeAnalysis;

namespace Zinq.Analyze;

public interface ISymbolProvider<TSymbol> where TSymbol : ISymbol
{
    bool Select(SyntaxNode node, CancellationToken cancellationToken = default);
    TSymbol Provide(GeneratorSyntaxContext context, CancellationToken cancellationToken = default);
}