using MP40.BLL.Models;

namespace MP40.BLL.Mapping
{
    public class BllMapperProfile : BijectiveProfile
    {
        public BllMapperProfile() 
        {
            CreateBijectiveMap<DAL.Models.Genre, Genre>();
        }
    }
}
