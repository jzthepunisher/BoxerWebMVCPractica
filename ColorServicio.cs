using BoxerWeb.Domain.DAL;
using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace BoxerWeb.WebUI.Servicios
{
    public class ColorServicio : IDisposable
    {
        private SchoolContext db = new SchoolContext();

        public List<ColorVistaModelo> Get(OpcionesConsulta opcionesConsulta)
        {
            var inicio = CalculadorOpcionesConsulta.CalculaInicio(opcionesConsulta);

            var colores = db.Colores.OrderBy(opcionesConsulta.Ordena);

            bool dataFiltrada = false;
            if (!String.IsNullOrEmpty(opcionesConsulta.CadenaFiltro))
            {
                if (opcionesConsulta.BusquedaRapida == false)
                {
                    switch (opcionesConsulta.CampoOrdenamiento)
                    {
                        case "idColor":
                            colores = colores.Where(color => color.IdColor.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "descripcion":
                            colores = colores.Where(color => color.Descripcion.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                    }
                }
                else
                {
                    colores = colores.Where(color => color.IdColor.Contains(opcionesConsulta.CadenaFiltro) || color.Descripcion.Contains(opcionesConsulta.CadenaFiltro));
                    dataFiltrada = true;
                }
            }

            colores = colores.Skip(inicio)
                             .Take(opcionesConsulta.TamanoPagina);

            var coloresEjecutado = colores.Select
            (
                color => 
                new ColorVistaModelo
                {
                    IdColor = color.IdColor,
                    Descripcion = color.Descripcion,
                }
            ).ToList();

            long cantidadRegistrosTotal = 0;
            if (dataFiltrada == true)
            {
                cantidadRegistrosTotal = coloresEjecutado.Count();
            }
            else
            {
                cantidadRegistrosTotal = db.Colores.Count();
            }

            opcionesConsulta.TotalPaginas = CalculadorOpcionesConsulta.CalculaTotalPaginas(cantidadRegistrosTotal, opcionesConsulta.TamanoPagina);

            return coloresEjecutado;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
