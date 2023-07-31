using MySqlX.XDevAPI;
using OfficeOpenXml.FormulaParsing.Excel.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatcheoAltice
{
    internal class InputBoletin
    {
        public double minVal;
        public double maxVal;
        string commisionInput;
        private bool isPercentage;
        private double commision;
        public double getCommision() => commision;
        public bool getIsPercentage() => isPercentage;

        InputBoletin(double minVal, double maxVal, string commisionInput, bool isPercentage)
        {
            this.minVal = minVal;
            this.maxVal = maxVal;
            this.commisionInput = commisionInput;
            this.isPercentage = commisionInput.Contains("%");
            this.commision = isPercentage ? double.Parse(commisionInput.Replace("%", "")) / 100 : double.Parse(commisionInput);
        }
    }
    class OutBoletin
    {
        public string Operator;
        public double? TotalVendido;
        public double Comision;
        public List<OutBoletin> IndividualGenerateReport(List<InputBoletin> _inputs, List<Final> finalData, String operatorName = null)
        {
            if (operatorName != null)
            {
                finalData = finalData.Where(final => final.Operador == operatorName).ToList();
            }
            var output = new List<OutBoletin>();

            foreach (var final in finalData)
            {
                // Find the matching input boletin based on the final's value
                var matchingBoletin = _inputs.FirstOrDefault(input => Double.Parse(final.TotalMontoRecargas) >= input.getCommision() && Double.Parse(final.TotalMontoRecargas) <= input.maxVal);

                if (matchingBoletin != null)
                {
                    double commission = matchingBoletin.getIsPercentage() ? Double.Parse(final.TotalMontoRecargas) * matchingBoletin.getCommision() : matchingBoletin.getCommision();
                    output.Add(new OutBoletin
                    {
                        Operator = final.Operador,
                        Comision = commission
                    });
                }
            }
            return output;
        }
        public List<OutBoletin> GenerateReport(List<InputBoletin> _inputs, List<Final> finalData)
        {
            var output = new List<OutBoletin>();

            foreach (var final in finalData)
            {
                double TotalVendido = Double.Parse(final.TotalMontoRecargas);
                double totalSales = TotalVendido;
                double totalCommission = 0.0;

                // Calculate the commission for each individual sale based on the matching InputBoletin
                foreach (var input in _inputs)
                {
                    if (totalSales >= input.minVal && totalSales <= input.maxVal)
                    {
                        double commission = input.getIsPercentage() ? totalSales * input.getCommision() : input.getCommision();
                        totalCommission += commission;
                    }
                }

                // Check if the operator is already present in the output list
                var existingOperator = output.FirstOrDefault(o => o.Operator == final.Operador);

                if (existingOperator != null)
                {
                    // If the operator is already in the output list, update the total sales and total commission
                    existingOperator.TotalVendido += totalSales;
                    existingOperator.Comision += totalCommission;
                }
                else
                {
                    // If the operator is not in the output list, add a new entry for the operator
                    output.Add(new OutBoletin
                    {
                        Operator = final.Operador,
                        TotalVendido = totalSales,
                        Comision = totalCommission
                    });
                }
            }

            return output;
        }

    }
}
