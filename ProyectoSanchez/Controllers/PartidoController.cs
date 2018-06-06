using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoSanchez.ViewModels;
namespace ProyectoSanchez.Controllers
{
    public class PartidoControllerDataBaseWrapper
    {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public PartidoControllerDataBaseWrapper()
        {
            db = new Models.ProyectoBasesSanchezEntities();
        }

        public List<PartidoVM> GetPartidosXFechaYTorneo(int idFecha, int idTorneo) {
            return (from Partido in db.Partidoes
                    join Torneo in db.Torneos on Partido.idTorneo equals Torneo.idTorneo
                    join Fecha in db.FechasCalendarios on Partido.idFecha equals Fecha.idFecha
                    where Partido.idFecha == idFecha && Partido.idTorneo == idTorneo
                    select new PartidoVM
                    {
                        IdEquipoLocal = Partido.idEquipoLocal,
                    }
                    ).ToList();
        }
        public int GetIdFecha()
        {
            string idFecha = HttpContext.Current.Request.QueryString["IdFecha"];
            if (idFecha == null)
            {
                return 0;
            }
            return Convert.ToInt32(idFecha);
        }

        public int GetIdTorneo()
        {
            string idTorneo = HttpContext.Current.Request.QueryString["IdTorneo"];
            if (idTorneo == null)
            {
                return 0;
            }
            return Convert.ToInt32(idTorneo);
        }

    }
    public class PartidoController : Controller
    {
        private PartidoControllerDataBaseWrapper _db;
        private static int idFecha;
        private static int idTorneo;
        public PartidoController()
        {
            _db = new PartidoControllerDataBaseWrapper();

        }

        // GET: Partido
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPartidosPorFechaYTorneo()
        {
            try
            {
                idFecha = _db.GetIdFecha();
                idTorneo = _db.GetIdTorneo();
                // Obtenemos la lista de Fechas programadas desde la base de datos
                //List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = _db.GetPartidosXFechaYTorneo(idFecha,idTorneo),
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