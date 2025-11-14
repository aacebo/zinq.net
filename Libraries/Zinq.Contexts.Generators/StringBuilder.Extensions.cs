using System.Text;

namespace Zinq.Contexts.Generators;

public static partial class Extensions
{
    public static StringBuilder AppendIf(this StringBuilder builder, Func<bool> predicate, params IEnumerable<string?> lines)
    {
        if (predicate())
        {
            foreach (var line in lines)
            {
                if (line is null) continue;
                builder = builder.AppendLine(line);
            }
        }

        return builder;
    }

    public static StringBuilder AppendIf(this StringBuilder builder, bool predicate, params IEnumerable<string?> lines)
    {
        if (predicate)
        {
            foreach (var line in lines)
            {
                if (line is null) continue;
                builder = builder.AppendLine(line);
            }
        }

        return builder;
    }
}