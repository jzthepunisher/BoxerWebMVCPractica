using BoxerWeb.Domain.DAL;
using BoxerWeb.Domain.Entidades;
using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;


namespace BoxerWeb.WebUI.Servicios
{
    public class TurnoServicio : IDisposable
    {
        private SchoolContext db = new SchoolContext();

        public List<TurnoVistaModelo> Get(OpcionesConsulta opcionesConsulta)
        {
            var inicio = CalculadorOpcionesConsulta.CalculaInicio(opcionesConsulta);

            var turnos = db.Turnos
                            .OrderBy(opcionesConsulta.Ordena);

            bool dataFiltrada = false;
            if ( ! String.IsNullOrEmpty(opcionesConsulta.CadenaFiltro))
            {
                if (opcionesConsulta.BusquedaRapida == false)
                {
                    switch (opcionesConsulta.CampoOrdenamiento)
                    {
                        case "idTurno":
                            turnos = turnos.Where(turno => turno.IdTurno.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "descripcion":
                            turnos = turnos.Where(turno => turno.Descripcion.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "horaInicio":
                            turnos = turnos.Where(turno => turno.HoraInicio.ToString().Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "horaFin":
                            turnos = turnos.Where(turno => turno.HoraFin.ToString().Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                    }
                }
                else
                {
                    turnos = turnos.Where(turno => turno.Descripcion.Contains(opcionesConsulta.CadenaFiltro) || 
                                                    turno.HoraFin.ToString().Contains(opcionesConsulta.CadenaFiltro) || 
                                                    turno.HoraFin.ToString().Contains(opcionesConsulta.CadenaFiltro));
                    dataFiltrada = true;
                }

                
            }

            turnos = turnos.Skip(inicio)
                           .Take(opcionesConsulta.TamanoPagina);
                        
            var turnosEjecutado = (List<TurnoVistaModelo>)null;

            if (opcionesConsulta.BusquedaRapida == false)
            {
                turnosEjecutado = turnos.Select(turno => new TurnoVistaModelo
                {
                    IdTurno = turno.IdTurno,
                    Descripcion = turno.Descripcion,
                    HoraInicio = turno.HoraInicio,
                    HoraFin = turno.HoraFin                   
                }).ToList();
            }
            else
            {
                turnosEjecutado = turnos.Select(turno => new TurnoVistaModelo
                {
                    IdTurno = turno.IdTurno,
                    Descripcion = turno.Descripcion,
                    HoraInicio = turno.HoraInicio,
                    HoraFin = turno.HoraFin,
                    GruposAsignados = db.TurnosGrupo.Count(tg => tg.IdTurno == turno.IdTurno)
                }).ToList();
            }

            long cantidadRegistrosTotal = 0;
            if (dataFiltrada == true)
            {
                cantidadRegistrosTotal = turnosEjecutado.Count;
            }
            else
            {
                cantidadRegistrosTotal = db.Turnos.Count();
            }

            opcionesConsulta.TotalPaginas = CalculadorOpcionesConsulta.CalculaTotalPaginas(cantidadRegistrosTotal, opcionesConsulta.TamanoPagina);
            
            return turnosEjecutado;
        }

        public Turno GetPorId(String idTurno)
        {
            Turno turno = db.Turnos.Find(idTurno);

            if (turno == null)
            {
                //throw new System.Data.Entity.Core.ObjectNotFoundException
                //    (string.Format("Unable to find author with id {0}", id));

                turno = new Turno();
            }

            return turno;
        }

        public TurnoGruposEditarCrearVistaModelo GetPorIdTurnoGrupo(String idTurno)
        {

            if (!String.IsNullOrEmpty(idTurno))
            {
                // var turno = (List<TurnoGruposEditarCrearVistaModelo>)null;

                List<TurnoGruposEditarCrearVistaModelo> turno= (List<TurnoGruposEditarCrearVistaModelo>)null;

                turno = db.Turnos
                    .Where(turn => turn.IdTurno == idTurno)
                    .Select(turn => new TurnoGruposEditarCrearVistaModelo
                    {
                        IdTurno = turn.IdTurno,
                        Descripcion = turn.Descripcion,
                        HoraInicio = turn.HoraInicio,
                        HoraFin = turn.HoraFin,
                        GruposAsignados = db.TurnosGrupo.Count(tg => tg.IdTurno == turn.IdTurno),
                        TurnosGrupoVistaModelo = (from tg in turn.TurnosGrupos
                                                orderby tg.Grupo.Descripcion
                                                select new TurnoGrupoVistaModelo
                                                {
                                                    IdTurno = tg.IdTurno,
                                                    IdGrupo = tg.IdGrupo,
                                                    DireccionUbicacionAsignada = tg.DireccionUbicacionAsignada,
                                                    Latitud=tg.Latitud,
                                                    Longitud=tg.Longitud,
                                                    GrupoVistaModelo = (new GrupoVistaModelo
                                                                        {
                                                                            IdGrupo =tg.Grupo.IdGrupo,
                                                                            Descripcion = tg.Grupo.Descripcion,
                                                                            RastreoGps = tg.Grupo.RastreoGps,
                                                                            VerEnMapa = tg.Grupo.VerEnMapa,
                                                                            DireccionUltimaUbicacion=tg.Grupo.DireccionUltimaUbicacion,
                                                                            FechaHoraUltimaUbicacion=tg.Grupo.FechaHoraUltimaUbicacion,
                                                                            EmpleadosVistaModelo = (from empl in tg.Grupo.Empleados
                                                                                                    orderby empl.ApellidoMaterno
                                                                                                    select new EmpleadoVistaModelo
                                                                                                    {
                                                                                                        Nombres = empl.Nombres,
                                                                                                        ApellidoPaterno = empl.ApellidoPaterno,
                                                                                                        ApellidoMaterno = empl.ApellidoMaterno,
                                                                                                        IdEmpleado = empl.IdEmpleado,
                                                                                                        Cargo = empl.Cargo.Descripcion,
                                                                                                        Foto = empl.Foto
                                                                                                    }).ToList(),
                                                                            UnidadesReaccionVistaModelo = (from unid in tg.Grupo.UnidadesReaccion
                                                                                                           orderby unid.Descripcion
                                                                                                           select new UnidadReaccionVistaModelo
                                                                                                           {
                                                                                                               Descripcion = unid.Descripcion,
                                                                                                               IdUnidadReaccion = unid.IdUnidadReaccion,
                                                                                                               Placa = unid.Placa,
                                                                                                               FotoIconoUnidadReaccion = unid.TipoUnidadReaccion.FotoIcono,
                                                                                                               Foto = unid.TipoUnidadReaccion.Foto
                                                                                                           }).ToList(),
                                                                            DispositivosVistaModelo = (from disp in tg.Grupo.Dispositivos
                                                                                                       orderby disp.Descripcion
                                                                                                       select new DispositivoVistaModelo
                                                                                                       {
                                                                                                           Imei = disp.Imei,
                                                                                                           Descripcion = disp.Descripcion,
                                                                                                           NumeroCelular = disp.NumeroCelular,
                                                                                                           Validado = disp.Validado
                                                                                                       }).ToList()
                                                                        })
                                                                                                            
                                                }).ToList(),                     
                      

                    }).ToList();

                if (turno.Count() == 0)
                {
                    var turnoNuevo = new TurnoGruposEditarCrearVistaModelo();
                    return turnoNuevo;
                }
                else
                {
                    return turno[0];
                }

            }

            return new TurnoGruposEditarCrearVistaModelo();          
        }

        public TurnoGruposEditarCrearVistaModelo GetTurnoGrupoParaMapa(String idTurno)
        {

            if (!String.IsNullOrEmpty(idTurno))
            {
                // var turno = (List<TurnoGruposEditarCrearVistaModelo>)null;

                List<TurnoGruposEditarCrearVistaModelo> turno = (List<TurnoGruposEditarCrearVistaModelo>)null;

                turno = db.Turnos
                    .Where(turn => turn.IdTurno == idTurno)
                    .Select(turn => new TurnoGruposEditarCrearVistaModelo
                    {
                        IdTurno = turn.IdTurno,
                        Descripcion = turn.Descripcion,
                        HoraInicio = turn.HoraInicio,
                        HoraFin = turn.HoraFin,
                        GruposAsignados = db.TurnosGrupo.Count(tg => tg.IdTurno == turn.IdTurno),
                        TurnosGrupoVistaModelo = (from tg in turn.TurnosGrupos
                                                  orderby tg.Grupo.Descripcion
                                                  where tg.Grupo.VerEnMapa == true
                                                  select new TurnoGrupoVistaModelo
                                                  {
                                                      IdTurno = tg.IdTurno,
                                                      IdGrupo = tg.IdGrupo,
                                                      DireccionUbicacionAsignada = tg.DireccionUbicacionAsignada,
                                                      Latitud = tg.Latitud,
                                                      Longitud = tg.Longitud,
                                                      GrupoVistaModelo = (new GrupoVistaModelo
                                                      {
                                                          IdGrupo = tg.Grupo.IdGrupo,
                                                          Descripcion = tg.Grupo.Descripcion,
                                                          RastreoGps = tg.Grupo.RastreoGps,
                                                          VerEnMapa = tg.Grupo.VerEnMapa,
                                                          DireccionUltimaUbicacion = tg.Grupo.DireccionUltimaUbicacion,
                                                          FechaHoraUltimaUbicacion = tg.Grupo.FechaHoraUltimaUbicacion,
                                                          EmpleadosVistaModelo = (from empl in tg.Grupo.Empleados
                                                                                  orderby empl.ApellidoMaterno
                                                                                  select new EmpleadoVistaModelo
                                                                                  {
                                                                                      Nombres = empl.Nombres,
                                                                                      ApellidoPaterno = empl.ApellidoPaterno,
                                                                                      ApellidoMaterno = empl.ApellidoMaterno,
                                                                                      IdEmpleado = empl.IdEmpleado,
                                                                                      Cargo = empl.Cargo.Descripcion,
                                                                                      Foto = empl.Foto
                                                                                  }).ToList(),
                                                          UnidadesReaccionVistaModelo = (from unid in tg.Grupo.UnidadesReaccion
                                                                                         orderby unid.Descripcion
                                                                                         select new UnidadReaccionVistaModelo
                                                                                         {
                                                                                             Descripcion = unid.Descripcion,
                                                                                             IdUnidadReaccion = unid.IdUnidadReaccion,
                                                                                             Placa = unid.Placa,
                                                                                             FotoIconoUnidadReaccion = unid.TipoUnidadReaccion.FotoIcono,
                                                                                             Foto = unid.TipoUnidadReaccion.Foto
                                                                                         }).ToList(),
                                                          DispositivosVistaModelo = (from disp in tg.Grupo.Dispositivos
                                                                                     orderby disp.Descripcion
                                                                                     select new DispositivoVistaModelo
                                                                                     {
                                                                                         Imei = disp.Imei,
                                                                                         Descripcion = disp.Descripcion,
                                                                                         NumeroCelular = disp.NumeroCelular,
                                                                                         Validado = disp.Validado
                                                                                     }).ToList()
                                                      })

                                                  }).ToList(),


                    }).ToList();

                if (turno.Count() == 0)
                {
                    var turnoNuevo = new TurnoGruposEditarCrearVistaModelo();
                    return turnoNuevo;
                }
                else
                {
                    return turno[0];
                }

            }

            return new TurnoGruposEditarCrearVistaModelo();
        }

        public void Insert(Turno turno)
        {
            db.Turnos.Add(turno);

            db.SaveChanges();
        }

        public void Update(Turno turno)
        {
            db.Entry(turno).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void Delete(Turno turno)
        {
            db.Turnos.Remove(turno);

            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
