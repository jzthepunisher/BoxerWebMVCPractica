using BoxerWeb.Domain.DAL;
using BoxerWeb.Domain.Entidades;
using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;

namespace BoxerWeb.WebUI.Servicios
{
    public class GrupoServicio : IDisposable
    {
        private SchoolContext db = new SchoolContext();

        public List<GrupoVistaModelo> Get(OpcionesConsulta opcionesConsulta)
        {
            var inicio = CalculadorOpcionesConsulta.CalculaInicio(opcionesConsulta);

            var query = db.Grupos.OrderBy("descripcion");

            bool dataFiltrada = false;
            if ( ! String.IsNullOrEmpty(opcionesConsulta.CadenaFiltro))
            {
                if (opcionesConsulta.BusquedaRapida == false)
                {

                }
                else
                {
                    switch (opcionesConsulta.EntidadOrdenamiento)
                    {
                        case "empleado":

                            query = query.Where(grupo => grupo.Empleados.Any(empl => empl.Nombres.Contains(opcionesConsulta.CadenaFiltro) ||
                                                                           empl.ApellidoPaterno.Contains(opcionesConsulta.CadenaFiltro) ||
                                                                           empl.ApellidoMaterno.Contains(opcionesConsulta.CadenaFiltro) ||
                                                                           empl.IdEmpleado.Contains(opcionesConsulta.CadenaFiltro) ||
                                                                           empl.Cargo.Descripcion.Contains(opcionesConsulta.CadenaFiltro))
                                                        && grupo.Empleados.Any(empl => empl.IdGrupo != "" && empl.IdGrupo != null));

                            
                            break;
                        case "unidadReaccion":

                            query = query.Where(grupo => grupo.UnidadesReaccion.Any(unid => unid.Descripcion.Contains(opcionesConsulta.CadenaFiltro) ||
                                                                           unid.IdUnidadReaccion.Contains(opcionesConsulta.CadenaFiltro) ||
                                                                           unid.Placa.Contains(opcionesConsulta.CadenaFiltro) 
                                                                           )
                                                        && grupo.UnidadesReaccion.Any(unid => unid.IdGrupo != "" && unid.IdGrupo != null));


                            break;
                        case "dispositivo":

                            query = query.Where(grupo => grupo.Dispositivos.Any(disp => disp.Descripcion.Contains(opcionesConsulta.CadenaFiltro) ||
                                                                           disp.NumeroCelular.Contains(opcionesConsulta.CadenaFiltro) ||
                                                                           disp.Validado.ToString().Contains(opcionesConsulta.CadenaFiltro)
                                                                           )
                                                        && grupo.Dispositivos.Any(unid => unid.IdGrupo != "" && unid.IdGrupo != null));


                            break;
                    }//cierra switch
                }

            }

            query = query.Skip(inicio)
                .Take(opcionesConsulta.TamanoPagina);

            var gruposEjecutado = query.Select(grupo => new GrupoVistaModelo
            {
                IdGrupo = grupo.IdGrupo,
                Descripcion = grupo.Descripcion,
                RastreoGps = grupo.RastreoGps,
                VerEnMapa = grupo.VerEnMapa,
                FechaHoraUltimaUbicacion = grupo.FechaHoraUltimaUbicacion,
                DireccionUltimaUbicacion = grupo.DireccionUltimaUbicacion,
                EmpleadosVistaModelo = (from empl in grupo.Empleados
                                        orderby empl.ApellidoMaterno
                                        select new EmpleadoVistaModelo
                                        {
                                            Nombres = empl.Nombres,
                                            ApellidoPaterno = empl.ApellidoPaterno,
                                            ApellidoMaterno = empl.ApellidoMaterno,
                                            IdEmpleado = empl.IdEmpleado,
                                            Cargo = empl.Cargo.Descripcion
                                        }).ToList(),
                UnidadesReaccionVistaModelo = (from unid in grupo.UnidadesReaccion
                                               orderby unid.Descripcion
                                               select new UnidadReaccionVistaModelo
                                               {
                                                   Descripcion = unid.Descripcion,
                                                   IdUnidadReaccion = unid.IdUnidadReaccion,
                                                   Placa = unid.Placa,
                                                   FotoIconoUnidadReaccion=unid.TipoUnidadReaccion.FotoIcono
                                               }).ToList(),
                DispositivosVistaModelo = (from disp in grupo.Dispositivos
                                           orderby disp.Descripcion
                                           select new DispositivoVistaModelo
                                           {
                                               Descripcion = disp.Descripcion,
                                               NumeroCelular = disp.NumeroCelular,
                                               Validado = disp.Validado
                                           }).ToList()

            }).ToList();


            long cantidadRegistrosTotal = 0;
            if (dataFiltrada == true)
            {
                cantidadRegistrosTotal = gruposEjecutado.Count();
            }
            else
            {
                cantidadRegistrosTotal = db.Grupos.Count();
            }

            opcionesConsulta.TotalPaginas = CalculadorOpcionesConsulta.CalculaTotalPaginas(cantidadRegistrosTotal, opcionesConsulta.TamanoPagina);

            dataFiltrada = true;

            return gruposEjecutado;
        }

