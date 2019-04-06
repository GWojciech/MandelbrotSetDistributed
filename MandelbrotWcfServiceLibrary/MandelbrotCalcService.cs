using MandelbrotDrawer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotWcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class MandelbrotCalcService : IMandelbrotCalc
    {
        private Mandelbrot mandelbrot;

        public Bitmap DrawMandelbrot(int width, int height)
        {
            Console.WriteLine("Maluję");
            mandelbrot = new Mandelbrot(width, height);
            Bitmap bitmap = new Bitmap(width, height);
            Parallel.For(0, width, i =>
            {
                Parallel.For(0, height, j =>
                {
                    int result = mandelbrot.Calculate_mandelbrot(i, j);
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

            return bitmap;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
