using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace MatcheoAltice.Estafeta
{
    public class Venta
    {
        // Properties for the Venta entity based on the selected columns
        public DateTime FechaDigitacionOrden { get; set; }
        public string EstadoTransaccion { get; set; }
        public string UsuarioCreoOrden { get; set; }
        public string Cedula { get; set; }
        public string Pasaporte { get; set; }
        public string NomFactura { get; set; }
        public string NomSubcanal { get; set; }
        public string NomActividad { get; set; }
        public string TipoActividad { get; set; }
        public string RazonServicio { get; set; }
        public string Telefono { get; set; }
        public string Sim { get; set; }
        public string NomPlan { get; set; }
        public string GrupoActivacionOrden { get; set; }
        public decimal MontoOrden { get; set; }
        public decimal MontoOrdenConImpuestos { get; set; }
        public string TelContacto { get; set; }
        public string TelContacto2 { get; set; }
        public string EstaFirmado { get; set; }
        public string Vendedor { get; set; }

        // Method to parse a DataTable into a list of Venta objects
        public static List<Venta> Parse(DataTable dataTable)
        {
            if (dataTable == null)
                throw new ArgumentNullException(nameof(dataTable));

            var ventasList = new List<Venta>();
            foreach (DataRow row in dataTable.Rows)
            {
                var venta = new Venta
                {
                    FechaDigitacionOrden = DateTime.Parse(row["fecha_digitacion_orden"].ToString()),
                    EstadoTransaccion = row["estado_transaccion"]?.ToString(),
                    UsuarioCreoOrden = row["usuario_creo_orden"]?.ToString(),
                    Cedula = row["cedula"]?.ToString(),
                    Pasaporte = row["pasaporte"]?.ToString(),
                    NomFactura = row["nom_factura"]?.ToString(),
                    NomSubcanal = row["nom_subcanal"]?.ToString(),
                    NomActividad = row["nom_actividad"]?.ToString(),
                    TipoActividad = row["tipo_actividad"]?.ToString(),
                    RazonServicio = row["razon_servicio"]?.ToString(),
                    Telefono = row["telefono"]?.ToString(),
                    Sim = row["sim"]?.ToString(),
                    NomPlan = row["nom_plan"]?.ToString(),
                    GrupoActivacionOrden = row["grupo_activacion_orden"]?.ToString(),
                    MontoOrden = decimal.Parse(row["monto_orden"]?.ToString() ?? "0"),
                    MontoOrdenConImpuestos = decimal.Parse(row["monto_orden_con_impuestos"]?.ToString() ?? "0"),
                    TelContacto = row["tel_contacto"]?.ToString(),
                    TelContacto2 = row["tel_contacto2"]?.ToString(),
                    EstaFirmado = row["esta_firmado"]?.ToString(),
                    Vendedor = row["vendedor"]?.ToString()
                };
                ventasList.Add(venta);
            }
            return ventasList;
        }

        public static List<Venta> FilterVentas(List<Venta> ventasList, string searchStr)
        {
            searchStr = searchStr?.ToLower() ?? string.Empty;

            return ventasList.Where(venta =>
                (venta.EstadoTransaccion != null && venta.EstadoTransaccion.ToLower().Contains(searchStr)) ||
                (venta.UsuarioCreoOrden != null && venta.UsuarioCreoOrden.ToLower().Contains(searchStr)) ||
                (venta.Cedula != null && venta.Cedula.ToLower().Contains(searchStr)) ||
                (venta.Pasaporte != null && venta.Pasaporte.ToLower().Contains(searchStr)) ||
                (venta.NomFactura != null && venta.NomFactura.ToLower().Contains(searchStr)) ||
                (venta.NomSubcanal != null && venta.NomSubcanal.ToLower().Contains(searchStr)) ||
                (venta.NomActividad != null && venta.NomActividad.ToLower().Contains(searchStr)) ||
                (venta.TipoActividad != null && venta.TipoActividad.ToLower().Contains(searchStr)) ||
                (venta.RazonServicio != null && venta.RazonServicio.ToLower().Contains(searchStr)) ||
                (venta.Telefono != null && venta.Telefono.ToLower().Contains(searchStr)) ||
                (venta.Sim != null && venta.Sim.ToLower().Contains(searchStr)) ||
                (venta.NomPlan != null && venta.NomPlan.ToLower().Contains(searchStr)) ||
                (venta.GrupoActivacionOrden != null && venta.GrupoActivacionOrden.ToLower().Contains(searchStr)) ||
                (venta.MontoOrden.ToString().Contains(searchStr)) ||
                (venta.MontoOrdenConImpuestos.ToString().Contains(searchStr)) ||
                (venta.TelContacto != null && venta.TelContacto.ToLower().Contains(searchStr)) ||
                (venta.TelContacto2 != null && venta.TelContacto2.ToLower().Contains(searchStr)) ||
                (venta.EstaFirmado.ToString().ToLower().Contains(searchStr)) ||
                (venta.Vendedor != null && venta.Vendedor.ToLower().Contains(searchStr))
            ).ToList();
        }

    }
}