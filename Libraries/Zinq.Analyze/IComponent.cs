using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Zinq.Analyze;

public interface IComponent
{
    bool Select(SyntaxNode syntax);
}

public interface ITypeComponent : IComponent
{
    bool Select(TypeDeclarationSyntax syntax);
    ISource Build(INamedTypeSymbol symbol);
}

public interface IInterfaceComponent : IComponent
{
    bool Select(InterfaceDeclarationSyntax symbol);
    ISource Build(INamedTypeSymbol symbol);
}

public interface IClassComponent : IComponent
{
    bool Select(ClassDeclarationSyntax symbol);
    ISource Build(INamedTypeSymbol symbol);
}

public interface IPropertyComponent : IComponent
{
    bool Select(PropertyDeclarationSyntax symbol);
    ISource Build(IPropertySymbol symbol);
}

public static partial class Extensions
{
    extension(ITypeComponent component)
    {
        public bool Select(SyntaxNode node) => node switch
        {
            TypeDeclarationSyntax type => component.Select(type),
            _ => false
        };
    }

    extension(IInterfaceComponent component)
    {
        public bool Select(SyntaxNode node) => node switch
        {
            InterfaceDeclarationSyntax type => component.Select(type),
            _ => false
        };
    }

    extension(IClassComponent component)
    {
        public bool Select(SyntaxNode node) => node switch
        {
            ClassDeclarationSyntax type => component.Select(type),
            _ => false
        };
    }

    extension(IPropertyComponent component)
    {
        public bool Select(SyntaxNode node) => node switch
        {
            PropertyDeclarationSyntax type => component.Select(type),
            _ => false
        };
    }
}