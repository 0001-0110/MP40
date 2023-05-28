namespace Mappy
{
    public interface IMapper
    {
        TDestination Map<TDestination>(object source);
    }
}
