using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
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

        public void AddFechasCalendario(Models.FechasCalendario fecha)
        {
            db.FechasCalendarios.Add(fecha);
        }

        public Models.FechasCalendario CreateFechaCalendario()
        {
            return db.FechasCalendarios.Create();
        }
        public void UpdateFechaCalendario(Models.FechasCalendario nuevaFecha)
        {
            Models.FechasCalendario oldFechaCalendario = db.FechasCalendarios.SingleOrDefault(b => b.idFecha == nuevaFecha.idFecha);

            if (oldFechaCalendario != null)
            {
                oldFechaCalendario.fechaProgramada = nuevaFecha.fechaProgramada;
            }
        }

        public void DeleteFechaCalendario(decimal idFecha)
        {
            Models.FechasCalendario fechasCalendario = db.FechasCalendarios.SingleOrDefault(b => b.idFecha == idFecha);
            if (fechasCalendario != null)
            {
                db.FechasCalendarios.Attach(fechasCalendario);
                db.FechasCalendarios.Remove(fechasCalendario);
            }
        }

        public void DeleteTorneo(decimal idTorneo)
        {
            Models.Torneo torneo = db.Torneos.SingleOrDefault(b => b.idTorneo == idTorneo);
            if (torneo != null)
            {
                db.Torneos.Attach(torneo);
                db.Torneos.Remove(torneo);
            }
        }

        public void GuardarCambios()
        {
            db.SaveChanges();
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
        public JsonResult GetListaTorneos()
        {
            try
            {
                // Obtenemos la lista de Fechas programadas desde la base de datos
                
                return new JsonResult()
                {
                    Data = _db.getListaTorneos(),
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

        public JsonResult AgregarFechaCalendario(FechasCalendarioVM fecha)
        {
            System.Diagnostics.Debug.WriteLine(fecha.IdTorneo + " " + fecha.FechaProgramada);
            // Se verifica si los datos ingresados son válidos a nivel de backend, o se reporta el error
            if (!ModelState.IsValid)
            {
                foreach (ModelState modelstate in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelstate.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                    }
                }
                return new JsonResult()
                {
                    Data = new { CODE = "CAMPOS_INVALIDOS" },
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }

            try
            {
                
                var nuevaFecha = _db.CreateFechaCalendario();
                nuevaFecha.idTorneo = fecha.IdTorneo;
                nuevaFecha.fechaProgramada = fecha.FechaProgramada;



                _db.AddFechasCalendario(nuevaFecha);

                //// Se guardan los cambios, indicando que se hace desde el proceso de edición
                _db.GuardarCambios();
                // Y se retorna un código de completación del proceso exitoso
                return new JsonResult()
                {
                    Data = new { CODE = "FECHA_GUARDADA" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            // En caso de ocurrir una excepción, se atrapa la excepción y se retorna un código ERROR
            catch (DbEntityValidationException e)
            {
                foreach (var entityValidationErrors in e.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
                return new JsonResult()
                {
                    Data = new { CODE = "ERROR" },
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }

        public JsonResult ActualizarFechaCalendario(FechasCalendarioVM fecha)
        {
            // Se verifica si los datos ingresados son válidos a nivel de backend, o se reporta el error
            if (!ModelState.IsValid)
            {
                foreach (ModelState modelstate in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelstate.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                    }
                }
                return new JsonResult()
                {
                    Data = new { CODE = "CAMPOS_INVALIDOS" },
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }

            try
            {

                var nuevaFecha = _db.CreateFechaCalendario();
                nuevaFecha.idFecha = fecha.IdFecha;
                nuevaFecha.idTorneo = fecha.IdTorneo;
                nuevaFecha.fechaProgramada = fecha.FechaProgramada;


                _db.UpdateFechaCalendario(nuevaFecha);

                //// Se guardan los cambios, indicando que se hace desde el proceso de edición
                _db.GuardarCambios();
                // Y se retorna un código de completación del proceso exitoso
                return new JsonResult()
                {
                    Data = new { CODE = "FECHA_ACTUALIZADA" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            // En caso de ocurrir una excepción, se atrapa la excepción y se retorna un código ERROR
            catch (DbEntityValidationException e)
            {
                foreach (var entityValidationErrors in e.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
                return new JsonResult()
                {
                    Data = new { CODE = "ERROR" },
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }

        public JsonResult DeleteFechaCalendario(decimal idFechaCalendario)
        {
            try
            {
               
                _db.DeleteFechaCalendario(idFechaCalendario);
                _db.GuardarCambios();
                // Y se retorna un código de completación del proceso exitoso
                return new JsonResult()
                {
                    Data = new { CODE = "FECHA_CALENDARIO_ELIMINADA" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            // En caso de ocurrir una excepción, se atrapa la excepción y se retorna un código ERROR
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { CODE = "ERROR" },
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }

        public JsonResult DeleteTorneo(decimal idTorneo)
        {
            try
            {

                _db.DeleteTorneo(idTorneo);
                _db.GuardarCambios();
                // Y se retorna un código de completación del proceso exitoso
                return new JsonResult()
                {
                    Data = new { CODE = "TORNEO_ELIMINADO" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            // En caso de ocurrir una excepción, se atrapa la excepción y se retorna un código ERROR
            catch (Exception e)
            {
                return new JsonResult()
                {
                    Data = new { CODE = "ERROR" },
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }
    }
}

