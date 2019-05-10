using MandelbrotDrawer;
using System;
using MandelbrotDrawer.MandelbrotCalcServiceReference;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;


namespace MandelbrotServer
{
    public partial class Form1 : Form
    {
        int downX, downY;

        Mandelbrot mandelbrot;
        ConnectionManager connectionManager;

        public Form1()
        {
            InitializeComponent();
            mandelbrot = new Mandelbrot(pictureBox.Width, pictureBox.Height);
            connectionManager = new ConnectionManager();
        }

        private void button_go_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (connectionManager.getNumberOfServers() > 0)
            {
                pictureBox.Image = connectionManager.getImageFromServer(pictureBox.Width, pictureBox.Height);
            }
            else
            {
                pictureBox.Image = mandelbrot.DrawMandelbrot(0);
            }

            watch.Stop();
            Console.WriteLine(watch.Elapsed);
        }


        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                downX = e.X;
                downY = e.Y;

                panel1.Width = 0;
                panel1.Height = 0;
                panel1.Visible = true;
            }

        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            panel1.Visible = false;
            mandelbrot.CalculateScaleAttributes(Math.Min(downX, e.X), Math.Max(downX, e.X), Math.Min(downY, e.Y), Math.Max(downY, e.Y), 10);
            pictureBox.Image = mandelbrot.DrawMandelbrot(0);

        }

        private void button_server_Click(object sender, EventArgs e)
        {
            connectionManager.SetServers();
            server_info_textBox.Text = "Connected: " + connectionManager.getNumberOfServers();
        }

        private void server_info_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void animation_button_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            mandelbrot.DrawMandelbrot(1);
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                panel1.Width = Math.Abs(downX - e.X);
                panel1.Height = Math.Abs(downY - e.Y);
                panel1.Left = pictureBox.Left + Math.Min(downX, e.X);
                panel1.Top = pictureBox.Top + Math.Min(downY, e.Y);
            }
        }
    }
}
