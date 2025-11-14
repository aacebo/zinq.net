using Microsoft.CodeAnalysis;

namespace Zinq.Contexts.Generators;

public interface IContextSourceEmitter
{
    void Emit(SourceProductionContext context, ContextBuilderInvoke invoke);
}