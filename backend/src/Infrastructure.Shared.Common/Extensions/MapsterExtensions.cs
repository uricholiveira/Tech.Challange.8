namespace Infrastructure.Shared.Common.Extensions;

public static class MapsterExtensions
{
    public static void PatchWithNonNullProperties<TSource, TDestination>(this TDestination destination, TSource source,
        List<string> ignoreProperties = null)
        where TSource : class?
        where TDestination : class?
    {
        var sourceProperties = source.GetType().GetProperties().Where(prop =>
            prop.GetValue(source) != null && (ignoreProperties == null || !ignoreProperties.Contains(prop.Name)));
        var destinationProperties = destination.GetType().GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            var targetProperty = destinationProperties.FirstOrDefault(
                x => x.Name == sourceProperty.Name && x.CanWrite);
            if (targetProperty == null) continue;

            var value = sourceProperty.GetValue(source);
            targetProperty.SetValue(destination, value);
        }
    }
}