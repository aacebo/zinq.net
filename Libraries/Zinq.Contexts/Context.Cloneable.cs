namespace Zinq.Contexts;

public partial class Context : ICloneable
{
    object ICloneable.Clone() => MemberwiseClone();
    public Context Clone() => (Context)MemberwiseClone();
}