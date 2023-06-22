﻿using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.MVC.Mapping;
using MP40.MVC.Models;

namespace MP40.MVC.Controllers.Public
{
	public class VideoController : BaseCrudController<Video>
    {
        public VideoController(IBijectiveMapper<MvcMapperProfile> mapper, IDataService dataService) : base(mapper, dataService) { }
    }
}