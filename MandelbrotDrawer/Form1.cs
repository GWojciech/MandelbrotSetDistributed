using MandelbrotDrawer;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace MandelbrotServer
{
    public partial class Form1 : Form
    {
        int downX, downY;

        Mandelbrot mandelbrot;
        ConnectionManager connectionManager;
        // The frame images.
        private Image[] images;

        // The index of the current frame.
        private int FrameNum = 0;

        public Form1()
        {
            InitializeComponent();
            mandelbrot = new Mandelbrot(pictureBox.Width, pictureBox.Height);
            connectionManager = new ConnectionManager();
        }

        private void button_go_Click(object sender, EventArgs e)
        {
            showAnimationButton.Enabled = false;
            mandelbrot = new Mandelbrot(pictureBox.Width, pictureBox.Height);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            pictureBox.Image = mandelbrot.DrawMandelbrot(0);
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
            mandelbrot.CalculateScaleAttributes(Math.Min(downX, e.X), Math.Max(downX, e.X), Math.Min(downY, e.Y), Math.Max(downY, e.Y), Convert.ToInt32(numericUpDownFrames.Value));
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
            if (connectionManager.getNumberOfServers() == 0)
            {
                mandelbrot.DrawMandelbrot(1);
            }
            else
            {
                connectionManager.SetScales(mandelbrot.GetScales());
                connectionManager.SetNumberOfFramesPerServer(Convert.ToInt32(numericUpDownServers.Value));
                connectionManager.getImagesFromServers(pictureBox.Width, pictureBox.Height);
            }
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
            showAnimationButton.Enabled = true;
        }


        private void showAnimationButton_Click(object sender, EventArgs e)
        {
            images = new Bitmap[mandelbrot.GetScales().Count];
            string name = "bitmap";
            for (int i = 0; i < mandelbrot.GetScales().Count; i++)
            {
                name = "bitmap" + i.ToString() + ".Jpeg";
                Console.WriteLine(name);
                images[i] = Image.FromFile(name);
            }
            timer1.Enabled = !timer1.Enabled;
            if (timer1.Enabled)
                showAnimationButton.Text = "Stop";
            else
            {
                showAnimationButton.Text = "Start";
                pictureBox.Image = images[images.Length - 1];
                images = null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FrameNum = ++FrameNum % images.Length;
            pictureBox.Image = images[FrameNum];
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
