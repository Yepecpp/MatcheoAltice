using NodaTime.Text;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatcheoAltice
{
    internal class DateUtils
    {
        static public DateTime parseDate(DateTime x)
        {
            var str = x.ToString().Split(' ')[0];

            // Format LocalDate to a string with the desired format
            var patterns = new IPattern<LocalDate>[]
    {
        LocalDatePattern.CreateWithInvariantCulture("dd/MM/yyyy"),
        LocalDatePattern.CreateWithInvariantCulture("d/MM/yyyy"),
        LocalDatePattern.CreateWithInvariantCulture("d/M/yyyy"),
        LocalDatePattern.CreateWithInvariantCulture("dd-MM-yyyy")
    };
            if (patterns.Any(pattern => pattern.Parse(str).Success))
            {
                var parsedDate = patterns.First(pattern => pattern.Parse(str).Success).Parse(str).Value;
                return parsedDate.ToDateTimeUnspecified();
            }
            return DateTime.MinValue;
        }
        static public string parseToSpanish(DateTime x)
        {
            LocalDate localDate = LocalDateTime.FromDateTime(x).Date;
            var pattern = LocalDatePattern.CreateWithInvariantCulture("dd/MM/yyyy");
            var formatted = pattern.Format(localDate);
            return formatted.ToString();
        }
        static public DateTime parseDate(string str)
        {
            //str recives 45171, convert it to DatetTime
            if (Double.TryParse(str, out double value))
            {
                return DateTime.FromOADate(value);
            }
           
            str = str.Split(' ')[0];
            // Format LocalDate to a string with the desired format
            var patterns = new IPattern<LocalDate>[]
    {
        LocalDatePattern.CreateWithInvariantCulture("dd/MM/yyyy"),
        LocalDatePattern.CreateWithInvariantCulture("d/MM/yyyy"),
        LocalDatePattern.CreateWithInvariantCulture("d/M/yyyy"),
        LocalDatePattern.CreateWithInvariantCulture("dd-MM-yyyy"),
        LocalDatePattern.CreateWithInvariantCulture("d/M/yy"),
        LocalDatePattern.CreateWithInvariantCulture("d/M/yyyy"),
        LocalDatePattern.CreateWithInvariantCulture("dd/MM/yyyy"),
        LocalDatePattern.CreateWithInvariantCulture("MM/dd/yyyy"),
        LocalDatePattern.CreateWithInvariantCulture("MM/dd/yy"),
        LocalDatePattern.CreateWithInvariantCulture("M/dd/yyyy"),
        LocalDatePattern.CreateWithInvariantCulture("M/d/yyyy")
    };
            if (str == null || str == "")
            {
                return DateTime.MinValue;
            }
            try
            {

                var rightPattern = patterns.First(pattern => pattern.Parse(str).Success);
                return rightPattern.Parse(str).Value.ToDateTimeUnspecified();
            }
            catch (Exception e)
            {
                return DateTime.MinValue;
            }
        }

        //parse a double and format it to a string with 2 decimals and a comma as mark of thousands
        static public string parseDouble(double x)
        {
            return $"{x.ToString("N2")}$";
        }
    }



}
