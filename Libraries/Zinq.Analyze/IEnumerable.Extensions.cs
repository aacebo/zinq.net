namespace Zinq.Analyze;

public static partial class Extensions
{
    public static string Render(this IEnumerable<string> list, string delim = "")
    {
        return string.Join(delim, list);
    }

    public static string Render(this IEnumerable<ISource> list, string delim = "")
    {
        return string.Join(delim, list.Select(src => src.Render()));
    }
}