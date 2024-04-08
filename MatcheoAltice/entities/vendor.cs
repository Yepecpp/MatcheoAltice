using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatcheoAltice.entities
{
    public class IReseller
    {
        public IReseller()
        {
        }
        static readonly string cString = ConfigurationManager.ConnectionStrings["ConexionAC"].ConnectionString;

        // 1. ID de vendedor
        // Assuming the ID is of type int. The mechanism for auto-generation or manual setting is not represented here.
        public int? Id { get; set; }
        // 2. Codigo del Revendedor
        public string Codigo { get; set; }

        // 2. Nombre del Revendedor
        public string Nombre { get; set; }

        // 3. Teléfonos
        // Assuming a single string to hold potentially multiple phone numbers or an array/list of strings.
        // Adjust according to your requirements.
        public string Telefonos { get; set; }

        // 4. Provincia
        public string Provincia { get; set; }

        // 5. Dirección
        public string Direccion { get; set; }

        // 6. Ejecutivo que lo manejara
        public string Ejecutivo { get; set; }

        // 7. Supervisor que lo visitará
        public string Supervisor { get; set; }

        // 8. Fecha de la captación
        // Using DateTime to represent dates.
        public DateTime FechaCaptacion { get; set; }

        // 9. Número de cuenta de banco de pago
        public string NumeroCuentaBanco { get; set; }

        // 10. Entidad bancaria
        public string EntidadBancaria { get; set; }

        // 11. Titular de la cuenta
        public string TitularCuenta { get; set; }
        public static IReseller createReller(IReseller reseller)
        {


            try
            {
                using (var Conexion = new MySql.Data.MySqlClient.MySqlConnection(cString))
                {
                    Conexion.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                        $"INSERT INTO Vendedor (Codigo,Nombre, Telefonos, Provincia, Direccion, Ejecutivo, Supervisor, FechaCaptacion, NumeroCuentaBanco, EntidadBancaria, TitularCuenta) VALUES ('{reseller.Codigo}', '{reseller.Nombre}', '{reseller.Telefonos}', '{reseller.Provincia}', '{reseller.Direccion}', '{reseller.Ejecutivo}', '{reseller.Supervisor}', '{reseller.FechaCaptacion.ToString("yyyy-MM-dd")}', '{reseller.NumeroCuentaBanco}', '{reseller.EntidadBancaria}', '{reseller.TitularCuenta}')",
                        Conexion))
                    {
                        cmd.ExecuteNonQuery();

                        // Fetch the auto-generated ID
                        var id = cmd.LastInsertedId;

                        // Assign the ID to the returned seller
                        reseller.Id = (int)id;
                    }
                    Conexion.Close();
                }
            }
            catch
            {
                return null;
            }

            return reseller;

        }
        static public List<IReseller> getResellers()
        {


            var resellers = new List<IReseller>();
            try
            {
                using (var Conexion = new MySql.Data.MySqlClient.MySqlConnection(cString))
                {
                    Conexion.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                                                      $"SELECT * FROM Vendedor",
                                                                                    Conexion))
                    {
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            resellers.Add(new IReseller
                            {
                                Id = dr.GetInt32(0),
                                Codigo = dr.GetString(1),
                                Nombre = dr.GetString(2),
                                Telefonos = dr.GetString(3),
                                Provincia = dr.GetString(4),
                                Direccion = dr.GetString(5),
                                Ejecutivo = dr.GetString(5),
                                Supervisor = dr.GetString(7),
                                FechaCaptacion = dr.GetDateTime(8),
                                NumeroCuentaBanco = dr.GetString(9),
                                EntidadBancaria = dr.GetString(10),
                                TitularCuenta = dr.GetString(11)
                            });
                        }
                    }
                    Conexion.Close();
                }

            }
            catch
            {
                return null;
            }

            return resellers;
        }
        static public bool CheckRellerTable()
        {


            try
            {
                using (var Conexion = new MySql.Data.MySqlClient.MySqlConnection(cString))
                {
                    Conexion.Open();
                    //check if the Usuarios table exists
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                               $"SHOW TABLES LIKE 'Vendedor'",
                               Conexion))
                    {
                        var dr = cmd.ExecuteReader();
                        if (!dr.Read())
                        {
                            Conexion.Close();
                            Conexion.Open();
                            var cmd2 = new MySql.Data.MySqlClient.MySqlScript(Conexion,
                                                               "create table if not exists Vendedor( \nid int not null auto_increment primary key,\n Codigo varchar(50) not null, \nNombre varchar(50) not null, \nTelefonos varchar(50) not null, \nProvincia varchar(50), \nDireccion varchar(50), \nEjecutivo varchar(50), \nSupervisor varchar(50) , \nFechaCaptacion date , \nNumeroCuentaBanco varchar(50) , \nEntidadBancaria varchar(50), \nTitularCuenta varchar(50));");

                            cmd2.Execute();
                        }
                        Conexion.Close();
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        //create the update and the delete methods
        static public IReseller updateReseller(IReseller targetRes)
        {
            if (targetRes == null || targetRes.Id is null)
            {
                return null;
            }
            try
            {

                using (var Conexion = new MySql.Data.MySqlClient.MySqlConnection(cString))
                {
                    Conexion.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                                           $"UPDATE Vendedor SET Nombre = '{targetRes.Nombre}', Telefonos = '{targetRes.Telefonos}', Codigo='{targetRes.Codigo}', Provincia = '{targetRes.Provincia}', Direccion = '{targetRes.Direccion}', Ejecutivo = '{targetRes.Ejecutivo}', Supervisor = '{targetRes.Supervisor}', FechaCaptacion = '{targetRes.FechaCaptacion.ToString("yyyy-MM-dd")}', NumeroCuentaBanco = '{targetRes.NumeroCuentaBanco}', EntidadBancaria = '{targetRes.EntidadBancaria}', TitularCuenta = '{targetRes.TitularCuenta}' WHERE id = {targetRes.Id}",
                                                              Conexion))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    Conexion.Close();
                }
                return targetRes;

            }
            catch
            {
                return null;
            }

        }
        static public bool deleteReseller(int id)
        {
            try
            {
                using (var Conexion = new MySql.Data.MySqlClient.MySqlConnection(cString))
                {
                    Conexion.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                                                                  $"DELETE FROM Vendedor WHERE id = {id}",
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
        static public List<IReseller> Filter(List<IReseller> list, string filter, bool contains) {
            //contains true means that string.contains must be true and else string.contains must be false
            filter = filter.ToLower();
            IEnumerable<IReseller> result;
            if (!contains)
            {
                result = from i in list
                         where (i.Nombre.ToLower().Contains(filter) || i.Codigo.ToLower().Contains(filter) || i.Telefonos.ToLower().Contains(filter) ||
                                            i.Provincia.ToLower().Contains(filter) || i.Direccion.ToLower().Contains(filter) || i.Ejecutivo.ToLower().Contains(filter)
                                           || i.Supervisor.ToLower().Contains(filter) || i.FechaCaptacion.ToString("dd/MM/yyyy").Contains(filter) || i.NumeroCuentaBanco.ToLower().Contains(filter)
                        || i.EntidadBancaria.ToLower().Contains(filter) || i.TitularCuenta.ToLower().Contains(filter))
                         select i;
            }
            else
            {
                result = from i in list
                         where !(i.Nombre.ToLower().Contains(filter) || i.Codigo.ToLower().Contains(filter) || i.Telefonos.ToLower().Contains(filter) ||
                     i.Provincia.ToLower().Contains(filter) || i.Direccion.ToLower().Contains(filter) || i.Ejecutivo.ToLower().Contains(filter)
                        || i.Supervisor.ToLower().Contains(filter) || i.FechaCaptacion.ToString("dd/MM/yyyy").Contains(filter) || i.NumeroCuentaBanco.ToLower().Contains(filter)
                        || i.EntidadBancaria.ToLower().Contains(filter) || i.TitularCuenta.ToLower().Contains(filter))
                         select i;
             }
         return result.ToList();
        }
    }
}
