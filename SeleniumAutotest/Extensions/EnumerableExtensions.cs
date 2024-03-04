namespace SeleniumAutotest.Extensions
{
    public static class EnumerableExtensions
    {
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int count)
        {
            return source
              .Select((x, y) => new { Index = y, Value = x })
              .GroupBy(x => x.Index / count)
              .Select(x => x.Select(y => y.Value).ToList())
              .ToList();
        }
    }
}
