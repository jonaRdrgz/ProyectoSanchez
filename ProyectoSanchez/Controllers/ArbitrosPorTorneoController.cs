using ProyectoSanchez.Models;
using ProyectoSanchez.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoSanchez.Controllers
{
    public class ArbitrosPorTorneoControllerDataBaseWrapper
    {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public ArbitrosPorTorneoControllerDataBaseWrapper()
        {
            db = new Models.ProyectoBasesSanchezEntities();
        }

        public List<TorneoVM> getListaTorneos()
        {
            return (from Torneo in db.Torneos
                    select new TorneoVM
                    {
                        IdTorneo = Torneo.idTorneo,
                        Nombre = Torneo.nombre

                    }).ToList();
        }

        public List<InformacionArbitrosTorneo_Result> GetInformacionArbitrosPorTorneo(decimal idTorneo)
        {
            return db.InformacionArbitrosTorneo(idTorneo).ToList();
        }
    }
    public class ArbitrosPorTorneoController : Controller
    {
        private ArbitrosPorTorneoControllerDataBaseWrapper _db;
        public ArbitrosPorTorneoController()
        {
            _db = new ArbitrosPorTorneoControllerDataBaseWrapper();

        }
        public ActionResult Index()
        {
            ViewBag.Torneos = _db.getListaTorneos();
            return View();
        }

        public JsonResult GetInformacionArbitrosPorTorneo(decimal idTorneo)
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                List<InformacionArbitrosTorneo_Result> informacion = _db.GetInformacionArbitrosPorTorneo(idTorneo);
                return new JsonResult()
                {
                    Data = informacion,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            // En caso de ocurrir una excepción, se atrapa la excepción y se retorna un código ERROR
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

                return new JsonResult()
                {
                    Data = new { CODE = "ERROR" },
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }
    }
}