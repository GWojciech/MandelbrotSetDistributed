using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotDrawer
{
    class Mandelbrot
    {
        double yScale1 = 2.0, yScale2 = -1.0, xScale1 = 3.5, xScale2 = -2.5;
        private int height, width;

        public Mandelbrot(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        double scaleY0(int y0)
        {
            return yScale1 * (((double)y0) / (height - 1)) + yScale2;
        }

        double scaleX0(int x0)
        {
            return xScale1 * (((double)x0) / (width - 1)) + xScale2;
        }

        public int Calculate_mandelbrot(int xp, int yp)
        {
            double x, y, x2, y2;
            int iteration = 0;
            double x0 = scaleX0(xp);
            double y0 = scaleY0(yp);

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

        public void SetNewScaleAttributes(int x1, int x2, int y1, int y2)
        {
            double coordinateX1, coordinateX2, coordinateY1, coordinateY2;
            coordinateX1 = scaleX0(x1);
            coordinateX2 = scaleX0(x2);
            coordinateY1 = scaleY0(y1);
            coordinateY2 = scaleY0(y2);

            this.xScale2 = coordinateX1;
            this.xScale1 = coordinateX2 + Math.Abs(this.xScale2);

            this.yScale2 = coordinateY1;
            this.yScale1 = coordinateY2 + Math.Abs(this.yScale2);

        }
    }
}
