using AutoMapper;

namespace MP40.BLL.Mapping
{
    public class BijectiveProfile : Profile
    {
        public Dictionary<Type, Type> Mappings;

        public BijectiveProfile() : base()
        {
            Mappings = new Dictionary<Type, Type>();
        }

        [Obsolete]
        public new IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            throw new AccessViolationException("Please use CreateBijectiveMap instead");
        }

        public IMappingExpression<TSource, TDestination> CreateBijectiveMap<TSource, TDestination>()
        {
            // Create basic mapping
            return CreateBijectiveMap<TSource, TDestination>(_ => { });
        }

        public IMappingExpression<TSource, TDestination> CreateBijectiveMap<TSource, TDestination>(Action<IMappingExpression<TSource, TDestination>> customization)
        {
            Mappings.Add(typeof(TSource), typeof(TDestination));
            Mappings.Add(typeof(TDestination), typeof(TSource));
            IMappingExpression<TSource, TDestination> mapping = base.CreateMap<TSource, TDestination>();
            customization(mapping);
            mapping.ReverseMap();
            return mapping;
        }
    }
}
