using AutoMapper;

namespace MP40.BLL.Mapping
{
    public class BijectiveMapper<TProfile> : Mapper, IBijectiveMapper<TProfile> where TProfile : BijectiveProfile
    {
        private TProfile profile;

        public BijectiveMapper(TProfile profile) : base(new MapperConfiguration(config => config.AddProfile(profile)))
        {
            this.profile = profile;
        }

        public Type GetMappedType(Type type)
        {
            return profile.Mappings[type];
        }

        public Type? GetMappedTypeOrNull(Type type)
        {
            return profile.Mappings.GetValueOrDefault(type);
        }

        public IEnumerable<T> MapRange<T>(IEnumerable<object> values)
        {
            foreach (object value in values)
                yield return Map<T>(value);
        }

        public object? Map(Type type, object model)
        {
            return GetType().GetMethod("Map", new Type[] { typeof(Type), })?.MakeGenericMethod(type).Invoke(this, new object?[] { model, });
        }
    }
}
