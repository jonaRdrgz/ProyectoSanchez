using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoSanchez.ViewModels;

namespace ProyectoSanchez.Controllers
{
    public class HomeControllerDataBaseWrapper 
    {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public HomeControllerDataBaseWrapper()
        {
            db = new Models.ProyectoBasesSanchezEntities();
        }

        public List<TorneoVM> getListaTorneos() {
            return (from Torneo in db.Torneos
                    select new TorneoVM
                    {
                        IdTorneo = Torneo.idTorneo,
                        Nombre = Torneo.nombre
                        
                    }).ToList();
        }

        public List<FechasCalendarioVM> GetFechasCalendario(decimal idTorneo)
        {
            return (from FechasCalendario in db.FechasCalendarios
                    join Torneo in db.Torneos on FechasCalendario.idTorneo equals Torneo.idTorneo
                    where  FechasCalendario.idTorneo == idTorneo 
                    select new FechasCalendarioVM
                    {
                        FechaProgramada = FechasCalendario.fechaProgramada,
                        IdFecha = FechasCalendario.idFecha,
                        IdTorneo = FechasCalendario.idTorneo
                    }).ToList();

        }

    }

    public class HomeController : Controller
    {
        private HomeControllerDataBaseWrapper _db;

        public HomeController()
        {
            _db = new HomeControllerDataBaseWrapper();

        }

        /* Retorna la vista principal
         *
         */
        public ActionResult Index()
        {
            ViewBag.Torneos = _db.getListaTorneos();
            return View();
        }

        /*
         * GetFechasProgramadasXTorneo: método para cargar las fechas programadas por torneo
         * 
         * Valor de retorno:
         * Un JsonResult (JSON) con un listado de  las  fechas programadas, 
         * o un mensaje ERROR en caso de que ocurra una excepción interna.
         */
        public JsonResult GetFechasProgramadasXTorneo(decimal idTorneo)
        {
            try
            {
                // Obtenemos la lista de Fechas programadas desde la base de datos
                List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = fechas,
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

