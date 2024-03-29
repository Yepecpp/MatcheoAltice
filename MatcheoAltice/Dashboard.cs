﻿using MatcheoAltice.Estafeta;
using System;
using System.Windows.Forms;

namespace MatcheoAltice
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            panelCForm.Controls.Add(childForm);
            panelCForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }



        private void iconButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm = null;
                timer1.Enabled = true;
                this.Name = "Dashboard";
                label2.Text = DateTime.Now.ToString("D");
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            openChildForm(new EstafetaForm());
            this.Name = "Reporte de Estafeta";
            timer1.Enabled = false;
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            openChildForm(new UsersCRUD());
            this.Name = "Usuarios";
            timer1.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("hh:mm:ss");

        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            label2.Text = DateTime.Now.ToString("D",
                System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")
                );
            if (Properties.Settings.Default.Rol.ToLower() != "admin")
            {
                iconButton10.Visible = false;
            }
            Userlb.Text = Properties.Settings.Default.Usuario;
        }
        public bool reauth = false;
        private void iconButton11_Click(object sender, EventArgs e)
        {
            reauth = true;
            this.Close();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {

            openChildForm(new EstafetaForm());
            timer1.Enabled = false;
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            openChildForm(new ReporteFinal());
            timer1.Enabled = false;
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

        private void panelCForm_SizeChanged(object sender, EventArgs e)
        {
            this.panel4.Location = new System.Drawing.Point((this.panelCForm.Width / 2) - (this.panel4.Width / 2), (int)Math.Round((this.panelCForm.Height / 2.2)) - (this.panel4.Height / 2));
        }
    }
}
