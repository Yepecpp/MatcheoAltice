using MatcheoAltice.entities;
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
            var lastInput = _inputs.Last();
            if (lastInput.commisionInput == "")
            {
                _inputs.RemoveAt(_inputs.Count - 1);
            }
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

                double totalVen = 0, totalCant = 0;
                Double.TryParse(final.TotalCantidadRecargas, out totalCant);
                Double.TryParse(final.TotalMontoRecargas, out totalVen);
                if (matchingBoletin != null)
                {
                    double commission = matchingBoletin.getIsPercentage() ? Double.Parse(final.TotalMontoRecargas) * matchingBoletin.getCommision() : matchingBoletin.getCommision();
                    output.Add(new OutBoletin
                    {
                        Codistribuidor = final.Codistribuidor,
                        TotalVendido = totalVen,
                        Comision = commission,
                        Operador = final.Operador,
                        estado = final.Estado,
                        numero = final.DNumb,
                        fecha = final.FechaTitle,
                        Totalcantidad = totalCant,
                    });
                }
                else
                {
                    output.Add(new OutBoletin
                    {
                        Codistribuidor = final.Codistribuidor,
                        TotalVendido = totalVen,
                        Comision = 0.0,
                        Operador = final.Operador,
                        numero = final.DNumb,
                        estado = final.Estado,
                        fecha = final.FechaTitle,
                        Totalcantidad = totalCant,
                    });
                }
            }
            return output;
        }

        public static List<OutBoletin> GenerateReport(List<InputBoletin> _inputs, List<Final> finalData)
        {
            var output = new List<OutBoletin>();
            var lastInput = _inputs.Last();
            if (lastInput.commisionInput == "")
            {
                _inputs.RemoveAt(_inputs.Count - 1);
            }

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
                double totalCant = 0;
                Double.TryParse(final.TotalCantidadRecargas, out totalCant);
                if (existingOperator != null)
                {
                    // If the operator is already in the output list, update the total sales and total commission
                    existingOperator.TotalVendido += totalSales;
                    existingOperator.Comision += totalCommission;
                    existingOperator.Totalcantidad += totalCant;
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
                        Totalcantidad = totalCant,
                    });
                }
            }

            return output;
        }
        public static List<OutBoletin> GenerateReportFromDb(List<InputBoletin> _inputs, List<Final> finalData)
        {
            var output = new List<OutBoletin>();
            var lastInput = _inputs.Last();
            if (lastInput.commisionInput == "")
            {
                _inputs.RemoveAt(_inputs.Count - 1);
            }
            try
            {
                List<IReseller> resellers = IReseller.getResellers();



                foreach (var final in finalData)
                {
                    string codigoDist = final.Codistribuidor.Split(' ')?[0];
                    if (codigoDist == null)
                    {
                        continue;
                    }
                    var reseller = resellers.FirstOrDefault(res => res.Codigo == codigoDist);
                    if (reseller == null)
                    {
                        continue;
                    }
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
                    double totalCant = 0;
                    Double.TryParse(final.TotalCantidadRecargas, out totalCant);
                    if (existingOperator != null)
                    {
                        // If the operator is already in the output list, update the total sales and total commission
                        existingOperator.TotalVendido += totalSales;
                        existingOperator.Comision += totalCommission;
                        existingOperator.Totalcantidad += totalCant;
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
                            Totalcantidad = totalCant,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return output;
        }
    }
}
