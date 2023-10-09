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
            var patterns = new List<IPattern<LocalDate>>
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
            str = str.Split(' ')[0];
            // Format LocalDate to a string with the desired format
            var patterns = new List<IPattern<LocalDate>>
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
    }


}
