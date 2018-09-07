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
    public class ClienteServicio : IDisposable
    {
        private SchoolContext db = new SchoolContext();

        public List<ClienteVistaModelo> Get(OpcionesConsulta opcionesConsulta)
        {
            var inicio = CalculadorOpcionesConsulta.CalculaInicio(opcionesConsulta);
         
            var nuevoOrdenamiento = "";
            if (opcionesConsulta.CampoOrdenamiento == "primerNombre")
            {
                nuevoOrdenamiento = nuevoOrdenamiento + opcionesConsulta.Ordena + ",RazonSocial " + opcionesConsulta.Orden;
            }
            else
            {
                nuevoOrdenamiento = opcionesConsulta.Ordena ;
            }

            var clientes = db.Clientes.OrderBy(nuevoOrdenamiento);

            bool dataFiltrada = false;
            if ( ! String.IsNullOrEmpty(opcionesConsulta.CadenaFiltro))
            {
                if (opcionesConsulta.BusquedaRapida == false)
                {
                    switch (opcionesConsulta.CampoOrdenamiento)
                    {
                        case "idCliente":
                            clientes = clientes.Where(cliente => cliente.IdCliente.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "primerNombre":
                            clientes = clientes.Where(cliente => cliente.PrimerNombre.Contains(opcionesConsulta.CadenaFiltro)
                                                                || cliente.SegundoNombre.Contains(opcionesConsulta.CadenaFiltro)
                                                                || cliente.ApellidoPaterno.Contains(opcionesConsulta.CadenaFiltro)
                                                                || cliente.ApellidoMaterno.Contains(opcionesConsulta.CadenaFiltro)
                                                                || cliente.RazonSocial.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "numeroDocumento":
                            clientes = clientes.Where(cliente => cliente.NumeroDocumento.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "direccion":
                            clientes = clientes.Where(cliente => cliente.Direccion.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;                       
                    }
                }
                else
                {
                    //////clientes = clientes.Where(unidadReaccion => unidadReaccion.Descripcion.Contains(opcionesConsulta.CadenaFiltro) || unidadReaccion.IdUnidadReaccion.Contains(opcionesConsulta.CadenaFiltro)
                    //////                                        || unidadReaccion.Placa.Contains(opcionesConsulta.CadenaFiltro));
                    //////dataFiltrada = true;
                }

            }

            clientes = clientes.Skip(inicio)
                               .Take(opcionesConsulta.TamanoPagina);

            var clientesEjecutado = (List<ClienteVistaModelo>)null;

            if (opcionesConsulta.BusquedaRapida == false)
            {
                clientesEjecutado = clientes.Select
                (
                    cliente => 
                    new ClienteVistaModelo
                    {
                        IdCliente = cliente.IdCliente,
                        PrimerNombre = cliente.PrimerNombre,
                        SegundoNombre = cliente.SegundoNombre, 
                        ApellidoPaterno=cliente.ApellidoPaterno,
                        ApellidoMaterno=cliente.ApellidoMaterno,
                        RazonSocial=cliente.RazonSocial,                   
                        NumeroDocumento = cliente.NumeroDocumento,
                        Direccion = cliente.Direccion,
                        IdTipoDocumento = cliente.IdTipoDocumento
                    }
                ).ToList();
                //DescripcionCliente =cliente.IdTipoDocumento=="2" ? cliente.PrimerNombre + ' ' + cliente.SegundoNombre + ' ' + cliente.ApellidoPaterno + ' ' + cliente.ApellidoMaterno : cliente.RazonSocial,
            }
            else
            {
                //////clientesEjecutado = clientes.Select(unidadReaccion => new UnidadReaccionVistaModelo
                //////{
                //////    IdUnidadReaccion = unidadReaccion.IdUnidadReaccion,
                //////    Descripcion = unidadReaccion.Descripcion,
                //////    TipoUnidadReaccion = unidadReaccion.TipoUnidadReaccion.Descripcion,
                //////    Placa = unidadReaccion.Placa,
                //////    Marca = unidadReaccion.Marca.Descripcion,
                //////    Modelo = unidadReaccion.Modelo,
                //////    IdGrupo = unidadReaccion.IdGrupo,
                //////    Grupo = unidadReaccion.Grupo.Descripcion,
                //////    FotoNegro = unidadReaccion.TipoUnidadReaccion.FotoNegro,
                //////    Foto = unidadReaccion.TipoUnidadReaccion.Foto
                //////}).ToList();
            }
            
            long cantidadRegistrosTotal = 0;
            if (dataFiltrada == true)
            {
                cantidadRegistrosTotal = clientesEjecutado.Count();
            }
            else
            {
                cantidadRegistrosTotal = db.Clientes.Count();
            }

            opcionesConsulta.TotalPaginas = CalculadorOpcionesConsulta.CalculaTotalPaginas(cantidadRegistrosTotal, opcionesConsulta.TamanoPagina);

            return clientesEjecutado;
        }

        public ClienteEditarCrearVistaModelo GetPorId(String idCliente)
        {
            if (!String.IsNullOrEmpty(idCliente))
            {
                var cliente = db.Clientes
                    .Where(clie => clie.IdCliente == idCliente)
                    .Select(clie => new ClienteEditarCrearVistaModelo
                    {
                        IdCliente = clie.IdCliente,
                        PrimerNombre = clie.PrimerNombre,
                        SegundoNombre = clie.SegundoNombre,
                        ApellidoPaterno = clie.ApellidoPaterno,
                        ApellidoMaterno = clie.ApellidoMaterno,
                        NumeroDocumento=clie.NumeroDocumento,
                        Direccion=clie.Direccion,
                        RazonSocial=clie.RazonSocial,
                        IdTipoDocumento=clie.IdTipoDocumento,
                        ContratosVistaModelo= (from contrato in clie.Contratos
                                               orderby contrato.IdContrato
                                               select new ContratoVistaModelo
                                               {
                                                   IdContrato = contrato.IdContrato,
                                                   Direccion = contrato.Direccion,
                                                   Latitud = contrato.Latitud,
                                                   Longitud = contrato.Longitud,
                                                   MonitoreoActivo = contrato.MonitoreoActivo,
                                                   IdCliente = contrato.IdCliente
                                               }).ToList()                   
                    }).ToList();

                if (cliente.Count() == 0)
                {
                    var clienteNuevo = new ClienteEditarCrearVistaModelo();
                    return clienteNuevo;
                }
                else
                {
                    return cliente[0];
                }

            }

            return new ClienteEditarCrearVistaModelo();
        }

        public void Insert(Cliente cliente)
        {
            var clienteFind = db.Clientes
                            .Where(clie => clie.IdCliente == cliente.IdCliente)
                            .Select(clie => clie.IdCliente)
                            .ToList();

            bool esNuevoElemento = false;

            if (clienteFind.Count() == 0)
            {
                esNuevoElemento = true;
            }
            else
            {
                esNuevoElemento = false;
            }
                       

            //Actualiza empleado con el grupo
            foreach (var contrato in cliente.Contratos)
            {
                var contratoFind = db.Contratos
                                    .Where(cont => cont.IdContrato == contrato.IdContrato)
                                    .Select(cont => cont.IdContrato)
                                    .ToList();

                bool esNuevoContrato = false;

                if (contratoFind.Count() == 0)
                {
                    esNuevoContrato = true;
                }
                else
                {
                    esNuevoContrato = false;
                }

                if (esNuevoContrato == true)
                {
                    db.Entry(contrato).State = EntityState.Added;
                    contrato.EstadoSincronizacion = EstadoRegistro.REGISTRADO_REMOTAMENTE;
                    contrato.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                }
                else
                {
                    db.Entry(contrato).State = EntityState.Modified;
                    contrato.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
                    contrato.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                }                          
            }        
       
            cliente.Contratos = null;
            cliente.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (esNuevoElemento == true)
            {
                db.Entry(cliente).State = EntityState.Added;
                cliente.EstadoSincronizacion = EstadoRegistro.REGISTRADO_REMOTAMENTE;
            }
            else
            {
                db.Entry(cliente).State = EntityState.Modified;
                cliente.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
            }

            db.SaveChanges();

        }

        public void Delete(Cliente cliente)
        {
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
