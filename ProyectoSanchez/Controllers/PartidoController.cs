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
                    join EquipoA in db.Equipoes on Partido.idEquipoLocal equals EquipoA.idEquipo
                    join EquipoB in db.Equipoes on Partido.idEquipoVisita equals EquipoB.idEquipo
                    where Partido.idFecha == idFecha && Partido.idTorneo == idTorneo
                    select new PartidoVM
                    {
                        IdPartido = Partido.idPartido,
                        IdEquipoLocal = Partido.idEquipoLocal,
                        IdEquipoVisita = Partido.idEquipoVisita,
                        NombreLocal = EquipoA.nombre,
                        NombreVisita = EquipoB.nombre,
                        GolLocal =  Partido.golLocal,
                        GolVisita = Partido.golVisita,
                        IdTorneo = Partido.idTorneo

                    }
                    ).ToList();
        }

        public List<EquipoVM> getListaEquiposXTorneo(int IdTorneo) {
            return ( from Equipo in db.Equipoes
                     join EquipoXTorneo in db.EquipoPorTorneos on Equipo.idEquipo equals EquipoXTorneo.idEquipo
                     join Torneo in db.Torneos on EquipoXTorneo.idTorneo equals Torneo.idTorneo
                     where Torneo.idTorneo == IdTorneo
                     select new EquipoVM
                     {  
                         IdEquipo = Equipo.idEquipo,
                         Nombre = Equipo.nombre
                     }
                ).ToList();
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
            idFecha = GetIdFecha();
            idTorneo = GetIdTorneo();
            return View();
        }

        public JsonResult GetPartidosPorFechaYTorneo()
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                //List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = _db.GetPartidosXFechaYTorneo(idFecha, idTorneo),
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

        public JsonResult GetEquiposXTorneo(int IdTorneo)
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                //List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = _db.getListaEquiposXTorneo(1),
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

        public JsonResult ActualizarPartido(PartidoVM partido)
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


           

                // Se crea un nuevo partido y se llena con los datos.
                //var nuevoPartido = CreateFormulario();
                //nuevoFormulario.ID = FormID;
                //nuevoFormulario.ID_Usuario_Final = GetUsuarioID(usuarioActual);
                //nuevoFormulario.Fecha_Inicial = formulario.FechaInicial;
                //nuevoFormulario.Fecha_Final = formulario.FechaFinal;
                //nuevoFormulario.ID_Turno_Inicial = formulario.ID_Turno_Inicial;
                //nuevoFormulario.ID_Turno_Final = formulario.ID_Turno_Final;
                //nuevoFormulario.ID_Producto_De = formulario.ID_Producto_De;
                //nuevoFormulario.ID_Producto_A = formulario.ID_Producto_A;
                //nuevoFormulario.ID_Tipo_Limpieza = formulario.ID_Tipo_Limpieza;
                //nuevoFormulario.ID_Empleado_Responsable_Lavado = formulario.ResponsableLavado;
                //nuevoFormulario.ID_Maquina = formulario.IdMaquina;
                //nuevoFormulario.ID_Verificacion_Agua = formulario.TipoVerificacionAgua;
                //nuevoFormulario.Maquina_Liberada = formulario.MaquinaLiberada;
                //nuevoFormulario.Hora_Liberacion_Maquina = formulario.HoraDeLiberacion;
                //nuevoFormulario.ID_Empleado_Verificacion_Desarme = formulario.ResponsableVerificacion;
                //nuevoFormulario.Verificacion_Zona_Trabajo = formulario.VerificacionZonaTrabajo;
                //nuevoFormulario.Observacion = formulario.Observacion;
                //nuevoFormulario.ID_Estado = GetEstadoEnProceso();

                //// Se actualiza el formulario utilizando el nuevo formulario
                //UpdateForm(nuevoFormulario, formulario.Piezas, formulario.PreguntasLimpieza,
                //    formulario.PiezasExtra, formulario.PiezasExtraBorradas, formulario.PiezasExtraCargadas,
                //    formulario.Desinfecciones, formulario.DesinfeccionesCargadas, formulario.DesinfeccionesBorradas,
                //    formulario.Quimicos, formulario.QuimicosCargados, formulario.QuimicosBorrados);

                //// Se guardan los cambios, indicando que se hace desde el proceso de edición
                //GuardarCambios();

                // Y se retorna un código de completación del proceso exitoso
                return new JsonResult()
                {
                    Data = new { CODE = "PARTIDO_GUARDADO" },
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

        public int GetIdFecha()
        {
            string idFecha = Request.QueryString["IdFecha"];
            if (idFecha == null)
            {
                return 0;
            }
            return Convert.ToInt32(idFecha);
        }

        public int GetIdTorneo()
        {
            string idTorneo = Request.QueryString["IdTorneo"];
            if (idTorneo == null)
            {
                return 1;
            }
            return Convert.ToInt32(idTorneo);
        }

    }
}