using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotDrawer
{
    class Mandelbrot
    {
        private int height, width;

        public Mandelbrot(int width, int height)
        {
            this.width = width;
            this.height = height;
        }



        double ScaleY0(int y0, double[] scale)
        {
            return scale[2] * (((double)y0) / (height - 1)) + scale[3];
        }


        double ScaleX0(int x0, double[] scale)
        {
            return scale[0] * (((double)x0) / (width - 1)) + scale[1];
        }



        public int Calculate_mandelbrot(int xp, int yp, double[] scale)
        {
            double x, y, x2, y2;
            int iteration = 0;
            double x0 = ScaleX0(xp, scale);
            double y0 = ScaleY0(yp, scale);

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


    }
}
