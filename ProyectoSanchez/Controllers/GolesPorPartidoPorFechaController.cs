using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProyectoSanchez.ViewModels;
using ProyectoSanchez.Models;

namespace ProyectoSanchez.Controllers
{
    public class GolesPorPartidoPorFechaDataBaseWrapper
    {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public GolesPorPartidoPorFechaDataBaseWrapper()
        {
            db = new Models.ProyectoBasesSanchezEntities();
        }

        public List<GolesPartidos_Result> GetGolesPartidoPorPartidoPorFecha(decimal EqA, decimal EqB, DateTime fecha)
        {
            return db.GolesPartidos(EqA, EqB, fecha).ToList();
        }
    }
    public class GolesPorPartidoPorFechaController : Controller
    {
        // GET: GolesPorPartidoPorFecha
        public ActionResult Index()
        {

            return View();
        }
    }
}