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
    internal class Mandelbrot
    {

        int Width;
        int Height;

        List<double[]> Scales;

        public Mandelbrot(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            Scales = new List<double[]>();
            Scales.Add(new double[] { 3.5d, -2.5d, 2.0d, -1.0d });
        }

        public List<double[]> GetScales()
        {
            return Scales;
        }

        double ScaleY0(int y0, double[] scale)
        {
            return scale[2] * (((double)y0) / (Height - 1)) + scale[3];
        }


        double ScaleX0(int x0, double[] scale)
        {
            return scale[0] * (((double)x0) / (Width - 1)) + scale[1];
        }

        public void CalculateScaleAttributes(int x1, int x2, int y1, int y2, int numberOfFrames)
        {
            double[] scale = Scales[Scales.Count - 1], scaleTmp;

            int deltaX1 = x1 / numberOfFrames;
            int deltaX2 = (Width - x2) / numberOfFrames;
            int deltaY1 = y1 / numberOfFrames;
            int deltaY2 = (Height - y2) / numberOfFrames;

            int xScreen1 = 0, xScreen2 = Width, yScreen1 = 0, yScreen2 = Height;
            for (int i = 0; i < numberOfFrames; i++)
            {
                scaleTmp = scale;
                xScreen1 += deltaX1;
                xScreen2 -= deltaX2;
                yScreen1 += deltaY1;
                yScreen2 -= deltaY2;
                Scales.Add(CalculateOneScaleAttribute(xScreen1, xScreen2, yScreen1, yScreen2, scaleTmp));
            }
        }

        public double[] CalculateOneScaleAttribute(int x1, int x2, int y1, int y2, double[] scale)
        {
            double[] scaleTmp = new double[4];
            double coordinateX1, coordinateX2, coordinateY1, coordinateY2;
            coordinateX1 = ScaleX0(x1, scale);
            coordinateX2 = ScaleX0(x2, scale);
            coordinateY1 = ScaleY0(y1, scale);
            coordinateY2 = ScaleY0(y2, scale);

            scaleTmp[1] = coordinateX1;
            scaleTmp[0] = coordinateX2 + Math.Abs(scaleTmp[1]);

            scaleTmp[3] = coordinateY1;
            scaleTmp[2] = coordinateY2 + Math.Abs(scaleTmp[3]);

            return scaleTmp;

        }

        public int Calculate_mandelbrot(int xp, int yp, double[] scale, int numberOfIterations)
        {
            double x, y, x2, y2;
            int iteration = 0;
            double x0 = ScaleX0(xp, scale);
            double y0 = ScaleY0(yp, scale);

            x = y = x2 = y2 = 0.0;
            while (x2 + y2 <= 4.0 && iteration < numberOfIterations)
            {
                double tmp = x2 - y2 + x0;
                y = 2.0 * x * y + y0;
                x = tmp;
                iteration++;
                x2 = x * x;
                y2 = y * y;
            }
            return iteration == numberOfIterations ? 0 : iteration;
        }


        public Bitmap DrawMandelbrot(int variant, int numberOfIterations)
        {
            double[] scale;
            int startIteration;
            if(variant == 0)
            {
                startIteration = Scales.Count-1;
            }
            else
            {
                startIteration = 0;
            }
            Bitmap bitmap = new Bitmap(Width, Height);

            for (int count = startIteration; count < Scales.Count; count++)
            {
                bitmap = new Bitmap(Width, Height);
                scale = Scales[count];
                Parallel.For(0, Width, i =>
                {
                    Parallel.For(0, Height, j =>
                    {
                        int result = Calculate_mandelbrot(i, j, scale, numberOfIterations);
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
                try
                {
                    bitmap.Save("bitmap" + count.ToString() + ".Jpeg", ImageFormat.Jpeg);
                }catch(Exception ex)
                {
                    Console.Write(ex);
                }
            }
            return bitmap;
        }
    }

}
