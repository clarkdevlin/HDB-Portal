using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "ECICS API";

            return View();
        }

        [Route("mvc/home/register")]
        public ActionResult Register()
        {
            RegisterBindingModel model = new RegisterBindingModel();

            
            return View(model);
        }
    }
}
