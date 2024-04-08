using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatcheoAltice.entities
{
    public class Sim
    {
        //               // 1. ID de SIM
        // Assuming the ID is of type int. The mechanism for auto-generation or manual setting is not represented here.
        public int? Id { get; set; }
        // 2. Número de SIM
        public string Serial { get; set; }
        public string Plan { get; set; }
        public DateTime? FechaActivacion { get; set; }
        // 8. Revendedor asociado
        private IReseller Revendedor { get; set; }
        public string RevendedorNombre => Revendedor.Nombre;
        public int RevendedorId => Revendedor.Id ?? 0;
        static readonly string cString = ConfigurationManager.ConnectionStrings["ConexionAC"].ConnectionString;
        public Sim(IReseller res)

        {
            Revendedor = res;
        }
        public static List<Sim>  getSims()
        {
            try
            {
                using (var Conexion = new MySql.Data.MySqlClient.MySqlConnection(cString))
                {
                    Conexion.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                                                                  "SELECT SIM.Id, Serial, Plan, FechaActivacion, Revendedor, Vendedor.Nombre FROM SIM INNER JOIN Vendedor ON SIM.Revendedor = Vendedor.Id",
                                                                                                                               Conexion))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            List<Sim> sims = new List<Sim>();
                            while (reader.Read())
                            {
                                sims.Add(new Sim(new IReseller
                                {
                                    Id = reader.GetInt32(4),
                                    Nombre = reader.GetString(5)
                                })
                                {
                                    Id = reader.GetInt32(0),
                                    Serial = reader.GetString(1),
                                    Plan = reader.GetString(2),
                                    FechaActivacion = reader.GetDateTime(3),

                                });
                            }
                            return sims;
                        }
                    }
                }
            }
            catch
            {
                return new List<Sim>();
            }
        }
        public static List<Sim> Filter (List<Sim> sims, string filter, bool contains)
        {
            filter = filter.ToLower();
            return (from s in sims
                                       where (contains ? s.Serial.ToLower().Contains(filter) : s.Serial.ToLower() == filter) ||
                                                                (contains ? s.Plan.ToLower().Contains(filter) : s.Plan.ToLower() == filter) ||
                                                                                         (contains ? s.RevendedorNombre.ToLower().Contains(filter) : s.RevendedorNombre.ToLower() == filter)
                                                                                                            select s).ToList();
        }
        public static bool checkSimTable()
        {
            //there is a relation between SIM and Reseller (Vendedor), create if not exists
            if (false == IReseller.CheckRellerTable())
            {
                return false;
            }
            try
            {
                using (var Conexion = new MySql.Data.MySqlClient.MySqlConnection(cString))
                {
                    Conexion.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                                           "CREATE TABLE IF NOT EXISTS SIM (Id INT AUTO_INCREMENT, Serial VARCHAR(255), Plan VARCHAR(255), FechaActivacion date not null,  Revendedor INT, PRIMARY KEY (Id), FOREIGN KEY (Revendedor) REFERENCES Vendedor(Id))",
                                                              Conexion))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    Conexion.Close();
                }
            }
            catch
            {
                return false;

            }
            return true;

        }
        //complete the crud operations
        public static Sim createSim(Sim sim)
        {
            if (false == checkSimTable())
            {
                return null;
            }
            try
            {
                using (var Conexion = new MySql.Data.MySqlClient.MySqlConnection(cString))
                {
                    Conexion.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                                               $"INSERT INTO SIM (Serial, Plan, Revendedor, FechaActivacion ) VALUES ('{sim.Serial}', '{sim.Plan}', '{sim.RevendedorId}', '{sim.FechaActivacion.ToString()}')",
                                                                      Conexion))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    Conexion.Close();
                }
            }
            catch
            {
                return null;
            }
            return sim;
        }
        public static Sim updateSim(Sim sim)
        {
            if (sim == null || sim.Id is null)
            {
                return null;
            }
            try
            {
                using (var Conexion = new MySql.Data.MySqlClient.MySqlConnection(cString))
                {
                    Conexion.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                                                                  $"UPDATE SIM SET Serial = '{sim.Serial}', Plan = '{sim.Plan}', Revendedor = '{sim.RevendedorId}' WHERE id = {sim.Id}",
                                                                                                                               Conexion))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    Conexion.Close();
                }
                return sim;

            }
            catch
            {
                return null;
            }

        }
        public static bool deleteSim(int id)
        {
            try
            {
                using (var Conexion = new MySql.Data.MySqlClient.MySqlConnection(cString))
                {
                    Conexion.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                                                                                         $"DELETE FROM SIM WHERE id = {id}",
                                                                                                                                                                                                                   Conexion))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    Conexion.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
