using MP40.BLL.Models;

namespace MP40.BLL.Mapping
{
    public class BllMapperProfile : BijectiveProfile
    {
        public BllMapperProfile() 
        {
            CreateMap<DAL.Models.Genre, Genre>();
            CreateMap<Genre, DAL.Models.Genre>();
        }
    }
}
