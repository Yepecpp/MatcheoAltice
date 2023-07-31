using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;
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

                // MessageBox.Show("No se pudo cargar el archivo");
                return;
            try
            {
                Basedoc = Base.Parse(Table);
            }
            catch (Exception)
            {
                MessageBox.Show("El archivo selecionado no es valido o erroneo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
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
            GenButton.Enabled = true;

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
            MessageBox.Show("No se pudo cargar el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;




        }

        private void iconButton5_Click(object sender, EventArgs e)
        {

        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Task SearchTask = new Task(() => { });
        CancellationTokenSource tokenSource2 = new CancellationTokenSource();
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (SearchTask.Status == TaskStatus.Running)
            {
                // Cancelar la tarea anterior
                tokenSource2.Cancel();


            }
            SearchTask = Task.Delay(700).ContinueWith(_ =>
            {
                Invoke(new Action(() =>
                {
                    if (tokenSource2.IsCancellationRequested)
                    {
                        tokenSource2 = new CancellationTokenSource();
                        return;
                    }
                    if (string.IsNullOrEmpty(textBox1.Text))
                    {
                        dataGridView1.DataSource = DB;
                        return;
                    }
                    // Filtrar los datos del DataGridView
                    dataGridView1.DataSource = Final.Filter(DB, textBox1.Text);
                }));
            });



        }


        private void btnAltice_Click(object sender, EventArgs e)
        {
            DataTable Table = Cargar_doc();
            if (Table == null)
            {
                MessageBox.Show("No se pudo cargar el archivo");
                return;
            }
            try
            {
                Alticedoc = Altice.Parse(Table);
            }
            catch (Exception)
            {
                MessageBox.Show("El archivo selecionado no es valido o erroneo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // sum the values in list db of .totalMonto

            setDataGrid();
        }
        private void setDataGrid()
        {
            double sum = 0;
            (dataGridView1.DataSource as List<Final>).ForEach(x => sum += double.Parse(x.TotalMontoRecargas));
            //format in currency like 1,000.00$
            string sumString = sum.ToString("C", CultureInfo.CurrentCulture);

            label2.Text = $@"{dataGridView1.RowCount} filas";
            label3.Text = $@"Total Monto de Recargas: {sumString}";
            // dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            setDataGrid();
        }

        private void GenButton_Click(object sender, EventArgs e)
        {
            new BoletinScreen(DB).Show();
        }
    }
}
