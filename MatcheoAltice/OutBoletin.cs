using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatcheoAltice
{
    public class OutBoletin
    {
        public string Codistribuidor;
        public double TotalVendido;
        public double Comision;
        public string Operador;
        public string estado;
        public string fecha;
        public string numero;
        public double? Totalcantidad;
        public static List<OutBoletin> IndividualGenerateReport(List<InputBoletin> _inputs, List<Final> finalData, String operatorName = null)
        {
            if (operatorName != null)
            {
                finalData = finalData.Where(final => final.Operador == operatorName).ToList();
            }
            var output = new List<OutBoletin>();

            foreach (var final in finalData)
            {
                // Find the matching input boletin based on the final's value
                var matchingBoletin = _inputs.FirstOrDefault(input => Double.Parse(final.TotalMontoRecargas) >=
                input.minVal && Double.Parse(final.TotalMontoRecargas) <= input.maxVal);

                if (matchingBoletin != null)
                {
                    double commission = matchingBoletin.getIsPercentage() ? Double.Parse(final.TotalMontoRecargas) * matchingBoletin.getCommision() : matchingBoletin.getCommision();
                    output.Add(new OutBoletin
                    {
                        Codistribuidor = final.Codistribuidor,
                        TotalVendido = Double.Parse(final.TotalMontoRecargas),
                        Comision = commission,
                        Operador = final.Operador,
                        estado = final.Estado,
                        numero = final.DNumb,
                        fecha = final.FechaTitle,
                        Totalcantidad = Double.Parse(final.TotalCantidadRecargas),
                    });
                }
                else
                {
                    output.Add(new OutBoletin
                    {
                        Codistribuidor = final.Codistribuidor,
                        TotalVendido = Double.Parse(final.TotalMontoRecargas),
                        Comision = 0.0,
                        Operador = final.Operador,
                        numero = final.DNumb,

                        estado = final.Estado,
                        fecha = final.FechaTitle,
                        Totalcantidad = Double.Parse(final.TotalCantidadRecargas),
                    });
                }
            }
            return output;
        }
        public static List<OutBoletin> GenerateReport(List<InputBoletin> _inputs, List<Final> finalData)
        {
            var output = new List<OutBoletin>();

            foreach (var final in finalData)
            {
                double totalSales = Double.Parse(final.TotalMontoRecargas);
                double totalCommission = 0.0;
                var matchingBoletin = _inputs.FirstOrDefault(input => totalSales >= input.minVal && totalSales <= input.maxVal);

                // Calculate the commission for each individual sale based on the matching InputBoletin

                if (matchingBoletin != null)
                {
                    totalCommission = matchingBoletin.getIsPercentage() ? totalSales * matchingBoletin.getCommision() : matchingBoletin.getCommision();
                }
                // Check if the operator is already present in the output list
                var existingOperator = output.FirstOrDefault(o => o.Codistribuidor == final.Operador);

                if (existingOperator != null)
                {
                    // If the operator is already in the output list, update the total sales and total commission
                    existingOperator.TotalVendido += totalSales;
                    existingOperator.Comision += totalCommission;
                    existingOperator.Totalcantidad += Double.Parse(final.TotalCantidadRecargas);
                }
                else
                {
                    // If the operator is not in the output list, add a new entry for the operator
                    output.Add(new OutBoletin
                    {
                        Codistribuidor = final.Codistribuidor,
                        TotalVendido = totalSales,
                        Comision = totalCommission,
                        Operador = final.Operador,
                        Totalcantidad = Double.Parse(final.TotalCantidadRecargas),
                    });
                }
            }

            return output;
        }

    }
}
