namespace PaymentApplyProject.Application.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool AnyWithNullCheck<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => source != null && source.Any();
    }
}
