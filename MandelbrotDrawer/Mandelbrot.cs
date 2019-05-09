using MandelbrotDrawer.MandelbrotCalcServiceReference;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MandelbrotDrawer
{
    class Mandelbrot
    {

        int Width;
        int Height;

        double yScale1 = 2.0, yScale2 = -1.0, xScale1 = 3.5, xScale2 = -2.5;

        public Mandelbrot(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        double ScaleY0(int y0)
        {
            return yScale1 * (((double)y0) / (Height - 1)) + yScale2;
        }


        double ScaleX0(int x0)
        {
            return xScale1 * (((double)x0) / (Width - 1)) + xScale2;
        }

        public void SetNewScaleAttributes(int x1, int x2, int y1, int y2)
        {
            double coordinateX1, coordinateX2, coordinateY1, coordinateY2;
            coordinateX1 = ScaleX0(x1);
            coordinateX2 = ScaleX0(x2);
            coordinateY1 = ScaleY0(y1);
            coordinateY2 = ScaleY0(y2);

            this.xScale2 = coordinateX1;
            this.xScale1 = coordinateX2 + Math.Abs(this.xScale2);

            this.yScale2 = coordinateY1;
            this.yScale1 = coordinateY2 + Math.Abs(this.yScale2);

        }

        public int Calculate_mandelbrot(int xp, int yp)
        {
            double x, y, x2, y2;
            int iteration = 0;
            double x0 = ScaleX0(xp);
            double y0 = ScaleY0(yp);

            x = y = x2 = y2 = 0.0;
            while (x2 + y2 <= 4.0 && iteration < 2000)
            {
                double tmp = x2 - y2 + x0;
                y = 2.0 * x * y + y0;
                x = tmp;
                iteration++;
                x2 = x * x;
                y2 = y * y;
            }
            return iteration == 2000 ? 0 : iteration;
        }


        public Bitmap DrawMandelbrot()
        {
            Bitmap bitmap = new Bitmap(Width, Height);
            Parallel.For(0, Width, i =>
            {
                Parallel.For(0, Height, j =>
                {
                    int result = Calculate_mandelbrot(i, j);
                    lock (bitmap)
                    {
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
                });


            });
            bitmap.Save("bitmap.png", ImageFormat.Png);
            return bitmap;
        }

    }

}
