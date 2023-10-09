using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatcheoAltice
{
    public class InputBoletin
    {
        public double minVal { get; set; }
        public double maxVal { get; set; }
        public string commisionInput { get; set; }

        private bool isPercentage => commisionInput.Contains("%");
        private double commision => isPercentage ? double.Parse(commisionInput.Replace("%", "")) / 100 : double.Parse(commisionInput);
        public double getCommision() => commision;
        public bool getIsPercentage() => isPercentage;
        public InputBoletin()
        {
            this.minVal = 0;
            this.maxVal = 0;
            this.commisionInput = "";
        }

    }

}
