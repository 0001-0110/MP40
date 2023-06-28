using MP40.BLL.Mapping;
using MP40.MVC.Models;
using MP40.MVC.Utilities;

namespace MP40.MVC.Mapping
{
    public class MvcMapperProfile : BijectiveProfile
    {
        public MvcMapperProfile()
        {
            CreateBijectiveMap<BLL.Models.Country, Country>();
            CreateBijectiveMap<BLL.Models.Genre, Genre>();
            CreateBijectiveMap<BLL.Models.Image, Image>();
            CreateBijectiveMap<BLL.Models.Notification, Notification>();
            CreateBijectiveMap<BLL.Models.Tag, Tag>();
            CreateBijectiveMap<BLL.Models.User, User>();
            CreateBijectiveMap<BLL.Models.Video, Video>(
                source => source.ForMember(destination => destination.Image, source => source.MapFrom(source => ImageUtility.ToMvcImage(source.Image))),
                source => source.ForMember(destination => destination.Image, source => source.MapFrom(source => ImageUtility.ToBllImage(source.Image))));

            // Not sure to keep this one
            CreateBijectiveMap<BLL.Models.VideoTag, VideoTag>();
        }
    }
}
