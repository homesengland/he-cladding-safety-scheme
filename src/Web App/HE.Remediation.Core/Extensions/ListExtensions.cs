namespace HE.Remediation.Core.Extensions;

public static class ListExtensions
{
    private static readonly Random _rand = new();

    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            var k = _rand.Next(i + 1);
            (list[i], list[k]) = (list[k], list[i]);
        }
    }
}
