using MatcheoAltice.entities;
using NodaTime.TimeZones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatcheoAltice.Estafeta
{
    public partial class SimForm : Form
    {
        List <Sim> sims = new List<Sim>();
        public SimForm()
        {
            InitializeComponent();
            bool res =Sim.checkSimTable();
            if(!res)
            {
                MessageBox.Show("Error al crear tabla de SIM");
                this.Close();
            }

        }
        List<IReseller> resellers = new List<IReseller>();
        void LoadData()
        {
            sims = Sim.getSims();
            resellers = IReseller.getResellers();
            
            dataGridView1.Invoke(new Action(() =>
            {
                dataGridView1.DataSource = sims;
            }));
            label2.Invoke((MethodInvoker)delegate
            {
                label2.Text = $"{sims.Count} filas";
                label2.Visible = true;
            });
            comboBox1.Invoke((MethodInvoker)delegate
            {
                comboBox1.DataSource = resellers;
                comboBox1.DisplayMember = "Nombre";
                comboBox1.ValueMember = "Id";
            });

        }

        private void insertPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private Sim inputsToClass()
        {
            Sim  sim = new Sim((IReseller)comboBox1.SelectedItem);
            sim.Serial = serialBox.Text;
            sim.Plan = planBox.Text;
            sim.Serial = serialBox.Text;
            sim.FechaActivacion = fCapDP.Value;
            return  sim;

        } 
        private void classToInputs(Sim sim)
        {
            serialBox.Text = sim.Serial;
            planBox.Text = sim.Plan;
            fCapDP.Value = sim.FechaActivacion ?? DateTime.Now;
            comboBox1.SelectedValue = sim.RevendedorId;
            planBox.Text = sim.Plan;
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
            SearchTask = Task.Delay(200).ContinueWith(_ =>
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
                        dataGridView1.DataSource = sims;
                        return;
                    }
                    // Filtrar los datos del DataGridView
                    var filteredData = Sim.Filter(sims, textBox1.Text, checkBox2.Checked);
                    dataGridView1.DataSource = filteredData;

                }));
            });
        }

        private async void SimForm_Load(object sender, EventArgs e)
        {
            await Task.Run(() => LoadData());
        }
        string currentCodigo = "";
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            currentCodigo = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            iconButton2.Enabled = true;
            insertPanel.Visible = true;
            classToInputs(sims.Find(x => x.Id == int.Parse(currentCodigo)));
        }
        string currentStatus = "none";
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

        private void iconButton2_Click(object sender, EventArgs e)
        {
            //update the current reseller
            if (currentStatus == "none" && currentCodigo != "")
            {
                currentStatus = "editing";
                cancelButton.Visible = true;
                insertPanel.Visible = true;
                iconButton1.Enabled = false;
                btnExportar.Enabled = false;
                return;
            }
            else if (currentStatus != "editing")
            {
                MessageBox.Show("No se ha seleccionado un vendedor para editar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Sim sim = inputsToClass();
            sim.Id = int.Parse(currentCodigo);
            if (Sim.updateSim(sim) is null)
            {
                MessageBox.Show("No se pudo editar el vendedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Distribuidor editado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            sims[sims.FindIndex(x => x.Id == int.Parse(currentCodigo))] = sim;
            dataGridView1.DataSource = resellers;
            label2.Text = $"{resellers.Count} filas";
            insertPanel.Visible = false;
            iconButton1.Enabled = true;
            btnExportar.Enabled = true;
            currentStatus = "none";
            cancelButton.Visible = false;
            insertPanel.Visible = false;
        }
        void cleanInputs()
        {
            serialBox.Text = "";
            planBox.Text = "";
            fCapDP.Value = DateTime.Now;
            comboBox1.SelectedIndex = 0;

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (currentStatus == "none")
            {
                currentStatus = "adding";
                cancelButton.Visible = true;
                insertPanel.Visible = true;
                iconButton2.Enabled = false;
                btnExportar.Enabled = false;
                cleanInputs();
                return;
            }
            if (currentStatus != "adding")
            {
                return;
            }
            Sim sim = inputsToClass();
            if (Sim.createSim(sim) is null)
            {
                MessageBox.Show("No se pudo agregar el Sim", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Sim agregado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            sims.Add(sim);
            dataGridView1.DataSource = resellers;
            label2.Text = $"{resellers.Count} filas";
            insertPanel.Visible = false;
            iconButton2.Enabled = true;
            btnExportar.Enabled = true;
            currentStatus = "none";
            cancelButton.Visible = false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            currentStatus = "none";
            cancelButton.Visible = false;
            iconButton1.Enabled = true;
            insertPanel.Visible = false;
            iconButton2.Enabled = true;
            btnExportar.Enabled = true;
        }
    }
}
