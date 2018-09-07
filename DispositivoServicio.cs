using BoxerWeb.Domain.DAL;
using BoxerWeb.Domain.Entidades;
using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace BoxerWeb.WebUI.Servicios
{
    public class DispositivoServicio : IDisposable
    {
        private SchoolContext db = new SchoolContext();

        public void Insert(Dispositivo dispositivo)
        {
            var dispositivoFind = db.Dispositivos
                .Where(disp => disp.Imei == dispositivo.Imei)
                .Select(disp =>  disp.Imei)
                .ToList();

            dispositivo.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (dispositivoFind.Count() == 0)
            {                
                dispositivo.EstadoSincronizacion = EstadoRegistro.REGISTRADO_REMOTAMENTE;
                db.Dispositivos.Add(dispositivo);               
            }
            else
            {
                db.Entry(dispositivo).State = EntityState.Modified;               
                dispositivo.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;               
            }

            db.SaveChanges();
        }

        public void Update(Dispositivo dispositivo)
        {
            db.Entry(dispositivo).State = EntityState.Modified;
            dispositivo.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            dispositivo.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;

            db.SaveChanges();
        }

        public DispositivoEditarCrearVistaModelo GetPorId(String imei)
        {
            //////Empleado empleado = db.Empleados.Find(idEmpleado);         

            if ( ! String.IsNullOrEmpty(imei))
            {
                var dispositivo = db.Dispositivos
                    .Where(disp => disp.Imei == imei)
                    .Select(disp => new DispositivoEditarCrearVistaModelo
                    {
                        Imei = disp.Imei,
                        IdSimCard = disp.IdSimCard,
                        Descripcion = disp.Descripcion,
                        NumeroCelular = disp.NumeroCelular,
                        Enviado = disp.Enviado,
                        Recibido = disp.Recibido,
                        Validado = disp.Validado,
                        EstadoSincronizacion = disp.EstadoSincronizacion,
                        FechaCreacion = disp.FechaCreacion                      
                    }).ToList();

                if (dispositivo.Count() == 0)
                {
                    //throw new System.Data.Entity.Core.ObjectNotFoundException
                    //    (string.Format("Unable to find author with id {0}", id));

                    var dispositivoNuevo = new DispositivoEditarCrearVistaModelo();
                    return dispositivoNuevo;
                }
                else
                {                    
                    return dispositivo[0];                
                }

            }

            return new DispositivoEditarCrearVistaModelo();

        }

        public List<DispositivoVistaModelo> Get(OpcionesConsulta opcionesConsulta)
        {
            var inicio = CalculadorOpcionesConsulta.CalculaInicio(opcionesConsulta);

            var dispositivos = db.Dispositivos.OrderBy(opcionesConsulta.Ordena);

            bool dataFiltrada = false;
            if (!String.IsNullOrEmpty(opcionesConsulta.CadenaFiltro))
            {
                if (opcionesConsulta.BusquedaRapida == false)
                {
                    switch (opcionesConsulta.CampoOrdenamiento)
                    {
                    case "imei":
                        dispositivos = dispositivos.Where(dispositivo => dispositivo.Imei.Contains(opcionesConsulta.CadenaFiltro));
                        dataFiltrada = true;
                        break;
                    case "idSimCard":
                        dispositivos = dispositivos.Where(dispositivo => dispositivo.IdSimCard.Contains(opcionesConsulta.CadenaFiltro));
                        dataFiltrada = true;
                        break;
                    case "descripcion":
                        dispositivos = dispositivos.Where(dispositivo => dispositivo.Descripcion.ToString().Contains(opcionesConsulta.CadenaFiltro));
                        dataFiltrada = true;
                        break;
                    case "numeroCelular":
                        dispositivos = dispositivos.Where(dispositivo => dispositivo.NumeroCelular.ToString().Contains(opcionesConsulta.CadenaFiltro));
                        dataFiltrada = true;
                        break;
                    case "validado":
                        dispositivos = dispositivos.Where(dispositivo => dispositivo.Validado.ToString().Contains(opcionesConsulta.CadenaFiltro));
                        dataFiltrada = true;
                        break;                  
                    }
                }
                else
                {
                    dispositivos = dispositivos.Where(dispositivo => dispositivo.Descripcion.Contains(opcionesConsulta.CadenaFiltro) || dispositivo.NumeroCelular.Contains(opcionesConsulta.CadenaFiltro)
                                                            || dispositivo.Validado.ToString().Contains(opcionesConsulta.CadenaFiltro));
                    dataFiltrada = true;
                }
            }
            
            if (opcionesConsulta.CampoFiltro01 != null && opcionesConsulta.CampoFiltro01.ToString() != "")
            {
                string campoValorFiltro01 = "";
                if (opcionesConsulta.CampoValorFiltro01 != null && opcionesConsulta.CampoValorFiltro01.ToString() != "")
                {
                    campoValorFiltro01 = opcionesConsulta.CampoValorFiltro01;
                }
                else
                {
                    campoValorFiltro01 = "";
                }

                switch (opcionesConsulta.CampoFiltro01)
                {
                    case "idTurno":
                        dispositivos = dispositivos.Where(dispositivo => dispositivo.Grupo.TurnosGrupos.Any(turnGrup => turnGrup.IdTurno.Contains(campoValorFiltro01)));
                        dataFiltrada = true;
                        break;
                }
            }

            dispositivos = dispositivos.Skip(inicio)
                           .Take(opcionesConsulta.TamanoPagina);


            var dispositivosEjecutado = (List<DispositivoVistaModelo>)null;

            if (opcionesConsulta.BusquedaRapida == false)
            {
                dispositivosEjecutado = dispositivos.Select(dispositivo => new DispositivoVistaModelo
                {
                    Imei = dispositivo.Imei,
                    IdSimCard = dispositivo.IdSimCard,
                    Descripcion = dispositivo.Descripcion,
                    NumeroCelular = dispositivo.NumeroCelular,
                    Validado = dispositivo.Validado
                }).ToList();
            }
            else
            {
                dispositivosEjecutado = dispositivos.Select(dispositivo => new DispositivoVistaModelo
                {
                    Imei = dispositivo.Imei,
                    IdSimCard = dispositivo.IdSimCard,
                    Descripcion = dispositivo.Descripcion,
                    NumeroCelular = dispositivo.NumeroCelular,
                    Validado = dispositivo.Validado,
                    IdGrupo=dispositivo.IdGrupo,
                    Grupo=dispositivo.Grupo.Descripcion,
                    FotoNegro = dispositivo.FotoNegro,
                }).ToList();
            }           

            long cantidadRegistrosTotal = 0;
            if (dataFiltrada == true)
            {
                cantidadRegistrosTotal = dispositivosEjecutado.Count();
            }
            else
            {
                cantidadRegistrosTotal = db.Dispositivos.Count();
            }

            opcionesConsulta.TotalPaginas = CalculadorOpcionesConsulta.CalculaTotalPaginas(cantidadRegistrosTotal, opcionesConsulta.TamanoPagina);

            return dispositivosEjecutado;
        }

        public void Delete(Dispositivo dispositivo)
        {        
            db.Entry(dispositivo).State = EntityState.Deleted;

            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
