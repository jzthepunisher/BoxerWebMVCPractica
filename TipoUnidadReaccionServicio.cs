using BoxerWeb.Domain.DAL;
using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace BoxerWeb.WebUI.Servicios
{
    public class TipoUnidadReaccionServicio : IDisposable
    {
        private SchoolContext db = new SchoolContext();

        public List<TipoUnidadReaccionVistaModelo> Get(OpcionesConsulta opcionesConsulta)
        {
            var inicio = CalculadorOpcionesConsulta.CalculaInicio(opcionesConsulta);

            var tiposUnidadReaccion = db.TiposUnidadesReaccion.OrderBy(opcionesConsulta.Ordena);

            bool dataFiltrada = false;
            if (!String.IsNullOrEmpty(opcionesConsulta.CadenaFiltro))
            {
                if (opcionesConsulta.BusquedaRapida == false)
                {
                    switch (opcionesConsulta.CampoOrdenamiento)
                    {
                        case "idCargo":
                            tiposUnidadReaccion = tiposUnidadReaccion.Where(tipoUnidadReaccion => tipoUnidadReaccion.IdTipoUnidadReaccion.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "descripcion":
                            tiposUnidadReaccion = tiposUnidadReaccion.Where(tipoUnidadReaccion => tipoUnidadReaccion.Descripcion.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                    }
                }
                else
                {
                    tiposUnidadReaccion = tiposUnidadReaccion.Where(tipoUnidadReaccion => tipoUnidadReaccion.IdTipoUnidadReaccion.Contains(opcionesConsulta.CadenaFiltro) || tipoUnidadReaccion.Descripcion.Contains(opcionesConsulta.CadenaFiltro));
                    dataFiltrada = true;
                }
            }

            tiposUnidadReaccion = tiposUnidadReaccion.Skip(inicio)
                           .Take(opcionesConsulta.TamanoPagina);

            var tiposUnidadReaccionsEjecutado = tiposUnidadReaccion.Select(tipoUnidadReaccion => new TipoUnidadReaccionVistaModelo
            {
                IdTipoUnidadReaccion = tipoUnidadReaccion.IdTipoUnidadReaccion,
                Descripcion = tipoUnidadReaccion.Descripcion,
                Foto= tipoUnidadReaccion.Foto
            }).ToList();

            long cantidadRegistrosTotal = 0;
            if (dataFiltrada == true)
            {
                cantidadRegistrosTotal = tiposUnidadReaccionsEjecutado.Count();
            }
            else
            {
                cantidadRegistrosTotal = db.TiposUnidadesReaccion.Count();
            }

            opcionesConsulta.TotalPaginas = CalculadorOpcionesConsulta.CalculaTotalPaginas(cantidadRegistrosTotal, opcionesConsulta.TamanoPagina);

            return tiposUnidadReaccionsEjecutado;
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}
