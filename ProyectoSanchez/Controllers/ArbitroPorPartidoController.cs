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
    public class ArbitroPorPartidoDataBaseWrapper
    {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public ArbitroPorPartidoDataBaseWrapper()
        {
            db = new Models.ProyectoBasesSanchezEntities();
        }

        public List<ArbitroPorPartidoVM> ObtenerDesempennoArbitro(int idTorneo)
        {
            return (from Arbitro in db.Arbitroes
                    join ArbitroPartido in db.ArbitroPorPartidoes on Arbitro.idArbitro equals ArbitroPartido.idArbitro
                    join TipoArbitro in db.TipoArbitroes on ArbitroPartido.idTipoArbitro equals TipoArbitro.idTipoArbitro
                    join Partido in db.Partidoes on ArbitroPartido.idPartido equals Partido.idPartido
                    join FechasC in db.FechasCalendarios on Partido.idFecha equals FechasC.idFecha
                    join Torneo in db.Torneos on FechasC.idTorneo equals Torneo.idTorneo
                    where Torneo.idTorneo == idTorneo
                    select new ArbitroPorPartidoVM
                    {
                        IdArbitro = Arbitro.idArbitro,
                        Nombre = Arbitro.nombre,
                        IdPartido = Partido.idPartido,
                        DescripcionTA = TipoArbitro.descripcion,
                        Desempenno = ArbitroPartido.desempenno,
                        IdTipoArbitro = TipoArbitro.idTipoArbitro
                    }
                ).ToList();
        }
        

    }
    public class ArbitroPorPartidoController : Controller
    {
        private ArbitroPorPartidoDataBaseWrapper _db;
        private static int idTorneo;
        public ArbitroPorPartidoController()
        {
            _db = new ArbitroPorPartidoDataBaseWrapper();

        }

        // GET: Partido
        public ActionResult Index()
        {
            idTorneo = GetIdTorneo();
            return View();
        }

        public JsonResult GetInformacionArbitros()
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                //List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = _db.ObtenerDesempennoArbitro(idTorneo),
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