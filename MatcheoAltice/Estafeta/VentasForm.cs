﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MatcheoAltice.ReporteFinal;

namespace MatcheoAltice.Estafeta
{
    public partial class VentasForm : Form
    {
        public VentasForm()
        {
            InitializeComponent();
        }
        List<Pagos> pagos = null;
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
                    var dataTable = ExcelFn.Convert(worksheet1, 9, 1);
                    return dataTable;

                }
            }
            MessageBox.Show("No se pudo cargar el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        Task SearchTask = new Task(() => { });
        CancellationTokenSource tokenSource2 = new CancellationTokenSource();

        private void textBox1_TextChanged_1(object sender, EventArgs e)
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
                        dataGridView1.DataSource = pagos;
                        return;
                    }
                    // Filtrar los datos del DataGridView
                    dataGridView1.DataSource = Pagos.FilterPagos(pagos, textBox1.Text);
                }));
            });

        }
       async private void btnExportar_Click(object sender, EventArgs e)
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
        private void btnPay_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable Table = Cargar_doc();
                if (Table == null)
                {
                    MessageBox.Show("No se pudo cargar el archivo");
                    return;
                }
                pagos = Pagos.Parse(Table);

                dataGridView1.DataSource = pagos;
                btnExportar.Enabled = true;
                panel4.Visible = true;
                btnLocal.Visible= true;
            }
            catch (Exception)
            {
                MessageBox.Show("El archivo selecionado no es valido o erroneo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            setDataGrid();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            setDataGrid();

        }
        private void setDataGrid()
        {
            double totalTarjeta = 0, totalEfectivo = 0, totalOtros = 0;
            List<Pagos> Data = dataGridView1.DataSource as List<Pagos>;
            if (Data != null)
            {
                foreach (var item in Data)
                {
                    totalTarjeta += double.Parse(item.FPtarjeta);
                    totalEfectivo += double.Parse(item.FPefectivo);
                    totalOtros += double.Parse(item.FPotras);


                }
            }
            label2.Text = $@"{Data.Count} filas";
            label4.Text = $@"Tarjeta: {DateUtils.parseDouble(totalTarjeta)}";
            label5.Text = $@"Efectivo: {DateUtils.parseDouble(totalEfectivo)}";
            label6.Text = $@"Otro: {DateUtils.parseDouble(totalOtros)}";


        }

        private void pagosBindingSource_CurrentChanged(object sender, EventArgs e)
        {

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
                    var output = Pagos.GenerateReport(dataGridView1.DataSource as List<Pagos>);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("UserLogin");
                    dt.Columns.Add("FPTarjeta");
                    dt.Columns.Add("FPEfectivo");
                    dt.Columns.Add("FPOtros");
                    dt.Columns.Add("Total");
                    foreach (var item in output)
                    {
                        dt.Rows.Add(item.Userlogin, item.FPtarjeta, item.FPefectivo, item.FPotras, item.FPtarjeta+item.FPefectivo+item.FPotras);
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
    }
}
