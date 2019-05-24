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
        private Image[] images;

        private int FrameNum = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_go_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = !timer1.Enabled;
                showAnimationButton.Text = "Start";
                showAnimationButton.Enabled = false;
            }
            images = null;
            mandelbrot = new Mandelbrot(pictureBox.Width, pictureBox.Height);
            connectionManager = new ConnectionManager();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            pictureBox.Image = mandelbrot.DrawMandelbrot(0, Convert.ToInt32(numericUpDownIterations.Value));
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
            mandelbrot.CalculateScaleAttributes(Math.Min(downX, e.X), Math.Max(downX, e.X), Math.Min(downY, e.Y), Math.Max(downY, e.Y), Convert.ToInt32(numericUpDownFrames.Value)+1);
            pictureBox.Image = mandelbrot.DrawMandelbrot(0, Convert.ToInt32(numericUpDownIterations.Value));

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
            int framesPerServer = Convert.ToInt32(numericUpDownServers.Value);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (connectionManager.getNumberOfServers() == 0 || framesPerServer == 0)
            {
                mandelbrot.DrawMandelbrot(1, Convert.ToInt32(numericUpDownIterations.Value));
            }
            else
            {
                connectionManager.SetScales(mandelbrot.GetScales());
                connectionManager.SetNumberOfFramesPerServer(framesPerServer);
                connectionManager.SetIterations(Convert.ToInt32(numericUpDownIterations.Value));
                connectionManager.getImagesFromServers(Convert.ToInt32(numericUpDownResolutionWidth.Value), Convert.ToInt32(numericUpDownResolutionHeight.Value));
            }
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
            showAnimationButton.Enabled = true;
        }


        private void showAnimationButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if (timer1.Enabled)
            {
                showAnimationButton.Text = "Stop";
                images = new Bitmap[mandelbrot.GetScales().Count+1];
                string name = "bitmap";
                for (int i = 0; i < mandelbrot.GetScales().Count; i++)
                {
                    name = "bitmap" + i.ToString() + ".Jpeg";
                    images[i] = Image.FromFile(name);
                }
                images[images.Length - 1] = pictureBox.Image;
            }
            else
            {
                showAnimationButton.Text = "Start";
                pictureBox.Image = images[images.Length - 1];
                images = null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FrameNum = ++FrameNum % (images.Length-1);
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
