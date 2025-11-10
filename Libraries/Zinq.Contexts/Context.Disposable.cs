namespace Zinq.Contexts;

public partial class Context : IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}