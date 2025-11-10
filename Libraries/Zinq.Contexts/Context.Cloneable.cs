namespace Zinq.Contexts;

public partial class Context : ICloneable
{
    public object Clone()
    {
        return MemberwiseClone();
    }
}