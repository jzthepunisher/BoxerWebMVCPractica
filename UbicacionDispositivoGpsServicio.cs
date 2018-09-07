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
    public class UbicacionDispositivoGpsServicio : IDisposable
    {
        private SchoolContext db = new SchoolContext();

        public void Insert(UbicacionDispositivoGps ubicacionDispositivoGps)
        {
            //var dispositivoFind = db.Dispositivos
            //    .Where(disp => disp.Imei == ubicacionDispositivoGps.IdUbicacion)
            //    .Select(disp => disp.Imei)
            //    .ToList();

            //ubicacionDispositivoGps.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            //if (dispositivoFind.Count() == 0)
            //{
            //    ubicacionDispositivoGps.EstadoSincronizacion = EstadoRegistro.REGISTRADO_REMOTAMENTE;
            //    db.Dispositivos.Add(ubicacionDispositivoGps);
            //}
            //else
            //{
            //    db.Entry(ubicacionDispositivoGps).State = EntityState.Modified;
            //    ubicacionDispositivoGps.EstadoSincronizacion = EstadoRegistro.ACTUALIZADO_REMOTAMENTE;
            //}

            //db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