        public GrupoEditarCrearVistaModelo GetPorId(String idGrupo)
        {              
            if (!String.IsNullOrEmpty(idGrupo))
            {
                var grupo = db.Grupos
                    .Where(grup => grup.IdGrupo == idGrupo)
                    .Select(grup => new GrupoEditarCrearVistaModelo
                    {
                        IdGrupo = grup.IdGrupo,                    
                        Descripcion = grup.Descripcion,
                        RastreoGps = grup.RastreoGps,
                        VerEnMapa = grup.VerEnMapa,
                        FechaHoraUltimaUbicacion = grup.FechaHoraUltimaUbicacion,
                        DireccionUltimaUbicacion = grup.DireccionUltimaUbicacion,
                        EmpleadosVistaModelo = (from empl in grup.Empleados
                                                orderby empl.ApellidoMaterno
                                                select new EmpleadoVistaModelo
                                                {
                                                    Nombres = empl.Nombres,
                                                    ApellidoPaterno = empl.ApellidoPaterno,
                                                    ApellidoMaterno = empl.ApellidoMaterno,
                                                    IdEmpleado = empl.IdEmpleado,
                                                    Cargo = empl.Cargo.Descripcion,
                                                    Foto=empl.Foto
                                                }).ToList(),
                        UnidadesReaccionVistaModelo = (from unid in grup.UnidadesReaccion
                                                       orderby unid.Descripcion
                                                       select new UnidadReaccionVistaModelo
                                                       {
                                                           Descripcion = unid.Descripcion,
                                                           IdUnidadReaccion = unid.IdUnidadReaccion,
                                                           Placa = unid.Placa,
                                                           FotoIconoUnidadReaccion = unid.TipoUnidadReaccion.FotoIcono,
                                                           Foto = unid.TipoUnidadReaccion.Foto
                                                       }).ToList(),
                        DispositivosVistaModelo = (from disp in grup.Dispositivos
                                                   orderby disp.Descripcion
                                                   select new DispositivoVistaModelo
                                                   {
                                                       Imei=disp.Imei,
                                                       Descripcion = disp.Descripcion,
                                                       NumeroCelular = disp.NumeroCelular,
                                                       Validado = disp.Validado
                                                   }).ToList()

                    }).ToList();

                if (grupo.Count() == 0)
                {                 
                    var grupoNuevo = new GrupoEditarCrearVistaModelo();
                    return grupoNuevo;
                }
                else
                {                 
                    return grupo[0];                
                }

            }

            return new GrupoEditarCrearVistaModelo();
        }

