using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace appmvclibrary.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("/administrator/[action]/{id?}")]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}