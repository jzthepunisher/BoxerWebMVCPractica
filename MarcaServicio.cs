using BoxerWeb.Domain.DAL;
using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace BoxerWeb.WebUI.Servicios
{
    public class MarcaServicio : IDisposable
    {
        private SchoolContext db = new SchoolContext();

        public List<MarcaVistaModelo> Get(OpcionesConsulta opcionesConsulta)
        {
            var inicio = CalculadorOpcionesConsulta.CalculaInicio(opcionesConsulta);

            var marcas = db.Marcas.OrderBy(opcionesConsulta.Ordena);

            bool dataFiltrada = false;
            if (!String.IsNullOrEmpty(opcionesConsulta.CadenaFiltro))
            {
                if (opcionesConsulta.BusquedaRapida == false)
                {
                    switch (opcionesConsulta.CampoOrdenamiento)
                    {
                        case "idMarca":
                            marcas = marcas.Where(marca => marca.IdMarca.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "descripcion":
                            marcas = marcas.Where(marca => marca.Descripcion.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                    }
                }
                else
                {
                    marcas = marcas.Where(marca => marca.IdMarca.Contains(opcionesConsulta.CadenaFiltro) || marca.Descripcion.Contains(opcionesConsulta.CadenaFiltro));
                    dataFiltrada = true;
                }
            }

            marcas = marcas.Skip(inicio)
                           .Take(opcionesConsulta.TamanoPagina);

            var marcasEjecutado = marcas.Select(marca => new MarcaVistaModelo
            {
                IdMarca = marca.IdMarca,
                Descripcion = marca.Descripcion,
            }).ToList();

            long cantidadRegistrosTotal = 0;
            if (dataFiltrada == true)
            {
                cantidadRegistrosTotal = marcasEjecutado.Count();
            }
            else
            {
                cantidadRegistrosTotal = db.Marcas.Count();
            }

            opcionesConsulta.TotalPaginas = CalculadorOpcionesConsulta.CalculaTotalPaginas(cantidadRegistrosTotal, opcionesConsulta.TamanoPagina);

            return marcasEjecutado;
        }
        
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
