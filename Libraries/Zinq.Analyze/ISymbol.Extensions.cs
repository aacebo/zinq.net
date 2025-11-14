using Microsoft.CodeAnalysis;

namespace Zinq.Analyze;

public static partial class Extensions
{
    extension(ISymbol symbol)
    {
        public string ShortName => symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
        public string FullName => symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    }

    extension(ITypeSymbol type)
    {
        public string ToIdentifier()
        {
            var name = type.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)
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
}