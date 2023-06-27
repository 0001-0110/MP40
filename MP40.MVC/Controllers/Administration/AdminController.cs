using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.MVC.Mapping;
using MP40.MVC.Models;

namespace MP40.MVC.Controllers.Administration
{
    public abstract class AdminController<TModel> : BaseController<TModel> where TModel : class, IViewModel
    {
        public AdminController(IBijectiveMapper<MvcMapperProfile> mapper, IDataService dataService) : base(mapper, dataService) { }

        protected override bool IsUserAuthorized()
        {
            // TODO Remove temp
            return true;
            return User.Identity?.IsAuthenticated ?? false && User.IsInRole("Admin"); 
        }
    }
}
