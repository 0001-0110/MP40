namespace Mappy
{
    // I know that I just could have used AutoMapper, but where is the fun in that ?
    public class Mapper : IMapper
    {
        public Mapper(MappingProfile profile) 
        {

        }

        public TDestination Map<TDestination>(object source)
        {
            throw new NotImplementedException();
        }
    }
}
