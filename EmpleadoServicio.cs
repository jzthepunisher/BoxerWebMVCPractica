using BoxerWeb.Domain.DAL;
using BoxerWeb.Domain.Entidades;
using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Data.Entity;

namespace BoxerWeb.WebUI.Servicios
{
    public class EmpleadoServicio : IDisposable
    {
        private SchoolContext db = new SchoolContext();

        public List<EmpleadoVistaModelo> Get(OpcionesConsulta opcionesConsulta)
        {
            var inicio = CalculadorOpcionesConsulta.CalculaInicio(opcionesConsulta);
                       
            var empleados = db.Empleados.OrderBy(opcionesConsulta.Ordena);                    

            bool dataFiltrada = false;
            if ( ! String.IsNullOrEmpty(opcionesConsulta.CadenaFiltro))
            {
                if (opcionesConsulta.BusquedaRapida == false)
                {

                    switch (opcionesConsulta.CampoOrdenamiento)
                    {
                        case "idEmpleado":
                            empleados = empleados.Where(empleado => empleado.IdEmpleado.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "nombres":
                            empleados = empleados.Where(empleado => empleado.Nombres.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "apellidoPaterno":
                            empleados = empleados.Where(empleado => empleado.ApellidoPaterno.ToString().Contains(opcionesConsulta.CadenaFiltro));
                                    
                            dataFiltrada = true;
                            break;
                        case "apellidoMaterno":
                            empleados = empleados.Where(empleado => empleado.ApellidoMaterno.ToString().Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "dni":
                            empleados = empleados.Where(empleado => empleado.DNI.ToString().Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "cargo":
                            //.Any(ph => ph.PhoneNumber.StartsWith("1"))
                            //empleados = empleados.Where(empleado => empleado.Cargo.(cargo => cargo.Descripcion.ToString().Contains(opcionesConsulta.CadenaFiltro)));
                            empleados = empleados.Where(empleado => empleado.Cargo.Descripcion.ToString().Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                    }
                }
                else
                {
                    empleados = empleados.Where(empleado => empleado.Nombres.Contains(opcionesConsulta.CadenaFiltro) 
                                                            || empleado.ApellidoPaterno.Contains(opcionesConsulta.CadenaFiltro)
                                                            || empleado.ApellidoMaterno.Contains(opcionesConsulta.CadenaFiltro) 
                                                            || empleado.IdEmpleado.Contains(opcionesConsulta.CadenaFiltro)
                                                            || empleado.Cargo.Descripcion.ToString().Contains(opcionesConsulta.CadenaFiltro));
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
                        empleados = empleados.Where(empleado => empleado.Grupo.TurnosGrupos.Any(turnGrup => turnGrup.IdTurno.Contains(campoValorFiltro01)));
                        dataFiltrada = true;
                        break;
                }
            }                      

            empleados = empleados.Skip(inicio)
                           .Take(opcionesConsulta.TamanoPagina);

            var empleadosEjecutado = (List<EmpleadoVistaModelo>)null;

            if (opcionesConsulta.BusquedaRapida == false)
            {
                empleadosEjecutado = empleados.Select(empleado => new EmpleadoVistaModelo
                                    {
                                        IdEmpleado = empleado.IdEmpleado,
                                        Nombres = empleado.Nombres,
                                        ApellidoPaterno = empleado.ApellidoPaterno,
                                        ApellidoMaterno = empleado.ApellidoMaterno,
                                        DNI = empleado.DNI,
                                        Foto=empleado.Foto,
                                        Cargo = empleado.Cargo.Descripcion                                      
                                    }).ToList();
            }
            else
            {
                empleadosEjecutado = empleados.Select(empleado => new EmpleadoVistaModelo
                                    {
                                        IdEmpleado = empleado.IdEmpleado,
                                        Nombres = empleado.Nombres,
                                        ApellidoPaterno = empleado.ApellidoPaterno,
                                        ApellidoMaterno = empleado.ApellidoMaterno,
                                        DNI = empleado.DNI,
                                        Cargo = empleado.Cargo.Descripcion,
                                        Foto = empleado.Foto,
                                        IdGrupo = empleado.IdGrupo,
                                        Grupo = empleado.Grupo.Descripcion
                                    }).ToList();
            }

            long cantidadRegistrosTotal = 0;
            if (dataFiltrada == true)
            {
                cantidadRegistrosTotal = empleadosEjecutado.Count();
            }
            else
            {
                cantidadRegistrosTotal = db.Empleados.Count();
            }

            opcionesConsulta.TotalPaginas = CalculadorOpcionesConsulta.CalculaTotalPaginas(cantidadRegistrosTotal, opcionesConsulta.TamanoPagina);

            return empleadosEjecutado;
        }

        public EmpleadoEditarCrearVistaModelo GetPorId(String idEmpleado)
        {
            //////Empleado empleado = db.Empleados.Find(idEmpleado);         

            if (!String.IsNullOrEmpty(idEmpleado))
            {
                var empleado = db.Empleados
                    .Where(empl => empl.IdEmpleado == idEmpleado)
                    .Select(empl => new EmpleadoEditarCrearVistaModelo
                    {
                        IdEmpleado = empl.IdEmpleado,
                        Nombres = empl.Nombres,
                        ApellidoPaterno = empl.ApellidoPaterno,
                        ApellidoMaterno = empl.ApellidoMaterno,
                        Direccion=empl.Direccion,
                        DNI = empl.DNI,
                        FechaNacimiento=empl.FechaNacimiento,
                        Celular=empl.Celular,
                        Email=empl.Email,
                        IdCargo=empl.IdCargo,
                        Cargo = new CargoVistaModelo { IdCargo =empl.Cargo.IdCargo,Descripcion=empl.Cargo.Descripcion},
                        FechaIngreso =empl.FechaIngreso,
                        FechaBaja=empl.FechaBaja,
                        Foto=empl.Foto,
                        IdGrupo=empl.IdGrupo
                    }).ToList();

                if (empleado.Count() == 0)
                {
                    //throw new System.Data.Entity.Core.ObjectNotFoundException
                    //    (string.Format("Unable to find author with id {0}", id));

                    var empleadoNuevo = new EmpleadoEditarCrearVistaModelo();
                    return empleadoNuevo;
                }
                else
                {
                    //if (empleado.Count() == 1)
                    //{
                        return empleado[0];
                    //}
                    //else
                    //{
                    //    return new EmpleadoEditarCrearVistaModelo();
                    //}

                   
                }
                
            }

            return new EmpleadoEditarCrearVistaModelo();

        }

        public void Insert(Empleado empleado)
        {
            var empleadoFind = db.Empleados
                          .Where(empl => empl.IdEmpleado == empleado.IdEmpleado)
                          .Select(empl => empl.IdEmpleado)
                          .ToList();

            bool esNuevoElemento = false;

            if (empleadoFind.Count() == 0)
            {
                esNuevoElemento = true;
            }
            else
            {
                esNuevoElemento = false;
            }
            
            empleado.Cargo = null;
            empleado.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                     
            if (esNuevoElemento == true)
            {
                db.Empleados.Add(empleado);
                empleado.EstadoSincronizacion = EstadoRegistro.REGISTRADO_REMOTAMENTE;
            }
            else
            {
                db.Entry(empleado).State = EntityState.Modified;
                empleado.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
            }

            db.SaveChanges();
        }

        public void UpdateFoto(String idEmpleado, String nombreArchivoFoto)
        {        
            var empleado = db.Empleados.Find(idEmpleado);
            empleado.Foto = nombreArchivoFoto;
            db.SaveChanges();
        }

        public void Update(Empleado empleado )
        {           

            db.Entry(empleado).State = EntityState.Modified;
            empleado.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            db.Entry(empleado.Cargo).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Empleado empleado)
        {
            db.Entry(empleado).State = EntityState.Deleted;
            //db.Empleados.Remove(empleado);

            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
