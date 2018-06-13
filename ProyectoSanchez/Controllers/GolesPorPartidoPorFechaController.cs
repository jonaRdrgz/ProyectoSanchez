using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProyectoSanchez.ViewModels;
using ProyectoSanchez.Models;

namespace ProyectoSanchez.Controllers
{
    public class GolesPorPartidoPorFechaControllerDataBaseWrapper
    {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public GolesPorPartidoPorFechaControllerDataBaseWrapper()
        {
            db = new Models.ProyectoBasesSanchezEntities();
        }

        public List<proc_anotadoresEquiposDeFecha_Result> GetGolesPartidoPorPartidoPorFecha(decimal idEquipoA, decimal idEquipoB,DateTime fecha)
        {
            return db.proc_anotadoresEquiposDeFecha(idEquipoA, idEquipoB,fecha).ToList();
        }
    }
    public class GolesPorPartidoPorFechaController : Controller
    {
        private GolesPorPartidoPorFechaControllerDataBaseWrapper _db;
        private static Decimal idEquipoA;
        private static Decimal idEquipoB;
        private static DateTime fecha;
        public GolesPorPartidoPorFechaController()
        {
            _db = new GolesPorPartidoPorFechaControllerDataBaseWrapper();

        }
        // GET: GolesPorPartidoPorFecha
        public ActionResult Index()
        {
            idEquipoA = GetIdEquipoA();
            idEquipoB = GetIdEquipoB();
            fecha = GetFecha();
            return View();
        }

        public JsonResult GetGolesPorPartidoPorFecha()
        {
            try
            {
                return new JsonResult()
                {
                    Data = _db.GetGolesPartidoPorPartidoPorFecha(idEquipoA,idEquipoB,fecha),
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
        public decimal GetIdEquipoA()
        {
            string idEquipoA = Request.QueryString["idEquipoA"];
            if (idEquipoA == null)
            {
                return 1;
            }
            return Convert.ToDecimal(idEquipoA);
        }
        public decimal GetIdEquipoB()
        {
            string idEquipoB = Request.QueryString["idEquipoB"];
            if (idEquipoB == null)
            {
                return 2;
            }
            return Convert.ToDecimal(idEquipoB);
        }
        public DateTime GetFecha()
        {
            string fecha = Request.QueryString["fecha"];
            if (fecha == null)
            {
                return Convert.ToDateTime("2017/06/01");
            }
            return Convert.ToDateTime(fecha);
        }
    }
}