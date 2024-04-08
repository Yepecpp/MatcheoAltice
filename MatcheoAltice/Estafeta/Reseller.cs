using MatcheoAltice.entities;
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
    public partial class ResellerForm : Form
    {
        public List <IReseller> resellers = new List<IReseller>();
        public ResellerForm()
        {
            InitializeComponent();
            bool tableStatus = IReseller.CheckRellerTable();
            if (!tableStatus)
            {
                MessageBox.Show("No se pudo conectar a la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.Close();
            }

        }
        void LoadResellers()
        {
            resellers = IReseller.getResellers() ?? new List<IReseller>();
            dataGridView1.Invoke((MethodInvoker)delegate
            {
                dataGridView1.DataSource = resellers;
            });
            label2.Invoke((MethodInvoker)delegate
            {
                label2.Text = $"{resellers.Count} filas";
                label2.Visible = true;
            });
            
        }
        private async void Reseller_Load(object sender, EventArgs e)
        {
        await Task.Run(() => LoadResellers());
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
                        dataGridView1.DataSource = resellers;
                        return;
                    }
                    // Filtrar los datos del DataGridView
                    var filteredData =  IReseller.Filter( resellers, textBox1.Text, checkBox2.Checked);
                    dataGridView1.DataSource = filteredData;
                    
                }));
            });



        }
    private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;
            var filteredData = IReseller.Filter(resellers, textBox1.Text, checkBox2.Checked);
            dataGridView1.DataSource = filteredData;
        }
        private IReseller inputsToClass ()
        {
            IReseller reseller = new IReseller();
            reseller.Codigo= codigoBox.Text;
            reseller.Nombre = nombreBox.Text;
            reseller.Telefonos = telefonoBox.Text;
            reseller.Provincia = provinciaBox.Text;
            reseller.Ejecutivo = ejecutivoBox.Text;
            reseller.Supervisor = supervisorBox.Text;
            reseller.NumeroCuentaBanco = numCuent.Text;
            reseller.EntidadBancaria = entBancBox.Text;
            reseller.TitularCuenta = titCuenBox.Text;
            reseller.FechaCaptacion = fCapDP.Value;
            reseller.Direccion = direccionBox.Text;
         
            
            return reseller;
        }
        private void classToInputs(IReseller reseller)
        {
            codigoBox.Text = reseller.Codigo;
            nombreBox.Text = reseller.Nombre;
            telefonoBox.Text = reseller.Telefonos;
            provinciaBox.Text = reseller.Provincia;
            ejecutivoBox.Text = reseller.Ejecutivo;
            supervisorBox.Text = reseller.Supervisor;
            numCuent.Text = reseller.NumeroCuentaBanco;
            entBancBox.Text = reseller.EntidadBancaria;
            titCuenBox.Text = reseller.TitularCuenta;
            fCapDP.Value = reseller.FechaCaptacion;
            direccionBox.Text = reseller.Direccion;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        string currentCodigo = "";
        string currentStatus = "none";
        void cleanInputs()
        {
            codigoBox.Text = "";
            nombreBox.Text = "";
            telefonoBox.Text = "";
            provinciaBox.Text = "";
            ejecutivoBox.Text = "";
            supervisorBox.Text = "";
            numCuent.Text = "";
            entBancBox.Text = "";
            titCuenBox.Text = "";
            fCapDP.Value = DateTime.Now;
            direccionBox.Text = "";
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
            IReseller reseller = inputsToClass();
            if (IReseller.createReller(reseller) is null)
            {
                MessageBox.Show("No se pudo agregar el Vendedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                MessageBox.Show("Vendedor agregado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                resellers.Add(reseller);
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
           
            IReseller reseller = inputsToClass();
            reseller.Id = int.Parse(currentCodigo);
            if (IReseller.updateReseller(reseller) is null)
            {
                MessageBox.Show("No se pudo editar el vendedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Distribuidor editado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            resellers[resellers.FindIndex(x => x.Id == int.Parse(currentCodigo))] = reseller;
            dataGridView1.DataSource = resellers;
            label2.Text = $"{resellers.Count} filas";
            insertPanel.Visible = false;
            iconButton1.Enabled = true;
            btnExportar.Enabled = true;
            currentStatus = "none";
            cancelButton.Visible = false;
            insertPanel.Visible = false;


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //set the current codigo to the selected row
            currentCodigo = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            iconButton2.Enabled = true;
            insertPanel.Visible = true;
            classToInputs(resellers.Find(x => x.Id == int.Parse (currentCodigo)));
        }

        private async  void btnExportar_Click(object sender, EventArgs e)
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

        }

        private void insertPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
