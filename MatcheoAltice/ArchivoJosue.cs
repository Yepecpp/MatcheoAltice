using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MatcheoAltice.ReporteFinal;

namespace MatcheoAltice
{
    public partial class ArchivoJosue : Form
    {
        private List<Base> BaseDoc = new List<Base>(); // Variable para almacenar los datos originales del archivo de Excel

        public ArchivoJosue()
        {
            InitializeComponent();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Ningun archivo fue cargado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                string filePath = openFileDialog1.FileName;
                using (ExcelPackage package1 = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorksheet worksheet1 = package1.Workbook.Worksheets[0];
                    BaseDoc = Base.Parse(ExcelToDataTableConverter.Convert(worksheet1));
                    dataGridView1.DataSource = BaseDoc;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("El archivo selecionado no es valido o erroneo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Workbook|*.xlsx";
            saveFileDialog.Title = "Exportar a Excel";
            string path = "";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
                label1.Visible = true;
                label1.Text = "Exportando...";
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
                    ExcelFn.ExportExcel(path, dataGridView1);
                    MessageBox.Show("Los datos han sido exportados correctamente.", @"Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al exportar los datos: " + ex.Message, @"Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            });
            label1.Text = "Exportado!";
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var searchValue = textBox1.Text.ToLower();
            if (string.IsNullOrEmpty(searchValue))
            {
                dataGridView1.DataSource = BaseDoc;
                return;
            }

            // Fechas	Cedula	Sim	Numero	Vendedor	Operador
            var query = from c in BaseDoc
                        where c.Fecha.ToString().Contains(searchValue) ||
                              c.Sim.ToLower().Contains(searchValue) ||
                        c.Numero.ToLower().Contains(searchValue) ||
                        c.Vendedor.ToLower().Contains(searchValue) ||
                        c.Operador.ToLower().Contains(searchValue)
                        select c;
            dataGridView1.DataSource = query.ToList();



        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            label2.Text = $@"{dataGridView1.RowCount} filas";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
