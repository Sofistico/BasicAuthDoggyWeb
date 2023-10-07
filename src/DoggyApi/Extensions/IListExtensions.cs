namespace DoggyApi.Extensions
{
    public static class IListExtensions
    {
        public static T GetRandomItem<T>(this IList<T> values, Random randomUsed)
        {
            return values[randomUsed.Next(values.Count + 1)];
        }
    }
}
