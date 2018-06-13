using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProyectoSanchez.ViewModels;

namespace ProyectoSanchez.Controllers
{
    public class PartidoEquiposDataBaseWrapper
    {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public PartidoEquiposDataBaseWrapper()
        {
            db = new Models.ProyectoBasesSanchezEntities();
        }

        public List<PartidoVM> ConsultaEncuentros(int EqA, int EqB, DateTime fecha)
        {
            return (from Partido in db.Partidoes
                    join Torneo in db.Torneos on Partido.idTorneo equals Torneo.idTorneo
                    join FechasC in db.FechasCalendarios on Torneo.idTorneo equals FechasC.idTorneo
                    join EquipoA in db.Equipoes on Partido.idEquipoLocal equals EquipoA.idEquipo
                    join EquipoB in db.Equipoes on Partido.idEquipoVisita equals EquipoB.idEquipo
                    where Partido.idEquipoLocal == EqA && Partido.idEquipoVisita == EqB && Partido.idFecha ==
                    FechasC.idFecha && FechasC.fechaProgramada > fecha
                    select new PartidoVM
                    {
                        IdPartido = Partido.idPartido,
                        IdTorneo = Partido.idTorneo,
                        NombreTorneo = Torneo.nombre,
                        FechaJuego = FechasC.fechaProgramada,
                        NombreLocal = EquipoA.nombre,
                        NombreVisita = EquipoB.nombre,
                        GolLocal = Partido.golLocal,
                        GolVisita = Partido.golVisita
                    }
                ).Union(
                    from Partido in db.Partidoes
                    join Torneo in db.Torneos on Partido.idTorneo equals Torneo.idTorneo
                    join FechasC in db.FechasCalendarios on Torneo.idTorneo equals FechasC.idTorneo
                    join EquipoA in db.Equipoes on Partido.idEquipoLocal equals EquipoA.idEquipo
                    join EquipoB in db.Equipoes on Partido.idEquipoVisita equals EquipoB.idEquipo
                    where Partido.idEquipoVisita == EqA && Partido.idEquipoLocal == EqB && Partido.idFecha ==
                    FechasC.idFecha && FechasC.fechaProgramada > fecha
                    select new PartidoVM
                    {
                        IdPartido = Partido.idPartido,
                        IdTorneo = Partido.idTorneo,
                        NombreTorneo = Torneo.nombre,
                        FechaJuego = FechasC.fechaProgramada,
                        NombreLocal = EquipoA.nombre,
                        NombreVisita = EquipoB.nombre,
                        GolLocal = Partido.golLocal,
                        GolVisita = Partido.golVisita
                    }
                ).ToList();
        }
    }
    public class PartidoEquiposController : Controller
    {
        private PartidoEquiposDataBaseWrapper _db;
        private static int idEquipoA;
        private static int idEquipoB;
        private static DateTime Fecha;
        
        public PartidoEquiposController()
        {
            _db = new PartidoEquiposDataBaseWrapper();
        }

        public ActionResult Index()
        {
            idEquipoA = GetIdEquipoA();
            idEquipoB = GetIdEquipoB();
            Fecha = GetFecha();
            return View();
        }

        public JsonResult GetInformacionEncuentros()
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                //List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = _db.ConsultaEncuentros(idEquipoA, idEquipoB,Fecha),
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

        public int GetIdEquipoA()
        {
            string idEquipoA = Request.QueryString["IdEquipoA"];
            if (idEquipoA == null)
            {
                return 1;
            }
            return Convert.ToInt32(idEquipoA);
        }

        public int GetIdEquipoB()
        {
            string idEquipoB = Request.QueryString["IdEquipoB"];
            if (idEquipoB == null)
            {
                return 1;
            }
            return Convert.ToInt32(idEquipoB);
        }
        
        public DateTime GetFecha()
        {
            string fecha = Request.QueryString["Fecha"];
            if(fecha == null)
            {
                return Convert.ToDateTime(null);
            }
            else
            {
                try
                {
                    return Convert.ToDateTime(fecha);
                }
                catch
                {
                    return Convert.ToDateTime("2017/06/13");
                }
            }
        }
    }
}