        public void Insert(Grupo grupo)
        {           
            var grupoFind = db.Grupos
                            .Where(grup => grup.IdGrupo == grupo.IdGrupo)
                            .Select(grup => grup.IdGrupo)
                            .ToList();

            bool esNuevoElemento = false;

            if (grupoFind.Count() == 0)
            {
                esNuevoElemento = true;
            }
            else
            {
                esNuevoElemento = false;
            }
          
            //Limpia empleados
            List<Empleado> empleadosGrupo = db.Empleados
                            .Where(empl => empl.IdGrupo == grupo.IdGrupo).ToList();                                                       

            foreach (Empleado empleado in empleadosGrupo)
            {
                empleado.IdGrupo = null;
                empleado.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
                empleado.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }

            //Limpia unidades de reacción
            List<UnidadReaccion> unidadesReaccion = db.UnidadesReaccion
                                .Where(unidadReac => unidadReac.IdGrupo == grupo.IdGrupo).ToList();
            foreach (UnidadReaccion unidadReaccion in unidadesReaccion)
            {
                unidadReaccion.IdGrupo = null;
                unidadReaccion.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
                unidadReaccion.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }

            //Limpia dispostivos
            List<Dispositivo> dispositivos = db.Dispositivos
                                .Where(disp => disp.IdGrupo == grupo.IdGrupo).ToList();
            foreach (Dispositivo dispositivo in dispositivos)
            {
                dispositivo.IdGrupo = null;
                dispositivo.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
                dispositivo.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }

            //Actualiza empleado con el grupo
            foreach (var empleado in grupo.Empleados)
            {
                Empleado empleadoFind = db.Empleados.Find(empleado.IdEmpleado);
                empleadoFind.IdGrupo = grupo.IdGrupo;
                empleadoFind.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
                empleadoFind.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }

            //Actualiza unidad reacción con el grupo
            foreach (var unidadReaccion in grupo.UnidadesReaccion)
            {
                UnidadReaccion unidadReaccionFind = db.UnidadesReaccion.Find(unidadReaccion.IdUnidadReaccion);
                unidadReaccionFind.IdGrupo = grupo.IdGrupo;
                unidadReaccionFind.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
                unidadReaccionFind.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }

            //Actualiza dispositivo con el grupo
            foreach (var dispositivo in grupo.Dispositivos)
            {
                Dispositivo dispositivoFind = db.Dispositivos.Find(dispositivo.Imei);
                dispositivoFind.IdGrupo = grupo.IdGrupo;
                dispositivoFind.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
                dispositivoFind.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }

            grupo.Empleados = null;
            grupo.UnidadesReaccion = null;
            grupo.Dispositivos = null;
            grupo.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (esNuevoElemento == true)
            {
                db.Entry(grupo).State = EntityState.Added;               
                grupo.EstadoSincronizacion = EstadoRegistro.REGISTRADO_REMOTAMENTE;
            }
            else
            {
                db.Entry(grupo).State = EntityState.Modified;               
                grupo.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
            }        

            db.SaveChanges();                    

        }

        public void Delete(Grupo grupo)
        {
            var grupoFind = db.Grupos
                           .Where(grup => grup.IdGrupo == grupo.IdGrupo)
                           .Select(grup => grup.IdGrupo)
                           .ToList();

            bool esNuevoElemento = false;

            if (grupoFind.Count() == 0)
            {
                esNuevoElemento = true;
            }
            else
            {
                esNuevoElemento = false;
            }

            //Limpia empleados
            List<Empleado> empleadosGrupo = db.Empleados
                            .Where(empl => empl.IdGrupo == grupo.IdGrupo).ToList();

            foreach (Empleado empleado in empleadosGrupo)
            {
                empleado.IdGrupo = null;
                empleado.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
                empleado.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }

            //Limpia unidades de reacción
            List<UnidadReaccion> unidadesReaccion = db.UnidadesReaccion
                                .Where(unidadReac => unidadReac.IdGrupo == grupo.IdGrupo).ToList();
            foreach (UnidadReaccion unidadReaccion in unidadesReaccion)
            {
                unidadReaccion.IdGrupo = null;
                unidadReaccion.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
                unidadReaccion.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }

            //Limpia dispostivos
            List<Dispositivo> dispositivos = db.Dispositivos
                                .Where(disp => disp.IdGrupo == grupo.IdGrupo).ToList();
            foreach (Dispositivo dispositivo in dispositivos)
            {
                dispositivo.IdGrupo = null;
                dispositivo.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
                dispositivo.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }                      

            grupo.Empleados = null;
            grupo.UnidadesReaccion = null;
            grupo.Dispositivos = null;

            if (esNuevoElemento == false)
            {
                db.Entry(grupo).State = EntityState.Deleted;
                db.SaveChanges();
            }         
                     
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }

   
}
