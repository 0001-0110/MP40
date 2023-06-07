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

        public new IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            Mappings.Add(typeof(TSource), typeof(TDestination));
            return base.CreateMap<TSource, TDestination>();
        }
        
        // TODO Handle ReverseMap
    }
}
