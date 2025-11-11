namespace Zinq.Contexts.Annotations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ContextAttribute : Attribute
{
    public string? Name { get; init; }

    public ContextAttribute()
    {

    }

    public ContextAttribute(string name)
    {
        Name = name;
    }
}