using BoxerWeb.Domain.DAL;
using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.Servicios
{
    public class CargoServicio : IDisposable
    {
        private SchoolContext db = new SchoolContext();

        public List<CargoVistaModelo> Get(OpcionesConsulta opcionesConsulta)
        {
            var inicio = CalculadorOpcionesConsulta.CalculaInicio(opcionesConsulta);

            var cargos = db.Cargos.OrderBy(opcionesConsulta.Ordena);

            bool dataFiltrada = false;
            if (!String.IsNullOrEmpty(opcionesConsulta.CadenaFiltro))
            {

                if (opcionesConsulta.BusquedaRapida == false)
                {
                    switch (opcionesConsulta.CampoOrdenamiento)
                    {
                        case "idCargo":
                            cargos = cargos.Where(cargo => cargo.IdCargo.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                        case "descripcion":
                            cargos = cargos.Where(cargo => cargo.Descripcion.Contains(opcionesConsulta.CadenaFiltro));
                            dataFiltrada = true;
                            break;
                    }
                }
                else
                {                     
                    cargos = cargos.Where(cargo => cargo.IdCargo.Contains(opcionesConsulta.CadenaFiltro) || cargo.Descripcion.Contains(opcionesConsulta.CadenaFiltro));                     
                    dataFiltrada = true;                     
                }
                
            }

            cargos = cargos.Skip(inicio)
                           .Take(opcionesConsulta.TamanoPagina);

            var cargosEjecutado = cargos.Select
                (cargo => 
                    new CargoVistaModelo
                    {
                        IdCargo = cargo.IdCargo,
                        Descripcion = cargo.Descripcion                
                    }
                ).ToList();

            long cantidadRegistrosTotal = 0;
            if (dataFiltrada == true)
            {
                cantidadRegistrosTotal = cargosEjecutado.Count();
            }
            else
            {
                cantidadRegistrosTotal = db.Cargos.Count();
            }

            opcionesConsulta.TotalPaginas = CalculadorOpcionesConsulta.CalculaTotalPaginas(cantidadRegistrosTotal, opcionesConsulta.TamanoPagina);

            return cargosEjecutado;
        }

        public CargoVistaModelo GetPorId(String idCargo)
        {
            //////Empleado empleado = db.Empleados.Find(idEmpleado);         

            if (!String.IsNullOrEmpty(idCargo))
            {
                var cargo = db.Cargos
                    .Where(carg => carg.IdCargo == idCargo)
                    .Select
                    (carg => 
                        new CargoVistaModelo
                        {
                            IdCargo = carg.IdCargo,
                            Descripcion = carg.Descripcion                       
                        }
                    ).ToList();

                if (cargo.Count() == 0)
                {
                    //throw new System.Data.Entity.Core.ObjectNotFoundException
                    //    (string.Format("Unable to find author with id {0}", id));

                    var cargoNuevo = new CargoVistaModelo();
                    return cargoNuevo;
                }
                else
                {                   
                    return cargo[0];
                }
            }

            return new CargoVistaModelo();

        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
