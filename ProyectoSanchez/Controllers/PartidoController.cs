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

        public List<JugadorVM> GetJugadores(decimal idEquipo)
        {
            return (from Jugador in db.Jugadors
                    join Funcionario in db.Funcionarios on Jugador.codigoFuncionario equals Funcionario.codigoFuncionario
                    join JugadorXEquipo in db.JugadorPorEquipoPorTorneos on Jugador.idJugador equals JugadorXEquipo.idJugador
                    where idEquipo == JugadorXEquipo.idEquipo
                    select new JugadorVM
                    {
                        IdJugador = Jugador.idJugador,
                        CodigoFuncionario = Funcionario.codigoFuncionario,
                        PesoKilos = Jugador.pesoKilos,
                        AlturaMetros = Jugador.alturaMetros,
                        Nombre = Funcionario.nombre,
                        FechaNacimiento = Funcionario.fechaNacimiento,
                        Imagen = Funcionario.urlImagen
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
        public void UpdatePartido(Models.Partido nuevoPartido)
        {
            Models.Partido oldPartido = db.Partidoes.SingleOrDefault(b => b.idPartido == nuevoPartido.idPartido);

            if (oldPartido != null)
            {
               oldPartido.idEquipoLocal = nuevoPartido.idEquipoLocal;
                oldPartido.idEquipoVisita = nuevoPartido.idEquipoVisita;
                oldPartido.golLocal = nuevoPartido.golLocal;
                oldPartido.golVisita = nuevoPartido.golVisita;
            }
        }

        public void UpdateGol(Models.Gol nuevoGol)
        {
            Models.Gol oldGol = db.Gols.SingleOrDefault(b => b.idGol == nuevoGol.idGol);

            if (oldGol != null)
            {
                oldGol.idJugador = nuevoGol.idJugador;
                oldGol.minuto = nuevoGol.minuto;
                oldGol.urlVideo = nuevoGol.urlVideo;
            }
        }

        public void AddPartido(Models.Partido partido)
        {
            db.Partidoes.Add(partido);
        }
        public Models.Partido CreatePartido()
        {
            return db.Partidoes.Create();
        }

        public void AddGol(Models.Gol gol)
        {
            db.Gols.Add(gol);
        }
        public Models.Gol CreateGol()
        {
            return db.Gols.Create();
        }

        public void DeletePartido(decimal idPartido)
        {
            Models.Partido partido = db.Partidoes.SingleOrDefault(b => b.idPartido == idPartido);
            if (partido != null)
            {
                System.Diagnostics.Debug.WriteLine(partido.idEquipoLocal + " "+ partido.idEquipoVisita + " " + partido.idPartido);
                db.Partidoes.Attach(partido);
                db.Partidoes.Remove(partido);
            }
        }

        //Retorna True si esta registrado
        public Boolean VerificarGolRegistrado(decimal idPartido, decimal idEquipo) {
            Models.Gol gol = db.Gols.FirstOrDefault(b => b.idPartido == idPartido && b.idEquipo == idEquipo);
            if (gol != null) {
                return true;
            }
            return false;
        }
        public List<GolVM> GolesRegistrados(decimal idPartido, decimal idEquipo) {
            return (from Goles in db.Gols where idPartido == Goles.idPartido
                    && idEquipo == Goles.idEquipo
                    select new GolVM
                    {
                        IdGol = Goles.idGol,
                        IdEquipo = Goles.idEquipo,
                        IdJugador = Goles.idJugador,
                        Minuto = Goles.minuto,
                        IdPartido = Goles.idPartido,
                        Url = Goles.urlVideo
                    }
                    ).ToList();
        }

        public void GuardarCambios()
        {
            db.SaveChanges();
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

        public JsonResult GetEquiposXTorneo()
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                //List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = _db.getListaEquiposXTorneo(idTorneo),
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

        public JsonResult GetJugadores(decimal idEquipo)
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                //List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = _db.GetJugadores(idEquipo),
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
        public JsonResult GetGolesRegistrados(decimal idPartido, decimal idEquipo)
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                //List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = _db.GolesRegistrados(idPartido, idEquipo),
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



        public JsonResult VerificarGolRegistrado(decimal idPartido, decimal idEquipo)
        {
            try
            {
               
                if (_db.VerificarGolRegistrado(idPartido, idEquipo)) {
                    return new JsonResult()
                    {
                        Data = _db.GolesRegistrados(idPartido,idEquipo),
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                return new JsonResult()
                {
                    Data = false,
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
                var nuevoPartido = _db.CreatePartido();
                nuevoPartido.idPartido = partido.IdPartido;
                nuevoPartido.idEquipoLocal = partido.IdEquipoLocal;
                nuevoPartido.idEquipoVisita = partido.IdEquipoVisita;
                nuevoPartido.golLocal = partido.GolLocal;
                nuevoPartido.golVisita = partido.GolVisita;


                _db.UpdatePartido(nuevoPartido);

                //// Se guardan los cambios, indicando que se hace desde el proceso de edición
                //GuardarCambios();
                _db.GuardarCambios();
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
        public JsonResult ActualizarGoles(GolesXPartidoVM golesXPartido)
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
                foreach (GolVM gol in golesXPartido.Goles)
                {
                    var nuevoGol = _db.CreateGol();
                    nuevoGol.idGol = gol.IdGol;
                    nuevoGol.idJugador = gol.IdJugador;
                    nuevoGol.minuto = gol.Minuto;
                    nuevoGol.idEquipo = gol.IdEquipo;
                    nuevoGol.idPartido = gol.IdPartido;
                    nuevoGol.urlVideo = gol.Url;
                    _db.UpdateGol(nuevoGol);
                }


                //// Se guardan los cambios, indicando que se hace desde el proceso de edición

                _db.GuardarCambios();
                // Y se retorna un código de completación del proceso exitoso
                return new JsonResult()
                {
                    Data = new { CODE = "GOLES_ACTUALIZADOS" },
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

        public JsonResult AgregarPartido(PartidoVM partido)
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
                var nuevoPartido = _db.CreatePartido();
                nuevoPartido.idEquipoLocal = partido.IdEquipoLocal;
                nuevoPartido.idEquipoVisita = partido.IdEquipoVisita;
                nuevoPartido.golLocal = partido.GolLocal;
                nuevoPartido.golVisita = partido.GolVisita;
                nuevoPartido.jugado = partido.Jugado;
                nuevoPartido.idTorneo = idTorneo;
                nuevoPartido.idFecha = idFecha;


                _db.AddPartido(nuevoPartido);

                //// Se guardan los cambios, indicando que se hace desde el proceso de edición
                //GuardarCambios();
                _db.GuardarCambios();
                // Y se retorna un código de completación del proceso exitoso
                return new JsonResult()
                {
                    Data = new { CODE = "PARTIDO_GUARDADO" },
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

        public JsonResult AgregarGoles(GolesXPartidoVM golesXPartido)
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
                foreach (GolVM gol in golesXPartido.Goles) {
                    var nuevoGol = _db.CreateGol();
                    nuevoGol.idJugador = gol.IdJugador;
                    nuevoGol.minuto = gol.Minuto;
                    nuevoGol.idEquipo = gol.IdEquipo;
                    nuevoGol.idPartido = gol.IdPartido;
                    nuevoGol.urlVideo = gol.Url;
                    _db.AddGol(nuevoGol);
                }
               

                //// Se guardan los cambios, indicando que se hace desde el proceso de edición

                _db.GuardarCambios();
                // Y se retorna un código de completación del proceso exitoso
                return new JsonResult()
                {
                    Data = new { CODE = "GOLES_GUARDADO" },
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

        public JsonResult DeletePartido(decimal idPartido)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(idPartido);
                _db.DeletePartido(idPartido);

                _db.GuardarCambios();
                // Y se retorna un código de completación del proceso exitoso
                return new JsonResult()
                {
                    Data = new { CODE = "PARTIDO_ELIMINADO" },
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