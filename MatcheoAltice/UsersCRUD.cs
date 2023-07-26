using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace MatcheoAltice
{
    public partial class UsersCRUD : Form
    {
        public UsersCRUD()
        {
            InitializeComponent();
        }

        private string id;
        MySql.Data.MySqlClient.MySqlConnection Conectar = new MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings["ConexionAC"].ConnectionString);

        private void Mostrar()
        {
            string mostrar = "SELECT * FROM Usuarios";
            MySql.Data.MySqlClient.MySqlDataAdapter Adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(mostrar, Conectar);
            DataTable dt = new DataTable();
            Adapter.Fill(dt);
            tablaUsuarios.DataSource = dt;
        }

        private void UsersCRUD_Load(object sender, EventArgs e)
        {
            Mostrar();
            tablaUsuarios.ClearSelection();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void nuevo()
        {
            id = "";
            txtNombre.Clear();
            txtClave.Clear();
            txtRol.SelectedIndex = -1;
        }

        private void insert_Click(object sender, EventArgs e)
        {
            string Name = txtNombre.Text, Pass = txtClave.Text, Rol = txtRol.Text;

            if (Name == "" || Pass == "" || Rol == "")
            {
                MessageBox.Show("No deje campos vacíos", "Error!");
            }
            else
            {
                using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(
                           $"INSERT INTO Usuarios (nombre,Clave,Rol, Cod_Usuario) VALUES ('{Name}','{Pass}','{Rol}', null)", Conectar))
                {
                    Conectar.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Se ha agregado exitosamente el usuario", "¡Enhorabuena!");
                    Mostrar();
                    Conectar.Close();
                }
                nuevo();
            }
        }

        private void tablaUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = tablaUsuarios.CurrentRow.Cells[0].Value.ToString();

            txtNombre.Text = tablaUsuarios.CurrentRow.Cells[1].Value.ToString();
            txtClave.Text = tablaUsuarios.CurrentRow.Cells[4].Value.ToString();
            txtRol.Text = tablaUsuarios.CurrentRow.Cells[3].Value.ToString();
        }

        private void update_Click(object sender, EventArgs e)
        {
            string Name = txtNombre.Text, Pass = txtClave.Text, Rol = txtRol.Text;

            if (txtNombre.Text == "" || txtClave.Text == "" || txtRol.Text == "")
            {
                MessageBox.Show("No deje campos vacíos", "Error!");
            }
            else
            {
                using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand($"UPDATE Usuarios SET nombre='{Name}',Clave='{Pass}',Rol='{Rol}' WHERE id = '{id}'", Conectar))
                {
                    Conectar.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Se ha editado exitosamente el usuario", "¡Enhorabuena!");
                    Mostrar();
                    Conectar.Close();
                }
                nuevo();
            }
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {

        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
