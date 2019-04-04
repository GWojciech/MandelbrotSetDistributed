using System;
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

        private Graphics graphics;


        public Form1()
        {
            InitializeComponent();
        }

        double scale_y0(int y0)
        {
            return 2.0 * (((double)y0) / (pictureBox.Height - 1)) - 1.0;
        }

        double scale_x0(int x0)
        {
            return 3.5 * (((double)x0) / (pictureBox.Width - 1)) - 2.5;
        }

        int Calculate_mandelbrot(int xp, int yp)
        {
            double x, y, x2, y2;
            int iteration = 0;
            double x0 = scale_x0(xp);
            double y0 = scale_y0(yp);
            x = y = x2 = y2 = 0.0;
            while (x2 + y2 <= 4.0 && iteration < 1000)
            {
                double tmp = x2 - y2 + x0;
                y = 2.0 * x * y + y0;
                x = tmp;
                iteration++;
                x2 = x * x;
                y2 = y * y;
            }
            return iteration;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            pictureBox.Image = DrawMandelbrot();
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
            //graphics = graphics.fromimage(picturebox.image);
            //int result;
            //for (int i = 0; i < picturebox.height; i++)
            //{
            //    for (int j = 0; j < picturebox.width; j++)
            //    {
            //        result = calculate_mandelbrot(j, i);
            //        graphics.drawrectangle(new pen((color)colortranslator.fromole(result & 255)), j, i, 1, 1);

            //    }
            //}
        }

        public Bitmap DrawMandelbrot()
        {
            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            //int result;
            Parallel.For(0, pictureBox.Width, i =>
            {
                //Parallel.For(0, pictureBox.Height, j =>
                //{
                for (int j = 0; j < pictureBox.Height; j++) {
                    int result = Calculate_mandelbrot(i, j);
                    lock (bitmap)
                    {
                        //bitmap.SetPixel(i, j, (Color)ColorTranslator.FromOle(result & 255));
                        if (result > 0 && result < 333)
                        {
                            bitmap.SetPixel(i, j, Color.FromArgb(result & 255, 0, 0));

                        }
                        else if (result >= 333 && result < 666)
                        {
                            bitmap.SetPixel(i, j, Color.FromArgb(0, result & 255, 0));

                        }
                        else if (result >= 666 && result < 999)
                            bitmap.SetPixel(i, j, Color.FromArgb(0, 0, result & 255));

                        else
                        {
                            bitmap.SetPixel(i, j, Color.FromArgb(0, 0, 0));

                        }
                    }

                }
                //});
            });
            return bitmap;
        }

    }
}
