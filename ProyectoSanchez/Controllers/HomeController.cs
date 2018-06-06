using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSanchez.Controllers
{
    public class HomeControllerDataBaseWrapper 
    {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public HomeControllerDataBaseWrapper()
        {
            db = new Models.ProyectoBasesSanchezEntities();
        }

    }

    public class HomeController : Controller
    {
        private HomeControllerDataBaseWrapper _db;

        public HomeController()
        {
            _db = new HomeControllerDataBaseWrapper();

        }
        public ActionResult Index()
        {
            return View();
        }
    }
}