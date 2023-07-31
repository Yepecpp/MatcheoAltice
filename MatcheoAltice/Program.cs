using System;
using System.Windows.Forms;
using OfficeOpenXml;

namespace MatcheoAltice
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var login = new Login();
            while (true)
            {

                Application.Run(login);
                if (login.auth)
                {

                    var Dashboard = new Dashboard();
                    Application.Run(Dashboard);
                    if (Dashboard.reauth)
                    {
                        login = new Login();
                    }
                    else
                        break;

                }
                else break;
            }
        }
    }
}
