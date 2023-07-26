using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace MatcheoAltice
{
    public partial class ReporteFinal : Form
    {
        List<Altice> Alticedoc = null;
        List<Base> Basedoc = null;
        List<Final> DB = new List<Final>();
        public ReporteFinal()
        {
            InitializeComponent();
        }
        public static class ExcelToDataTableConverter
        {
            public static DataTable Convert(ExcelWorksheet worksheet)
            {
                DataTable dt = new DataTable(worksheet.Name);

                int totalColumns = worksheet.Dimension.End.Column;

                // Leer los datos de las celdas y agregar columnas al DataTable
                for (int col = 1; col <= totalColumns; col++)
                {
                    var cellValue = worksheet.Cells[1, col].Text;
                    dt.Columns.Add(cellValue);
                }

                // Leer los datos de las filas y agregarlas al DataTable
                int totalRows = worksheet.Dimension.End.Row;
                for (int row = 2; row <= totalRows; row++)
                {
                    var newRow = dt.NewRow();
                    for (int col = 1; col <= totalColumns; col++)
                    {
                        var cellValue = worksheet.Cells[row, col].Text;
                        newRow[col - 1] = cellValue;
                    }
                    dt.Rows.Add(newRow);
                }

                return dt;
            }
        }
        private void ReporteFinal_Load(object sender, EventArgs e)
        {

        }
        private void iconButton1_Click(object sender, EventArgs e)
        {

            DataTable Table = Cargar_doc();
            if (Table == null)
            {
                MessageBox.Show("No se pudo cargar el archivo");
                return;
            }
            Basedoc = Base.Parse(Table);
            UnirTablas();

        }

        private void UnirTablas()
        {
            if (Alticedoc == null || Basedoc == null)
            {
                return;
            }

            DB = Final.Parse(Alticedoc, Basedoc);
            dataGridView1.DataSource = DB;

            btnExportar.Enabled = true;

        }
        private DataTable Cargar_doc()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
            openFileDialog1.Title = "Seleccione el primer archivo de Excel";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath1 = openFileDialog1.FileName;

                // Mostrar el cuadro de diálogo de selección de archivo para el segundo archivo de Excel

                // Cargar el primer archivo de Excel
                using (ExcelPackage package1 = new ExcelPackage(new FileInfo(filePath1)))
                {
                    ExcelWorksheet worksheet1 = package1.Workbook.Worksheets[0];
                    var dataTable = ExcelToDataTableConverter.Convert(worksheet1);
                    return dataTable;

                }
            }
            return null;
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {

        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                dataGridView1.DataSource = DB;
                return;
            }
            // Filtrar los datos del DataGridView
            dataGridView1.DataSource = Final.Filter(DB, textBox1.Text);
        }


        private void btnAltice_Click(object sender, EventArgs e)
        {
            DataTable Table = Cargar_doc();
            if (Table == null)
            {
                MessageBox.Show("No se pudo cargar el archivo");
                return;
            }

            Alticedoc = Altice.Parse(Table);
            UnirTablas();
        }

        private async void btnExportar_Click(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

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
