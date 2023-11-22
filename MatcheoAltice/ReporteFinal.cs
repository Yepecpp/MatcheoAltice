using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NodaTime.Text;
using NodaTime;
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
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
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
            //find the oldest date in the db, they are not sorted by date, Final.Fecha


            dataGridView1.DataSource = DB;
            btnExportar.Enabled = true;
            GenButton.Visible = true;
            GenButton.Enabled = true;

            //date manipulation

            var firstDate = DB.FirstOrDefault(
                x => x.Fecha is null == false
                );
            if (firstDate == null) return;
            DateTime oldest = (DateTime)firstDate.Fecha;
            // find the newest date in the db
            DateTime newest = (DateTime)firstDate.Fecha;
            //set the date range
            foreach (var final in DB)
            {
                if (final.Fecha == null) continue;
                var Fecha = (DateTime)final.Fecha;
                if (Fecha == DateTime.MinValue) continue;

                if (Fecha < oldest)
                {
                    oldest = Fecha;
                }

                if (Fecha > newest)
                {
                    newest = Fecha;
                }
            }
            dateTimePicker1.MaxDate = newest;
            dateTimePicker1.MinDate = oldest;
            dateTimePicker2.MaxDate = newest;
            dateTimePicker2.MinDate = oldest;
            dateTimePicker1.Value = oldest;
            dateTimePicker2.Value = newest;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;

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
                    var dataTable = ExcelFn.Convert(worksheet1);
                    return dataTable;

                }
            }
            MessageBox.Show("No se pudo cargar el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {

        }
        Task SearchTask = new Task(() => { });
        CancellationTokenSource tokenSource2 = new CancellationTokenSource();
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;
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
                    dataGridView1.DataSource = checkBox1.Checked ? Final.Filter(DB, textBox1.Text, checkBox2.Checked).Where(x => x.Fecha is null ? false : checkDate((DateTime)x.Fecha)).ToList() : Final.Filter(DB, textBox1.Text, checkBox2.Checked);
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


        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // sum the values in list db of .totalMonto

            setDataGrid();
        }
        private void setDataGrid()
        {
            double sum = 0;
            (dataGridView1.DataSource as List<Final>).ForEach(x => sum += double.Parse(x.TotalMontoRecargas));

            label2.Text = $@"{dataGridView1.RowCount} filas";
            label3.Text = $@"Total Monto de Recargas: {DateUtils.parseDouble(sum)}";
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            setDataPerDate();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            setDataPerDate();

        }
        bool checkDate(DateTime x)
        {

            // Convert DateTime to LocalDate using NodaTime
            LocalDate localDate = LocalDateTime.FromDateTime(x).Date;



            // Check if any of the patterns can successfully parse the date

            // Convert DateTimePicker values to LocalDate using NodaTime
            LocalDate startDate = LocalDateTime.FromDateTime(dateTimePicker1.Value).Date;
            LocalDate endDate = LocalDateTime.FromDateTime(dateTimePicker2.Value).Date;

            return localDate >= startDate.Minus(Period.FromDays(1)) && localDate <= endDate.PlusDays(1);

        }


        private void setDataPerDate()
        {
            if (!checkBox1.Checked) return;
            //the format is dd/mm/yyyy
            if (textBox1.Text == "" && !(textBox1.Text is null))
            {
                dataGridView1.DataSource = DB
     .Where(x => !(x.Fecha is null) && checkDate((DateTime)x.Fecha))
     .ToList();
            }
            dataGridView1.DataSource = Final.Filter(DB, textBox1.Text, checkBox2.Checked).Where(x => !(x.Fecha is null) && checkDate((DateTime)x.Fecha))
     .ToList();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panel5.Visible = checkBox1.Checked;
            dataGridView1.DataSource = checkBox1.Checked ? Final.Filter(DB, textBox1.Text, checkBox2.Checked).Where(x => !(x.Fecha is null) && checkDate((DateTime)x.Fecha)).ToList() : Final.Filter(DB, textBox1.Text, checkBox2.Checked);

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Text = checkBox2.Checked ? "No Contiene busqueda" : "Contiene busqueda";
            dataGridView1.DataSource = checkBox1.Checked ? Final.Filter(DB, textBox1.Text, checkBox2.Checked).Where(x => x.Fecha is null ? false : checkDate((DateTime)x.Fecha)).ToList() : Final.Filter(DB, textBox1.Text, checkBox2.Checked);
        }
    }
}
