using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatcheoAltice.Estafeta
{
    public class Pagos
    {
        public string Caja { get; set; }
        public string Estado { get; set; }
        public string Dte { get; set; }
        public string IdCuenta { get; set; }
        public string Cliente { get; set; }
        public string Subcanal { get; set; }
        public string Monto { get; set; }
        public string ConceptoPago { get; set; }
        public string FPefectivo { get; set; }
        public string FPtarjeta { get; set; }
        public string FPotras { get; set; }
        public string TelContacto { get; set; }
        public string TelContacto2 { get; set; }

        public string Userlogin { get; set; }
        public static List<Pagos> Parse(DataTable x)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x));
            }
            List<Pagos> pagos = new List<Pagos>();
            foreach (DataRow row in x.Rows)
            {
                Pagos pay = new Pagos();
                pay.Caja = row["caja"]?.ToString();
                pay.Estado = row["estado"]?.ToString();
                pay.Dte = DateUtils.parseDate(row["dte"]?.ToString()).ToString("dd/MM/yyyy");
                pay.IdCuenta = row["id_cuenta"]?.ToString();
                pay.Cliente = row["cn"]?.ToString();
                pay.Subcanal = row["id_subcanal"]?.ToString();
                pay.Monto = row["monto"]?.ToString();
                pay.ConceptoPago = row["concepto_pago"]?.ToString();
                pay.FPefectivo = row["fp_efectivo"]?.ToString();
                pay.FPtarjeta = row["fp_tarjeta"]?.ToString();
                pay.FPotras = row["fp_otras"]?.ToString();
                pay.Userlogin = row["userlogin"].ToString();
                pay.TelContacto = row["tel_contacto"]?.ToString();
                pay.TelContacto2 = row["tel_contacto2"]?.ToString();
                pagos.Add(pay);

            }
            return pagos;
        }
        public static List<Pagos> FilterPagos(List<Pagos> pagos, string SearchStr)
        {
            //filter by every field in the entity using toLower()
            SearchStr = SearchStr.ToLower();
            return pagos.Where(k =>
           k.Estado.ToLower().Contains(SearchStr) ||
           k.Monto.ToLower().Contains(SearchStr) ||
           k.Userlogin.ToLower().Contains(SearchStr) ||
           k.ConceptoPago.ToLower().Contains(SearchStr) ||
           k.FPefectivo.ToLower().Contains(SearchStr) ||
           k.FPtarjeta.ToLower().Contains(SearchStr) ||
           k.FPotras.ToLower().Contains(SearchStr) ||
           //format date to string without hours
           k.Dte.Split(' ')[0].Contains(SearchStr) ||
           k.IdCuenta.ToLower().Contains(SearchStr) ||
           k.Subcanal.ToLower().Contains(SearchStr) ||
           k.Caja.ToLower().Contains(SearchStr) ||
           k.TelContacto.ToLower().Contains(SearchStr) ||
           k.TelContacto2.ToLower().Contains(SearchStr) ||
           k.Cliente.ToLower().Contains(SearchStr)).ToList();
        }
        public static List<Pagos> GenerateReport(List<Pagos> finalData)
        {
            var output = new Dictionary<string, Pagos>();

            foreach (var final in finalData)
            {
                if (!output.TryGetValue(final.Userlogin, out Pagos existingOperator))
                {
                    existingOperator = new Pagos
                    {
                        Userlogin = final.Userlogin,
                        FPefectivo = "0",
                        FPtarjeta = "0",
                        FPotras = "0",
                    };
                    output[final.Userlogin] = existingOperator;
                }

                if (Double.TryParse(final.FPefectivo, out double totalEfe))
                {
                    existingOperator.FPefectivo = (double.Parse(existingOperator.FPefectivo) + totalEfe).ToString();
                }

                if (Double.TryParse(final.FPotras, out double totalOtra))
                {
                    existingOperator.FPotras = (double.Parse(existingOperator.FPotras) + totalOtra).ToString();
                }

                if (Double.TryParse(final.FPtarjeta, out double totalTarj))
                {
                    existingOperator.FPtarjeta = (double.Parse(existingOperator.FPtarjeta) + totalTarj).ToString();
                }
            }

            return output.Values.ToList();
        }



    }

}
