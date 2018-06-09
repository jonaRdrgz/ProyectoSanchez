using ProyectoSanchez.Models;
using ProyectoSanchez.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSanchez.Controllers
{
    public class TorneoControllerDataBaseWrapper
    {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public TorneoControllerDataBaseWrapper()
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

        public List<tablaPosiciones_Result> GetPosicionesTorneo(decimal idTorneo)
        {
            return db.tablaPosiciones(idTorneo).ToList();
        }
    }
    public class TorneoController : Controller
    {
        private TorneoControllerDataBaseWrapper _db;
        public TorneoController()
        {
            _db = new TorneoControllerDataBaseWrapper();

        }
        // GET: Torneo
        public ActionResult Index()
        {
            ViewBag.Torneos = _db.getListaTorneos();
            return View();
        }

        public JsonResult GetPosicionesTorneo(decimal idTorneo)
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                List<tablaPosiciones_Result> posiciones = _db.GetPosicionesTorneo(idTorneo);
                return new JsonResult()
                {
                    Data = posiciones,
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