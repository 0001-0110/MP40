using System.Reflection;

namespace MP40.DAL.Extensions
{
    public static class ObjectExtensions
    {
        public static void CopyDataTo<TSource, TDestination>(this TSource source, TDestination destination, bool overrideValues = false) where TSource : class where TDestination : class
        {
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            PropertyInfo[] sourceProperties = source.GetType().GetProperties(bindingFlags);
            PropertyInfo[] destinationProperties = destination.GetType().GetProperties(bindingFlags);
            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo? destinationProperty = Array.Find(destinationProperties, property => property.Name == sourceProperty.Name);
                if (destinationProperty != null && destinationProperty.PropertyType == sourceProperty.PropertyType && destinationProperty.CanWrite)
                {
                    if ((!overrideValues && destinationProperty.GetValue(destination) != default) || sourceProperty.GetValue(source) == default)
                        continue;
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
            }
        }

        public static void CopyDataFrom<TSource, TDestination>(this TDestination destination, TSource source, bool overrideValues = false) where TSource : class where TDestination : class
        {
            source.CopyDataTo(destination, overrideValues);
        }

        public static TDestination To<TDestination>(this object source) where TDestination : class, new()
        {
            TDestination destination = new TDestination();
            destination.CopyDataFrom(source);
            return destination;
        }
    }
}
