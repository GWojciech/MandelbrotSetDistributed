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

namespace MandelbrotServer
{
    public partial class Form1 : Form
    {
        int downX, downY;
        private MandelbrotCalcClient client;

        public Form1()
        {
            InitializeComponent();
            client = new MandelbrotCalcClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            pictureBox.Image = client.DrawMandelbrot(pictureBox.Width, pictureBox.Height);
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
        }

        //public Bitmap DrawMandelbrot()
        //{
        //    Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
        //    Parallel.For(0, pictureBox.Width, i =>
        //    {
        //        Parallel.For(0, pictureBox.Height, j =>
        //        {
        //            int result = mandelbrot.Calculate_mandelbrot(i, j);
        //            lock (bitmap)
        //            {
        //                if (result > 0 && result < 333)
        //                {
        //                    bitmap.SetPixel(i, j, Color.FromArgb(result & 255, 0, 0));

        //                }
        //                else if (result >= 333 && result < 666)
        //                {
        //                    bitmap.SetPixel(i, j, Color.FromArgb(0, result & 255, 0));

        //                }
        //                else if (result >= 666 && result < 999)
        //                    bitmap.SetPixel(i, j, Color.FromArgb(0, 0, result & 255));

        //                else
        //                {
        //                    bitmap.SetPixel(i, j, Color.FromArgb(0, 0, 0));

        //                }
        //                //bitmap.SetPixel(i, j, (Color) ColorTranslator.FromWin32(result));

        //            }
        //        });


        //    });
        //    return bitmap;
        //}

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
            //mandelbrot.SetNewScaleAttributes(Math.Min(downX, e.X), Math.Max(downX, e.X), Math.Min(downY, e.Y), Math.Max(downY, e.Y));
            //pictureBox.Image = DrawMandelbrot();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //mandelbrot = new Mandelbrot(pictureBox);
            //pictureBox.Image = DrawMandelbrot();
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
