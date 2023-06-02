﻿using MP40.BLL.Models;
using MP40.BLL.Services;

namespace MP40.Controllers
{
    public class TagController : Controller<Tag>
    {
        public TagController(ILogger<Controller<Tag>> logger, IDataService dataService) : base(logger, dataService) { }
    }
}
