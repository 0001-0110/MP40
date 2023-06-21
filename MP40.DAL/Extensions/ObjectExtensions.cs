using System.Reflection;

namespace MP40.DAL.Extensions
{
	public static class ObjectExtensions
	{
		#region CopyData

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

		#endregion

		#region Method calling

		public static object? CallGeneric(this object instance, string methodName, Type type, params object[] arguments)
		{
			return CallGeneric(instance, methodName, new Type[] { type, }, arguments);
		}

		public static object? CallGeneric(this object instance, string methodName, Type[] types, params object[] arguments)
		{
			IEnumerable<Type> argumentTypes1 = arguments.Select(argument => argument.GetType());
			// Testing for types does not work as we can't know the type of the generic methods
			// Checking the number of parameter should be enough for now
			return instance.GetType().GetMethods().Single(method => method.Name == methodName
				&& method.GetGenericArguments().Length == types.Length
				&& method.GetParameters().Length == arguments.Length)
				.MakeGenericMethod(types).Invoke(instance, arguments);
		}

		public static object? CallMethod(this object instance, string methodName, params object[] arguments)
		{
			Type[] argumentTypes = arguments.Select(argument => argument.GetType()).ToArray();
			return instance.GetType().GetMethod(methodName, argumentTypes)!.Invoke(instance, arguments);
		}

		#endregion
	}
}
