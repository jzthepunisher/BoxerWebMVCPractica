using BoxerWeb.Domain.DAL;
using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace BoxerWeb.WebUI.Servicios
{
    public class ContratoServicio : IDisposable
    {
        private SchoolContext db = new SchoolContext();

        public List<ContratoVistaModelo> Get(OpcionesConsulta opcionesConsulta)
        {
            var inicio = CalculadorOpcionesConsulta.CalculaInicio(opcionesConsulta);

            var contratos = db.Contratos.OrderBy(opcionesConsulta.Ordena);

            bool dataFiltrada = false;
            if (!String.IsNullOrEmpty(opcionesConsulta.CadenaFiltro))
            {
                if (opcionesConsulta.BusquedaRapida == false)
                {
                    switch (opcionesConsulta.CampoOrdenamiento)
                    {
                        case "idContrato":
                            contratos = contratos.Where(contrato => contrato.IdContrato.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "direccion":
                            contratos = contratos.Where(contrato => contrato.Direccion.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                    }
                }
                else
                {
                    contratos = contratos.Where(contrato => contrato.IdContrato.Contains(opcionesConsulta.CadenaFiltro) 
                                                            || contrato.Direccion.Contains(opcionesConsulta.CadenaFiltro)
                                                            || contrato.MonitoreoActivo.ToString().Contains(opcionesConsulta.CadenaFiltro));
                    dataFiltrada = true;
                }
            }

            contratos = contratos.Skip(inicio)
                                .Take(opcionesConsulta.TamanoPagina);

            var contratosEjecutado = contratos.Select
            (
                contrato => 
                new ContratoVistaModelo
                {
                    IdContrato = contrato.IdContrato,
                    Direccion = contrato.Direccion,
                    Latitud = contrato.Latitud,
                    Longitud=contrato.Longitud,
                    MonitoreoActivo=contrato.MonitoreoActivo
                }
            ).ToList();

            long cantidadRegistrosTotal = 0;
            if (dataFiltrada == true)
            {
                cantidadRegistrosTotal = contratosEjecutado.Count();
            }
            else
            {
                cantidadRegistrosTotal = db.Colores.Count();
            }

            opcionesConsulta.TotalPaginas = CalculadorOpcionesConsulta.CalculaTotalPaginas(cantidadRegistrosTotal, opcionesConsulta.TamanoPagina);

            return contratosEjecutado;
        }

        public List<ContratoMapaTermicoVistaModelo> GetContratosMapaTermicos(OpcionesConsulta opcionesConsulta)
        {

            bool dataFiltrada = false;
            var contratosMapaTermico = db.Contratos.OrderBy(opcionesConsulta.Ordena)
                                                   .Where(contrato => contrato.MonitoreoActivo == true);

            dataFiltrada = true;

            var contratosEjecutado = contratosMapaTermico.Select
            (
                contrato =>
                new ContratoMapaTermicoVistaModelo
                {
                    IdContrato = contrato.IdContrato,               
                    Latitud = contrato.Latitud,
                    Longitud = contrato.Longitud              
                }
            ).ToList();

            long cantidadRegistrosTotal = 0;
            if (dataFiltrada == true)
            {
                cantidadRegistrosTotal = contratosEjecutado.Count();
            }
            else
            {
                cantidadRegistrosTotal = db.Colores.Count();
            }

            return contratosEjecutado;
        }

        
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
