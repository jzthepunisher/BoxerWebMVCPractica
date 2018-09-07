using BoxerWeb.Domain.DAL;
using BoxerWeb.Domain.Entidades;
using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.Servicios
{
    public class AutorServicio : IDisposable
    {

        private SchoolContext db = new SchoolContext();

        public List<Autor> Get(OpcionesConsulta opcionesConsulta)
        {
            var inicio = CalculadorOpcionesConsulta.CalculaInicio(opcionesConsulta);

            var authors = db.Autores
                            .OrderBy(opcionesConsulta.Ordena);

            bool dataFiltrada = false;
            if (!String.IsNullOrEmpty(opcionesConsulta.CadenaFiltro))
            {
                switch (opcionesConsulta.CampoOrdenamiento)
                {
                    case "primerNombre":
                        authors = authors.Where(author => author.PrimerNombre.Contains(opcionesConsulta.CadenaFiltro));
                        dataFiltrada = true;
                        break;
                    case "apellido":
                        authors = authors.Where(author => author.Apellido.Contains(opcionesConsulta.CadenaFiltro));
                        dataFiltrada = true;
                        break;                 
                }
            }

            authors = authors.Skip(inicio)
                             .Take(opcionesConsulta.TamanoPagina);

            var autoresEjecutado = authors.ToList();

            long cantidadRegistrosTotal = 0;
            if (dataFiltrada == true)
            {
                cantidadRegistrosTotal = autoresEjecutado.Count;
            }
            else
            {
                cantidadRegistrosTotal = db.Autores.Count();
            }

            opcionesConsulta.TotalPaginas = CalculadorOpcionesConsulta.CalculaTotalPaginas(cantidadRegistrosTotal, opcionesConsulta.TamanoPagina);
            
            //return authors.ToList();

            return autoresEjecutado;
        }

        public Autor GetPorId(long id)
        {
            Autor autor = db.Autores.Find(id);
            if (autor == null)
            {
                //throw new System.Data.Entity.Core.ObjectNotFoundException
                //    (string.Format("Unable to find author with id {0}", id));

                autor = new Autor();
            }           

            return autor;
        }

        public void Update(Autor autor)
        {
            db.Entry(autor).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void Insert(Autor autor)
        {
            db.Autores.Add(autor);

            db.SaveChanges();
        }

        public void Delete(Autor autor)
        {
            db.Autores.Remove(autor);

            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
