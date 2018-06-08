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

    public class JugadorControllerDataBaseWrapper
    {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public JugadorControllerDataBaseWrapper()
        {
            db = new Models.ProyectoBasesSanchezEntities();
        }

        public List<JugadorVM> GetJugadores()
        {
            return (from Jugador in db.Jugadors
                    join Funcionario in db.Funcionarios on Jugador.codigoFuncionario equals Funcionario.codigoFuncionario
                    select new JugadorVM
                    {
                        IdJugador = Jugador.idJugador,
                        CodigoFuncionario = Funcionario.codigoFuncionario,
                        PesoKilos = Jugador.pesoKilos,
                        AlturaMetros = Jugador.alturaMetros,
                        Nombre = Funcionario.nombre
                    }
                   ).ToList();
        }
    }

    public class JugadorController : Controller
    {
        private JugadorControllerDataBaseWrapper _db;
        private static int idJugador;
        public JugadorController()
        {
            _db = new JugadorControllerDataBaseWrapper();

        }


        // GET: Jugador
        public ActionResult Index()
        {
            ViewBag.jugadores = _db.GetJugadores();
            return View();
        }

        public JsonResult GetInformacionJugadores()
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                //List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = _db.GetJugadores(),
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