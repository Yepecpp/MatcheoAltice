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
            Application.Exit();
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
            //inputBoletinBindingSource.DataSource = inputs;
        }
    }
}
