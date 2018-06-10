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

    public class EntrenadorPorEquipoPorTorneoControllerDataBaseWrapper
    {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public EntrenadorPorEquipoPorTorneoControllerDataBaseWrapper()
        {
            db = new Models.ProyectoBasesSanchezEntities();
        }

        public List<EntrenadorEquipoTorneoVM> GetInformacionEntrenador(Decimal idEntrenador)
        {
            return (from Entrenador in db.Entrenadors
                    join EntrenadorE in db.EntrenadorEquipoTorneos on Entrenador.idEntrenador equals EntrenadorE.idEntrenador
                    join Equipo in db.Equipoes on EntrenadorE.idEquipo equals Equipo.idEquipo
                    join Torneo in db.Torneos on EntrenadorE.idTorneo equals Torneo.idTorneo
                    join Competicion in db.Competicions on Torneo.idCompeticion equals Competicion.idCompeticion
                    join Funcionario in db.Funcionarios on EntrenadorE.idEntrenador equals Funcionario.codigoFuncionario
                    where Entrenador.idEntrenador == idEntrenador
                    select new EntrenadorEquipoTorneoVM
                    {
                        IdEntrenador = Entrenador.idEntrenador,
                        IdEquipo = Equipo.idEquipo,
                        IdTorneo = Torneo.idTorneo,
                        Tipo = Competicion.tipo,
                        Posicion = EntrenadorE.posicion,
                        Sinopsis = EntrenadorE.sinopsis,
                        NombreTorneo = Torneo.nombre,
                        Anno = Torneo.anno,
                        NombreEquipo = Equipo.nombre
                    }
                   ).Union(from Entrenador in db.Entrenadors
                           join EntrenadorEH in db.EntrenadorEquipoTorneo_H on Entrenador.idEntrenador equals EntrenadorEH.idEntrenador
                           join Equipo in db.Equipoes on EntrenadorEH.idEquipo equals Equipo.idEquipo
                           join TorneoH in db.Torneo_H on EntrenadorEH.idTorneo equals TorneoH.idTorneo
                           join Competicion in db.Competicions on TorneoH.idCompeticion equals Competicion.idCompeticion
                           join Funcionario in db.Funcionarios on EntrenadorEH.idEntrenador equals Funcionario.codigoFuncionario
                           where Entrenador.idEntrenador == idEntrenador
                           select new EntrenadorEquipoTorneoVM
                           {
                               IdEntrenador = Entrenador.idEntrenador,
                               IdEquipo = Equipo.idEquipo,
                               IdTorneo = TorneoH.idTorneo,
                               Tipo = Competicion.tipo,
                               Posicion = EntrenadorEH.posicion,
                               Sinopsis = EntrenadorEH.sinopsis,
                               NombreTorneo = TorneoH.nombre,
                               Anno = TorneoH.anno,
                               NombreEquipo = Equipo.nombre
                           }

                    ).ToList();
        }
    }

    public class EntrenadorPorEquipoPorTorneoController : Controller
    {
        private EntrenadorPorEquipoPorTorneoControllerDataBaseWrapper _db;
        private static int idEntrenador;
        public EntrenadorPorEquipoPorTorneoController()
        {
            _db = new EntrenadorPorEquipoPorTorneoControllerDataBaseWrapper();

        }


        // GET: Entrenador
        public ActionResult Index()
        {
            idEntrenador = GetIdEntrenador();
            return View();
        }

        public JsonResult GetInformacionEntrenador()
        {
            try
            {
                return new JsonResult()
                {
                    Data = _db.GetInformacionEntrenador(idEntrenador),
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

        public int GetIdEntrenador()
        {
            string idEntrenador = Request.QueryString["idEntrenador"];
            if (idEntrenador == null)
            {
                return 1;
            }
            return Convert.ToInt32(idEntrenador);
        }
    }
}