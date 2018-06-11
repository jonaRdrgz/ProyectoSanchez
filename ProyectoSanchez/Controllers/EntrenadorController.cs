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

    public class EntrenadorControllerDataBaseWrapper
    {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public EntrenadorControllerDataBaseWrapper()
        {
            db = new Models.ProyectoBasesSanchezEntities();
        }

        public List<EntrenadorVM> GetEntrenadores()
        {
            return (from Entrenador in db.Entrenadors
                    join Funcionario in db.Funcionarios on Entrenador.codigoFuncionario equals Funcionario.codigoFuncionario
                    select new EntrenadorVM
                    {
                        IdEntrenador = Entrenador.idEntrenador,
                        CodigoFuncionario = Entrenador.codigoFuncionario,
                        FechaInicio = Entrenador.fechaInicio,
                        NombreEntrenador = Funcionario.nombre

                    }
                   ).ToList();
        }

        public List<EntrenadorVM> GetEntrenador(decimal idEntrenador)
        {
            return (from Entrenador in db.Entrenadors
                    join Funcionario in db.Funcionarios on Entrenador.codigoFuncionario equals Funcionario.codigoFuncionario
                    where Entrenador.idEntrenador == idEntrenador
                    select new EntrenadorVM
                    {
                        IdEntrenador = Entrenador.idEntrenador,
                        CodigoFuncionario = Entrenador.codigoFuncionario,
                        FechaInicio = Entrenador.fechaInicio,
                        FechaNacimiento = Funcionario.fechaNacimiento,
                        NombreEntrenador = Funcionario.nombre,
                        Imagen = Funcionario.urlImagen
                    }
                   ).ToList();
        }
    }

    public class EntrenadorController : Controller
    {
        private EntrenadorControllerDataBaseWrapper _db;
        public EntrenadorController()
        {
            _db = new EntrenadorControllerDataBaseWrapper();

        }


        // GET: Entrenador
        public ActionResult Index()
        {
            ViewBag.entrenador = _db.GetEntrenadores();
            return View();
        }

        public JsonResult GetInformacionEntrenadores()
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                //List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = _db.GetEntrenadores(),
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

        public JsonResult GetInformacionEntrenador(int idEntrenador)
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                //List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = _db.GetEntrenador(idEntrenador),
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