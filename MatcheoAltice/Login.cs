using System;
using System.Configuration;
using System.Windows.Forms;

namespace MatcheoAltice
{
    public partial class Login : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public Login()
        {
            InitializeComponent();
        }
        public bool auth = false;
        public void LoginC()
        {
            if (txtClave.Text == "" || txtUsuario.Text == "")
            {
                MessageBox.Show("Rellene todos los Campos", "Error!");
            }
            else
            {
                try
                {
                    var cString = ConfigurationManager.ConnectionStrings["ConexionAC"].ConnectionString;
                    string User = txtUsuario.Text, Pass = txtClave.Text;
                    using (var Conexion = new MySql.Data.MySqlClient.MySqlConnection(cString))
                    {
                        Conexion.Open();
                        using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                                   $"SELECT * FROM Usuarios WHERE Cod_Usuario = '{txtUsuario.Text}' AND Clave = '{txtClave.Text}' ",
                                   Conexion))
                        {
                            var dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                Properties.Settings.Default.Rol = dr.GetString(3);
                                Properties.Settings.Default.Usuario = dr.GetString(1);
                                auth = true;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Datos Incorrectos", "Error!");
                            }
                        }

                        Conexion.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("No pudo establecer la coneccion a la base de datos",
                        @"error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            LoginC();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void verClave_Click(object sender, EventArgs e)
        {
            txtClave.PasswordChar = '\0';
            ocultarClave.BringToFront();
        }

        private void OcultarClave_Click(object sender, EventArgs e)
        {
            txtClave.PasswordChar = '•';
            verClave.BringToFront();
        }

        private void IconButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                string cString = ConfigurationManager.ConnectionStrings["ConexionAC"].ConnectionString;
                using (var Conexion = new MySql.Data.MySqlClient.MySqlConnection(cString))
                {
                    Conexion.Open();
                    //check if the Usuarios table exists
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                               $"SHOW TABLES LIKE 'Usuarios'",
                               Conexion))
                    {
                        var dr = cmd.ExecuteReader();
                        if (!dr.Read())
                        {
                            Conexion.Close();
                            Conexion.Open();
                            MessageBox.Show("No se ha encontrado la tabla Usuarios, se creara una por defecto",
                                "Error!");
                            var cmd2 = new MySql.Data.MySqlClient.MySqlScript(Conexion,
                                $"create table if not exists Usuarios( \nid int not null auto_increment primary key, \nnombre varchar(50) not null, \nCod_Usuario varchar(10), \nRol varchar(10) not null, \nClave varchar(250) not null); \ndelimiter // \ncreate trigger Usuarios_before_insert " +
                                $"\nbefore insert on Usuarios \nfor each row \nbegin \ndeclare number int; \nselect LAST_INSERT_ID()+1 into number from Usuarios; \nif number is null or number = 0 then \nset number = 1; \nend if; " +
                                $"\nset new.Cod_Usuario = concat(year(now()),'-',lpad(number,4,'0')); \nend//");
                            cmd2.Execute();
                        }


                    }

                    Conexion.Close();

                }
                using (var Conexion = new MySql.Data.MySqlClient.MySqlConnection(cString))
                {
                    Conexion.Open();
                    //check if the Usuarios table exists

                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                               $"SELECT * FROM Usuarios WHERE Rol='admin'",
                               Conexion))
                    {
                        var dr = cmd.ExecuteReader();
                        if (!dr.Read())
                        {
                            Conexion.Close();
                            Conexion.Open();
                            MessageBox.Show("No se ha encontrado el usuario admin, se creara uno por defecto", "Error!");
                            using (var cmd2 = new MySql.Data.MySqlClient.MySqlCommand(
                                       $"INSERT INTO Usuarios (Nombre, Clave, Rol, Cod_Usuario) VALUES ('Josue','admin','admin', null)",
                                       Conexion))
                            {
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }
                    Conexion.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                var response = MessageBox.Show("No pudo establecer la coneccion a la base de datos",
                     @"error",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error);

                if (response == DialogResult.OK)
                    Application.Exit();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
