using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MatcheoAltice.ReporteFinal;

namespace MatcheoAltice
{
    public partial class ArchivoAltice : Form
    {
        private List<Altice> AlticeDoc = null; // Variable para almacenar los datos del archivo de Excel

        public ArchivoAltice()
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
            string filePath = openFileDialog1.FileName;
            using (ExcelPackage package1 = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet1 = package1.Workbook.Worksheets[0];
                AlticeDoc = Altice.Parse(ExcelToDataTableConverter.Convert(worksheet1));
                dataGridView1.DataSource = AlticeDoc;
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
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;

                try
                {
                    worksheet.Name = "Datos";

                    int columnIndex = 0;
                    // fill the header row
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        columnIndex++;
                        worksheet.Cells[1, columnIndex] = column.HeaderText;
                    }
                    // fill the actual content
                    int rowIndex = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        rowIndex++;
                        columnIndex = 0;
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            columnIndex++;
                            worksheet.Cells[rowIndex + 1, columnIndex] = row.Cells[columnIndex - 1].Value;
                        }
                    }
                    // save the application
                    workbook.SaveAs(path);
                    MessageBox.Show("Los datos han sido exportados correctamente.", @"Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al exportar los datos: " + ex.Message, @"Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    excel.Quit();
                    workbook = null;
                    excel = null;
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
            if (dataGridView1.DataSource == null) return;
            var searchText = textBox1.Text.ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                dataGridView1.DataSource = AlticeDoc;
                return;
            }

            ///Nombre Usuario	DN Numero	Fecha Activacion	Sim	Ordenes	Total Dias Recargas	Total Monto Recargas

            var filteredData = from x in AlticeDoc
                               where x.Nombre.ToLower().Contains(searchText) ||
                                     x.DNumb.ToLower().Contains(searchText) ||
                                     x.FechaActivacion.ToString().ToLower().Contains(searchText) ||
                                     x.Sim.ToLower().Contains(searchText) ||
                                     x.Ordenes.ToLower().Contains(searchText)


                               select x;
            dataGridView1.DataSource = filteredData.ToList();



        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            label2.Text = $@"{dataGridView1.RowCount} filas";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

