namespace Zinq.Contexts.Annotations;

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
public class ContextAttribute(string name, Type type) : Attribute
{
    public string Name { get; } = name;
    public Type Type { get; } = type;
}