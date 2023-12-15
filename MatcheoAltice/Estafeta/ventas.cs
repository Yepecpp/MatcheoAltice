using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatcheoAltice.Estafeta
{
    internal class Ventas { 
       public string Compania { get; set; }
    public string TransactionID { get; set; }
    public DateTime Fecha { get; set; }// fecha_digitacion_orden
    public string Estado { get; set; } //estado_transaccion
    public string Usuario { get; set; }
    public string NomFactura { get; set; }
    public string NomActividad { get; set; }
    public string TipoActividad { get; set; }
    public string RazonServicio { get; set; }
    public string Sim { get; set; }
    public string IDplan { get; set; }
    public string NomPlan { get; set; }
    public string MontoDeposito { get; set; }
    public string MontoEquipo { get; set; }
    public string MontoEquipoImpuestos { get; set; }
    public string MontoFinanciamiento { get; set; }
    public string PagoInicial { get; set; }
    public string MontoPaquetico { get; set; }
    public string MontoOrden { get; set; }
    public string MontoOrdenImpuestos { get; set; }
    public string MontoCargoparcial { get; set; }
    public bool Firmado { get; set; }
    }
}

