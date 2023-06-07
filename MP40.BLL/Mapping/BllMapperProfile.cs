using MP40.BLL.Models;

namespace MP40.BLL.Mapping
{
    public class BllMapperProfile : BijectiveProfile
    {
        public BllMapperProfile() 
        {
            CreateBijectiveMap<DAL.Models.Country, Country>();
            CreateBijectiveMap<DAL.Models.Genre, Genre>();
            CreateBijectiveMap<DAL.Models.Image, Image>();
            CreateBijectiveMap<DAL.Models.Notification, Notification>();
            CreateBijectiveMap<DAL.Models.Tag, Tag>();
            CreateBijectiveMap<DAL.Models.User, User>();
            CreateBijectiveMap<DAL.Models.Video, Video>();
            // Not sure to keep this one
            CreateBijectiveMap<DAL.Models.VideoTag, VideoTag>();
        }
    }
}
