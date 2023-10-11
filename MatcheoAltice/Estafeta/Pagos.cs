using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatcheoAltice.Estafeta
{
    internal class Pagos
    {
        // el campo de cliente es el que se llama cn en el documento de pagos
        //el campo que se llama DTE son las fechas de los pagos
        public string Compania { get; set; }
        public bool Estado { get; set; }
        public string Monto { get; set; }
        public string Userlogin { get; set; }
        public string ConceptoPago{ get; set; }
        public string TransactionID { get; set; }
        public string FPefectivo{ get; set; }
        public string FPtarjeta{ get; set; }
        public string FPcheques { get; set; }
        public string FPotras { get; set; }
        public string Cliente { get; set; }
        public DateTime DTE { get; set; }
        public static List<Base> Parse(DataTable x)
    }
}
