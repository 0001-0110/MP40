using AutoMapper;

namespace MP40.BLL.Mapping
{
    public interface IBijectiveMapper<TProfile> : IMapper where TProfile : BijectiveProfile
    {
        public Type GetMappedType(Type type);

        public Type? GetMappedTypeOrNull(Type type);

        public object? Map(Type type, object model);

        public IEnumerable<T> MapRange<T>(IEnumerable<object> values);
    }
}
