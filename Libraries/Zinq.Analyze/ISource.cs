namespace Zinq.Analyze;

public interface ISource
{
    string Name { get; }
    IEnumerable<string> Content { get; }

    string Render(string delim = "");
}

public class Source : ISource
{
    public required string Name { get; set; }
    public required IEnumerable<string> Content { get; set; }

    public string Render(string delim = "")
    {
        return string.Join(delim, Content);
    }
}
