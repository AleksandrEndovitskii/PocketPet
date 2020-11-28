using System.Collections.Generic;

public static class ListExtensions
{
    public static string ToString<T>(this List<T> list)
    {
        var result = string.Join(", ", list);

        return result;
    }

    public static T GetRandomElement<T>(this List<T> list)
    {
        var randomIndex = UnityEngine.Random.Range(0, list.Count);
        var randomElement = list[randomIndex];
        return randomElement;
    }
}
