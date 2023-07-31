using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MatcheoAltice
{
    public class Base
    {
        // Fechas	Cedula	Sim	Numero	Vendedor	Operador
        public DateTime? Fecha { get; set; }
        public string Sim { get; set; }
        public string Cedula { get; set; }
        public string Numero { get; set; }
        public string Vendedor { get; set; }
        public string Operador { get; set; }
        public bool IsDuplicate { get; set; }
        public static List<Base> Parse(DataTable x)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x));
            }
            List<Base> bases = new List<Base>();
            foreach (DataRow row in x.Rows)
            {
                Base b = new Base();


                DateTime parse;
                b.Fecha = row.IsNull("Fechas")
                    ? null
                    : DateTime.TryParse(row["Fechas"].ToString(), out parse) ?

                       // try to parse the date, the format is "dd/MM/yyyy hh:mm:ss", split the string in two it and just use the d/m/y part
                       parse
                        :
                        // if the date is empty, return null
                        (DateTime?)null;

                b.Sim = row["Sim"].ToString();
                b.Cedula = row["Cedula"].ToString();
                b.Numero = row["Numero"].ToString();
                b.Vendedor = row["Vendedor"].ToString();
                b.Operador = row["Operador"].ToString();


                bases.Add(b);
            }
            return bases.OrderBy(k => k.Fecha)
                              .Where(k => !string.IsNullOrEmpty(k.Numero)) // Remove records where k.Numero is an empty string
                              .GroupBy(k => k.Numero)
                              .SelectMany(g => g.Select((record, index) => new { Record = record, Index = index }))
                              .Select(item =>
                              {
                                  var record = item.Record;
                                  if (item.Index > 0)
                                      record.IsDuplicate = true;
                                  else record.IsDuplicate = false;

                                  return record;
                              })
                              .ToList();
        }
        public Base()
        {
            Fecha = DateTime.Now;
            Sim = "";
            Numero = "";
            Vendedor = "";
            Operador = "";
        }

    }
}
