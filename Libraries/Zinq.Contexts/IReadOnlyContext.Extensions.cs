namespace Zinq.Contexts;

public static partial class ReadOnlyContextExtensions
{
    public static bool Has<T>(this IReadOnlyContext context, Key<T> key) where T : notnull
    {
        return context.Has(key.Name);
    }

    public static T Get<T>(this IReadOnlyContext context, Key<T> key) where T : notnull
    {
        var value = context.Get(key.Name) ?? throw new Exception($"key '{key}' not found");

        if (value is not T casted)
        {
            throw new Exception($"'{key}' => expected type '{typeof(T)}', found '{value.GetType()}'");
        }

        return casted;
    }

    public static bool TryGet<T>(this IReadOnlyContext context, Key<T> key, out T value) where T : notnull
    {
        if (context.TryGet(key.Name, out var o) && o is T output)
        {
            value = output;
            return true;
        }

        value = default!;
        return false;
    }
}