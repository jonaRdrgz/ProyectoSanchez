using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoSanchez.ViewModels;
using System.Data.Entity.Validation;

namespace ProyectoSanchez.Controllers
{
    public class ConsultaEncuentrosDataBaseWrapper {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public ConsultaEncuentrosDataBaseWrapper()
        {
            db = new Models.ProyectoBasesSanchezEntities();
        }

        public List<EquipoVM> GetEquipos()
        {
            return (from Equipo in db.Equipoes
                    select new EquipoVM {
                        Nombre = Equipo.nombre,
                        IdEquipo = Equipo.idEquipo
                    }
                ).ToList();
        }
    }
    public class ConsultaEncuentrosController : Controller
    {
        private ConsultaEncuentrosDataBaseWrapper _db;

        public ConsultaEncuentrosController()
        {
            _db = new ConsultaEncuentrosDataBaseWrapper();

        }
        // GET: ConsultaEncuentros
        public ActionResult Index()
        {
            ViewBag.equipos = _db.GetEquipos();
            return View();
        }

        public JsonResult GetInformacionEquipos()
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                //List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = _db.GetEquipos(),
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