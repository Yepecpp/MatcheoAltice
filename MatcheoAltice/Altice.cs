﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MatcheoAltice
{
    public class Altice
    {
        //Nombre Usuario	DN Numero	Fecha Activacion	Sim	Ordenes	Total Dias Recargas	Total Monto Recargas
        public string Nombre { get; set; }
        public string DNumb { get; set; }
        public DateTime? FechaActivacion { get; set; }
        public string Sim { get; set; }
        public string Ordenes { get; set; }
        public string Estado { get; set; }
        public string TotalDiasRecargas { get; set; }
        public string TotalCantidadRecargas { get; set; }
        public string TotalMontoRecargas { get; set; }



        public static List<Altice> Parse(DataTable x)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x));
            }
            //declare a lambda function to parse the date
            Func<string, DateTime?> parseDate = (string row) =>
            {
                if (string.IsNullOrEmpty(row))
                    return null;
                DateTime parse = DateUtils.parseDate(row);
                if (parse == DateTime.MinValue)
                    return null;
                return parse;
            };

            return (from DataRow row in x.Rows
                    select new Altice
                    {
                        Nombre = row["Nombre Usuario"].ToString(),
                        DNumb = row["DN Numero"].ToString(),
                        FechaActivacion = row.IsNull("Fecha Activacion") ? null : parseDate(row["Fecha Activacion"].ToString()),
                        Sim = row["SIM Card"].ToString(),
                        Estado = row["Estado"].ToString(),
                        Ordenes = row["Orden Instalacion"].ToString(),
                        TotalCantidadRecargas = row["Total Cantidad Recargas"].ToString(),
                        TotalDiasRecargas = row["Total Dias Recargas"].ToString(),
                        TotalMontoRecargas = row["Total Monto Recargas"].ToString()
                    }).Where(k => !string.IsNullOrEmpty(k.DNumb))
                    .ToList();
        }
        public Altice()
        {
            Nombre = "";
            DNumb = "";
            FechaActivacion = DateTime.Now;
            Sim = "";
            Ordenes = "";
        }

    }
}
