using MP40.BLL.Mapping;
using MP40.MVC.Models;

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
            CreateBijectiveMap<BLL.Models.Video, Video>(source => source
                .ForMember(destination => destination.TagIds, source => source.MapFrom(source => source.VideoTags.Select(videoTag => videoTag.Tag.Id))));
            // Not sure to keep this one
            CreateBijectiveMap<BLL.Models.VideoTag, VideoTag>();
        }
    }
}
