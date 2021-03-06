﻿using System;
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

    public class JugadorPorEquipoPorTorneoControllerDataBaseWrapper
    {
        private Models.ProyectoBasesSanchezEntities db = new Models.ProyectoBasesSanchezEntities();

        public JugadorPorEquipoPorTorneoControllerDataBaseWrapper()
        {
            db = new Models.ProyectoBasesSanchezEntities();
        }

        public List<JugadorPorEquipoPorTorneoVM> GetInformacionJugador(int idJugador)
        {
            return (from Jugador in db.Jugadors
                    join JugadorE in db.JugadorPorEquipoPorTorneos on Jugador.idJugador equals JugadorE.idJugador
                    join Equipo in db.Equipoes on JugadorE.idEquipo equals Equipo.idEquipo
                    join Torneo in db.Torneos on JugadorE.idTorneo equals Torneo.idTorneo
                    join Competicion in db.Competicions on Torneo.idCompeticion equals Competicion.idCompeticion
                    where Jugador.idJugador == idJugador
                    select new JugadorPorEquipoPorTorneoVM
                    {
                        IdEquipo = Equipo.idEquipo,
                        Posicion = JugadorE.posicion,
                        IdJugador = Jugador.idJugador,
                        IdTorneo = Torneo.idTorneo,
                        Evaluacion = JugadorE.evaluacion,
                        TipoTorneo = Competicion.tipo,
                        Anno = Torneo.anno,
                        Periodo = Torneo.periodo,
                        NombreEquipo = Equipo.nombre,
                        NombreTorneo = Torneo.nombre
                    }

                   ).Union(from Jugador in db.Jugadors
                           join JugadorEH in db.JugadorPorEquipoPorTorneo_H on Jugador.idJugador equals JugadorEH.idJugador
                           join Equipo in db.Equipoes on JugadorEH.idEquipo equals Equipo.idEquipo
                           join TorneoH in db.Torneo_H on JugadorEH.idTorneo equals TorneoH.idTorneo
                           join Competicion in db.Competicions on TorneoH.idCompeticion equals Competicion.idCompeticion
                           where Jugador.idJugador == idJugador
                           select new JugadorPorEquipoPorTorneoVM
                           {
                               IdEquipo = Equipo.idEquipo,
                               Posicion = JugadorEH.posicion,
                               IdJugador = Jugador.idJugador,
                               IdTorneo = TorneoH.idTorneo,
                               Evaluacion = JugadorEH.sinopsis,
                               TipoTorneo = Competicion.tipo,
                               Anno = TorneoH.anno,
                               Periodo = TorneoH.periodo,
                               NombreEquipo = Equipo.nombre,
                               NombreTorneo = TorneoH.nombre
                           }
                  ).ToList();
        }
    }

    public class JugadorPorEquipoPorTorneoController : Controller
    {
        private JugadorPorEquipoPorTorneoControllerDataBaseWrapper _db;
        private static int IdJugador;
        public JugadorPorEquipoPorTorneoController()
        {
            _db = new JugadorPorEquipoPorTorneoControllerDataBaseWrapper();

        }


        // GET: Jugador
        public ActionResult Index()
        {
            IdJugador = GetIdJugador();
            return View();
        }

        public JsonResult GetInformacionJugador()
        {
            try
            {

                // Obtenemos la lista de Fechas programadas desde la base de datos
                //List<FechasCalendarioVM> fechas = _db.GetFechasCalendario(idTorneo);
                return new JsonResult()
                {
                    Data = _db.GetInformacionJugador(IdJugador),
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

        public int GetIdJugador()
        {
            string idJugador = Request.QueryString["idJugador"];
            if (idJugador == null)
            {
                return 1;
            }
            return Convert.ToInt32(idJugador);
        }
    }
}