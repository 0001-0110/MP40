﻿using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.MVC.Mapping;
using MP40.MVC.Models;

namespace MP40.MVC.Controllers.Public
{
    public abstract class PublicController<TModel> : BaseController<TModel> where TModel : class, IViewModel
    {
        public PublicController(IBijectiveMapper<MvcMapperProfile> mapper, IDataService dataService) : base(mapper, dataService) { }

        protected override bool IsUserAuthorized()
        {
            // TODO Remove temp
            return true;
            return User.Identity?.IsAuthenticated ?? false;
        }
    }
}
