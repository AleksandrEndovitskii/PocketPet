using System.Collections.Generic;

public static class ListExtensions
{
    public static string ToString<T>(this List<T> list)
    {
        var result = string.Join(", ", list);

        return result;
    }
}
