using BoxerWeb.Domain.DAL;
using BoxerWeb.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.Servicios
{
    public class TurnoGrupoServicio : IDisposable
    {
        private SchoolContext db = new SchoolContext();

        public void Insert(Turno turno)
        {
            var turnosGrupoFind = db.TurnosGrupo
                            .Where(turnGrup => turnGrup.IdTurno == turno.IdTurno)                           
                            .ToList();

            bool esNuevoElemento = false;

            if (turnosGrupoFind.Count() == 0)
            {
                esNuevoElemento = true;
            }
            else
            {
                esNuevoElemento = false;
            }

            if (esNuevoElemento == false)
            {            
                foreach (TurnoGrupo turnoGrupo in turnosGrupoFind)
                {
                    db.Entry(turnoGrupo).State = EntityState.Deleted;                   
                }
            }

            foreach (TurnoGrupo turnoGrupo in turno.TurnosGrupos)
            {
                db.Entry(turnoGrupo).State = EntityState.Added;
                turnoGrupo.Grupo = null;
                turnoGrupo.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                if (esNuevoElemento == false)
                {
                    turnoGrupo.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;                   
                }
                else
                {
                    turnoGrupo.EstadoSincronizacion = EstadoRegistro.REGISTRADO_REMOTAMENTE;
                }
            }     

            db.SaveChanges();

        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
