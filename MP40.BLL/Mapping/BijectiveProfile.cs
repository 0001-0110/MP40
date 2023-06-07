using AutoMapper;
using System.Collections.Immutable;

namespace MP40.BLL.Mapping
{
    public class BijectiveProfile : Profile
    {
        private Dictionary<Type, Type> _mappings;

        private ImmutableDictionary<Type, Type>? mappings;
        public ImmutableDictionary<Type, Type> Mappings 
        {
            get { if (mappings == null) mappings = _mappings.ToImmutableDictionary(); return mappings; }
            set { mappings = value; } 
        }

        public BijectiveProfile() : base()
        {
            _mappings = new Dictionary<Type, Type>();
        }

        [Obsolete]
        public new IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            throw new AccessViolationException($"Please use {nameof(CreateBijectiveMap)} instead");
        }

        public void CreateBijectiveMap<TSource, TDestination>()
        {
            // Create basic mapping
            CreateBijectiveMap<TSource, TDestination>(mapping => mapping);
        }

        public void CreateBijectiveMap<TSource, TDestination>(Func<IMappingExpression<TSource, TDestination>, IMappingExpression<TSource, TDestination>> customization)
        {
            // This part is taking care of the one to one relationship
            // If any of the two types have already been mapped, then it'll throw an ArgumentException
            _mappings.Add(typeof(TSource), typeof(TDestination));
            _mappings.Add(typeof(TDestination), typeof(TSource));
            IMappingExpression<TSource, TDestination> mapping = base.CreateMap<TSource, TDestination>();
            customization(mapping).ReverseMap();
        }
    }
}
