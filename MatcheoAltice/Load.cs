using System;
using System.Threading;
using System.Windows.Forms;

namespace MatcheoAltice
{
    public partial class Load : Form
    {
        public Load()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel4.Width < 50) panel4.Width += 5;
            else if (panel4.Width < 75) panel4.Width += 10;
            else if (panel4.Width < 150) panel4.Width += 18;
            else if (panel4.Width < 260) panel4.Width += 25;
            else if (panel4.Width < 369) panel4.Width += 30;
            else if (panel4.Width < panel3.Width) panel4.Width += 40;

            if (panel4.Width >= panel3.Width)
            {
                timer1.Stop();
                Dashboard menu = new Dashboard();


                this.Hide();
                Thread.Sleep(200);
                menu.Show();

            }
        }

        private void Load_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
