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
    public class UnidadReaccionServicio : IDisposable
    {
        private SchoolContext db = new SchoolContext();

        public List<UnidadReaccionVistaModelo> Get(OpcionesConsulta opcionesConsulta)
        {
            var inicio = CalculadorOpcionesConsulta.CalculaInicio(opcionesConsulta);

            var unidadesReaccion = db.UnidadesReaccion.OrderBy(opcionesConsulta.Ordena);

            bool dataFiltrada = false;
            if (!String.IsNullOrEmpty(opcionesConsulta.CadenaFiltro))
            {
                if (opcionesConsulta.BusquedaRapida == false)
                {
                    switch (opcionesConsulta.CampoOrdenamiento)
                    {
                        case "idUnidadReaccion":
                            unidadesReaccion = unidadesReaccion.Where(unidadReaccion => unidadReaccion.IdUnidadReaccion.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "descripcion":
                            unidadesReaccion = unidadesReaccion.Where(unidadReaccion => unidadReaccion.Descripcion.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "tipoUnidadReaccion":
                            unidadesReaccion = unidadesReaccion.Where(unidadReaccion => unidadReaccion.TipoUnidadReaccion.Descripcion.ToString().Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "placa":
                            unidadesReaccion = unidadesReaccion.Where(unidadReaccion => unidadReaccion.Placa.ToString().Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "marca":
                            unidadesReaccion = unidadesReaccion.Where(empleado => empleado.Marca.Descripcion.ToString().Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "modelo":
                            //.Any(ph => ph.PhoneNumber.StartsWith("1"))
                            //empleados = empleados.Where(empleado => empleado.Cargo.(cargo => cargo.Descripcion.ToString().Contains(opcionesConsulta.CadenaFiltro)));
                            unidadesReaccion = unidadesReaccion.Where(unidadReaccion => unidadReaccion.Modelo.ToString().Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                    }
                }
                else
                {
                    unidadesReaccion = unidadesReaccion.Where(unidadReaccion => unidadReaccion.Descripcion.Contains(opcionesConsulta.CadenaFiltro) || unidadReaccion.IdUnidadReaccion.Contains(opcionesConsulta.CadenaFiltro)
                                                            || unidadReaccion.Placa.Contains(opcionesConsulta.CadenaFiltro));
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
                        unidadesReaccion = unidadesReaccion.Where(unidadReaccion => unidadReaccion.Grupo.TurnosGrupos.Any(turnGrup => turnGrup.IdTurno.Contains(campoValorFiltro01)));
                        dataFiltrada = true;
                        break;
                }
            }

            unidadesReaccion = unidadesReaccion.Skip(inicio)
                           .Take(opcionesConsulta.TamanoPagina);

            var unidadesReaccionEjecutado = (List<UnidadReaccionVistaModelo>)null;

            if (opcionesConsulta.BusquedaRapida == false)
            {
                unidadesReaccionEjecutado = unidadesReaccion.Select(unidadReaccion => new UnidadReaccionVistaModelo
                {
                    IdUnidadReaccion = unidadReaccion.IdUnidadReaccion,
                    Descripcion = unidadReaccion.Descripcion,
                    TipoUnidadReaccion = unidadReaccion.TipoUnidadReaccion.Descripcion,
                    Placa = unidadReaccion.Placa,
                    Marca = unidadReaccion.Marca.Descripcion,
                    Modelo = unidadReaccion.Modelo
                }).ToList();
            }
            else
            {
                unidadesReaccionEjecutado = unidadesReaccion.Select(unidadReaccion => new UnidadReaccionVistaModelo
                {
                    IdUnidadReaccion = unidadReaccion.IdUnidadReaccion,
                    Descripcion = unidadReaccion.Descripcion,
                    TipoUnidadReaccion = unidadReaccion.TipoUnidadReaccion.Descripcion,
                    Placa = unidadReaccion.Placa,
                    Marca = unidadReaccion.Marca.Descripcion,
                    Modelo = unidadReaccion.Modelo,
                    IdGrupo = unidadReaccion.IdGrupo,
                    Grupo = unidadReaccion.Grupo.Descripcion,
                    FotoNegro=unidadReaccion.TipoUnidadReaccion.FotoNegro,
                    Foto=unidadReaccion.TipoUnidadReaccion.Foto
                }).ToList();
            }
           
            long cantidadRegistrosTotal = 0;
            if (dataFiltrada == true)
            {
                cantidadRegistrosTotal = unidadesReaccionEjecutado.Count();
            }
            else
            {
                cantidadRegistrosTotal = db.UnidadesReaccion.Count();
            }

            opcionesConsulta.TotalPaginas = CalculadorOpcionesConsulta.CalculaTotalPaginas(cantidadRegistrosTotal, opcionesConsulta.TamanoPagina);

            return unidadesReaccionEjecutado;
        }

        public UnidadReaccionEditarCrearVistaModelo GetPorId(String idUnidadReaccion)
        {      

            if (!String.IsNullOrEmpty(idUnidadReaccion))
            {
                var unidadReaccion = db.UnidadesReaccion
                    .Where(uniReac => uniReac.IdUnidadReaccion == idUnidadReaccion)
                    .Select(uniReac => new UnidadReaccionEditarCrearVistaModelo
                    {
                        IdUnidadReaccion = uniReac.IdUnidadReaccion,
                        Descripcion = uniReac.Descripcion,
                        IdTipoUnidadReaccion = uniReac.IdTipoUnidadReaccion,
                        TipoUnidadReaccion = new TipoUnidadReaccionVistaModelo
                                {
                                    IdTipoUnidadReaccion = uniReac.TipoUnidadReaccion.IdTipoUnidadReaccion,
                                    Descripcion = uniReac.TipoUnidadReaccion.Descripcion,
                                    Foto = uniReac.TipoUnidadReaccion.Foto
                                },
                        Placa = uniReac.Placa,
                        IdMarca = uniReac.IdMarca,
                        Marca = new MarcaVistaModelo
                                {
                                    IdMarca = uniReac.Marca.IdMarca,
                                    Descripcion = uniReac.Marca.Descripcion
                                },
                        Modelo = uniReac.Modelo,
                        IdColor = uniReac.IdColor,                     
                        Color = new ColorVistaModelo
                                {
                                    IdColor = uniReac.Color.IdColor,
                                    Descripcion = uniReac.Color.Descripcion
                                }

                    }).ToList();

                if (unidadReaccion.Count() == 0)
                {               
                    var unidadReaccionNuevo = new UnidadReaccionEditarCrearVistaModelo();
                    return unidadReaccionNuevo;
                }
                else
                {                  
                    return unidadReaccion[0];                
                }

            }

            return new UnidadReaccionEditarCrearVistaModelo();
        }

        public void Insert(UnidadReaccion unidadReaccion)
        {
            var unidadReaccionFind = db.UnidadesReaccion
                          .Where(unid => unid.IdUnidadReaccion == unidadReaccion.IdUnidadReaccion)
                          .Select(unid => unid.IdUnidadReaccion)
                          .ToList();

            bool esNuevoElemento = false;

            if (unidadReaccionFind.Count() == 0)
            {
                esNuevoElemento = true;
            }
            else
            {
                esNuevoElemento = false;
            }


            if (unidadReaccion.IdMarca == "")
            {
                unidadReaccion.IdMarca = null;
            }

            if (unidadReaccion.IdColor == "")
            {
                unidadReaccion.IdColor = null;
            }

            unidadReaccion.TipoUnidadReaccion = null;
            unidadReaccion.Marca = null;
            unidadReaccion.Color = null;

            unidadReaccion.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            if (esNuevoElemento == true)
            {               
                db.UnidadesReaccion.Add(unidadReaccion);
                unidadReaccion.EstadoSincronizacion = EstadoRegistro.REGISTRADO_REMOTAMENTE;
            }
            else
            {
                db.Entry(unidadReaccion).State = EntityState.Modified;
                unidadReaccion.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
            }
            
            db.SaveChanges();
        }

        public void Update(UnidadReaccion unidadReaccion)
        {
            if (unidadReaccion.IdMarca == "")
            {
                unidadReaccion.IdMarca = null;
            }

            if (unidadReaccion.IdColor == "")
            {
                unidadReaccion.IdColor = null;
            }

            unidadReaccion.TipoUnidadReaccion = null;
            unidadReaccion.Marca = null;
            unidadReaccion.Color = null;

            db.Entry(unidadReaccion).State = EntityState.Modified;           
        
            db.SaveChanges();
        }

        public void Delete(UnidadReaccion unidadReaccion)
        {
            if (unidadReaccion.IdMarca == "")
            {
                unidadReaccion.IdMarca = null;
            }

            if (unidadReaccion.IdColor == "")
            {
                unidadReaccion.IdColor = null;
            }

            unidadReaccion.TipoUnidadReaccion = null;
            unidadReaccion.Marca = null;
            unidadReaccion.Color = null;

            db.Entry(unidadReaccion).State = EntityState.Deleted;
           
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
