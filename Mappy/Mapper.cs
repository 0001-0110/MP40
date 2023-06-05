using System.Reflection;

namespace Mappy
{
    public class Mapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source) where TSource : class where TDestination : class, new()
        {
            TDestination destination = new TDestination();
            Type destinationType = typeof(TDestination);
            foreach (PropertyInfo sourceProperty in source.GetType().GetProperties())
            {
                PropertyInfo? destinationProperty = destinationType.GetProperty(sourceProperty.Name);
                if (destinationProperty != null
                    && destinationProperty.CanWrite
                    && destinationProperty.PropertyType == sourceProperty.PropertyType)
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
            }
            return destination;
        }
    }
}
