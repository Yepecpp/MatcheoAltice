using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatcheoAltice
{
    public partial class BoletinScreen : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        List<Final> finalData;
        public BoletinScreen(List<Final> _final)
        {
            InitializeComponent();
            finalData = _final;
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                iconButton6.IconChar = FontAwesome.Sharp.IconChar.WindowMaximize;
                return;
            }

            this.WindowState = FormWindowState.Maximized;
            iconButton6.IconChar = FontAwesome.Sharp.IconChar.WindowRestore;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void inputBoletinBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
        List<InputBoletin> inputs = new List<InputBoletin>();
        private void BoletinScreen_Load(object sender, EventArgs e)
        {
            inputBoletinBindingSource.DataSource = inputs;
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            //tota
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Workbook|*.xlsx";
            saveFileDialog.Title = "Exportar a Excel";
            string path = "";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;

            }
            else
            {
                MessageBox.Show("Los datos no fueron exportados", @"Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            await Task.Run(() =>
            {
                try
                {
                    // list to datatable
                    var output = OutBoletin.GenerateReport(inputs, finalData);
                    //list to datatable
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Codistribuidor");
                    dt.Columns.Add("Operador");
                    // dt.Columns.Add("Estado")
                    dt.Columns.Add("Total Cantidad Vendido");
                    dt.Columns.Add("Total Monto Vendido");

                    dt.Columns.Add("Comision");
                    foreach (var item in output)
                    {
                        dt.Rows.Add(item.Codistribuidor, item.Operador, item.Totalcantidad, item.TotalVendido, item.Comision);
                    }


                    ExcelFn.ExportExcel(path, dt);
                    MessageBox.Show("Los datos han sido exportados correctamente.", @"Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al exportar los datos: " + ex.Message, @"Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            });
        }

        private async void btnLocal_Click(object sender, EventArgs e)
        {
            //indi
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Workbook|*.xlsx";
            saveFileDialog.Title = "Exportar a Excel";
            string path = "";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
            }
            else
            {
                MessageBox.Show("Los datos no fueron exportados", @"Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            await Task.Run(() =>
            {
                try
                {
                    var output = OutBoletin.IndividualGenerateReport(inputs, finalData);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Codistribuidor");
                    dt.Columns.Add("Operador");
                    dt.Columns.Add("Estado");
                    dt.Columns.Add("Numero");
                    dt.Columns.Add("Fecha");
                    dt.Columns.Add("Cantidad Vendido");
                    dt.Columns.Add("Monto Vendido");
                    dt.Columns.Add("Comision");
                    foreach (var item in output)
                    {
                        dt.Rows.Add(item.Codistribuidor, item.Operador, item.estado, item.numero, item.fecha, item.Totalcantidad, item.TotalVendido, item.Comision);
                    }


                    ExcelFn.ExportExcel(path, dt);
                    MessageBox.Show("Los datos han sido exportados correctamente.", @"Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al exportar los datos: " + ex.Message, @"Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            });
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }
    }
}
/*
 *  if (input.minVal < input.maxVal)
                MessageBox.Show("El valor minimo no puede ser mayor que el valor maximo", "Dato mal ingresado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (input.minVal > input.maxVal)
                MessageBox.Show("El valor maximo no puede ser menor que el valor minimo", "Dato mal ingresado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (input.minVal == input.maxVal)
                MessageBox.Show("El valor maximo no puede ser igual que el valor minimo", "Dato mal ingresado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (input.minVal <= inputs[e.RowIndex - 2].maxVal)
                MessageBox.Show("El valor minimo no puede ser menor o igual que el valor maximo anterior", "Dato mal ingresado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (input.maxVal <= inputs[e.RowIndex - 2].maxVal)
                MessageBox.Show("El valor maximo no puede ser menor o igual que el valor maximo anterior", "Dato mal ingresado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (input.minVal <= inputs[e.RowIndex - 2].minVal)
                MessageBox.Show("El valor minimo no puede ser menor o igual que el valor minimo anterior", "Dato mal ingresado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (input.maxVal <= inputs[e.RowIndex - 2].minVal)
                MessageBox.Show("El valor maximo no puede ser menor o igual que el valor minimo anterior", "Dato mal ingresado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (input.minVal == inputs[e.RowIndex - 2].minVal)
                MessageBox.Show("El valor minimo no puede ser igual que el valor minimo anterior", "Dato mal ingresado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (input.maxVal == inputs[e.RowIndex - 2].maxVal)
                MessageBox.Show("El valor maximo no puede ser igual que el valor maximo anterior", "Dato mal ingresado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (input.minVal == inputs[e.RowIndex - 2].maxVal)
                MessageBox.Show("El valor minimo no puede ser igual que el valor maximo anterior", "Dato mal ingresado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (input.maxVal == inputs[e.RowIndex - 2].minVal)
                MessageBox.Show("El valor maximo no puede ser igual que el valor minimo anterior", "Dato mal ingresado", MessageBoxButtons.OK, MessageBoxIcon.Error);
 */