﻿using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatcheoAltice
{
    public class Final
    {
        // Nombre Usuario	DN Numero	Fecha Activacion	SIM Card	Total Cantidad Recargas	Total Dias Recargas	Orden Instalacion	Total Monto Recargas	Codistribuidor
        public string Nombre { get; set; }
        public string DNumb { get; set; }
        public Nullable<DateTime> Fecha { get; set; }
        public string FechaTitle => Fecha != null ? DateUtils.parseToSpanish((DateTime)Fecha) : "";
        public string Sim { get; set; }
        public string Estado { get; set; }
        public string OrdenInstalacion { get; set; }
        public string Codistribuidor { get; set; }
        public string Operador { get; set; }
        public string Cedula { get; set; }
        public string TotalDiasRecargas { get; set; }
        public string TotalCantidadRecargas { get; set; }
        public string TotalMontoRecargas { get; set; }

        public Final()
        {
            Nombre = "";
            DNumb = "";
            Fecha = DateTime.Now;
            Sim = "";
            OrdenInstalacion = "";
            Estado = "";
            Codistribuidor = "";
            Cedula = "";
            TotalCantidadRecargas = "";
            TotalDiasRecargas = "";
            TotalMontoRecargas = "";
        }
        public static List<Final> Parse(List<Altice> altDoc, List<Base> baseDoc)
        {
            // join alt.sim on baseDoc.sim 

            //remove the list 
            // Find and mark duplicates

            // create lamda function that recibes a basedoc? and returns a string
            return (from i in altDoc
                    join k in baseDoc
                    on i.DNumb equals k.Numero
                    into joinedDocs
                    from k in joinedDocs.DefaultIfEmpty()
                    select new Final
                    {
                        Nombre = i.Nombre,
                        DNumb = i.DNumb,
                        Fecha = i.FechaActivacion,
                        Sim = i.Sim,
                        Estado = k != null && k.IsDuplicate ? "Duplicado" : i.Estado,
                        OrdenInstalacion = i.Ordenes,
                        Codistribuidor = k?.Vendedor ?? "No asignado",
                        Operador = k?.Operador ?? "No asignado",
                        Cedula = k?.Cedula ?? "No asignado",
                        TotalDiasRecargas = i.TotalDiasRecargas,
                        TotalCantidadRecargas = i.TotalCantidadRecargas,
                        TotalMontoRecargas = i.TotalMontoRecargas
                        // IsDuplicate = k.IsDuplicate // Include the IsDuplicate flag in the Final result
                    }).ToList();
        }
        public static List<Final> Filter(List<Final> DB, string filter, bool cont)
        {
            filter = filter.ToLower();
            IEnumerable<Final> result;
            if (!cont)
            {
                result = from i in DB
                             //filter by any column
                         where (i.Nombre.ToLower().Contains(filter) || i.DNumb.Contains(filter) || i.Sim.ToLower().Contains(filter) ||
                         i.OrdenInstalacion.ToLower().Contains(filter) || i.Codistribuidor.ToLower().Contains(filter) || i.Operador.ToLower().Contains(filter)
                        || i.Estado.ToLower().Contains(filter) || i.FechaTitle.Contains(filter))


                         select i;
            }
            else
            {
                //not contains
                result = from i in DB
                             //filter by any column
                         where !(i.Nombre.ToLower().Contains(filter) || i.DNumb.Contains(filter) || i.Sim.ToLower().Contains(filter) ||
                                                     i.OrdenInstalacion.ToLower().Contains(filter) || i.Codistribuidor.ToLower().Contains(filter) || i.Operador.ToLower().Contains(filter)
                                                                                || i.Estado.ToLower().Contains(filter) || i.FechaTitle.Contains(filter))
                         select i;
            }
            return result.ToList();
        }
    }
}
