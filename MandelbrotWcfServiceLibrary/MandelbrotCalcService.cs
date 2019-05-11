using MandelbrotDrawer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        public List<MemoryStream> DrawMandelbrot(int width, int height, List<List<double>> Scales)
        {
            Console.WriteLine("I am drawing");
            mandelbrot = new Mandelbrot(width, height);
            List<MemoryStream> images = new List<MemoryStream>();
            Bitmap bitmap;
            double[] scale;
            MemoryStream stream = new MemoryStream(); 

            for (int count = 0; count < Scales.Count; count++)
            {
                bitmap = new Bitmap(width, height);
                scale = Scales[count].ToArray();
                Console.WriteLine("{0}, {1}", scale[0], scale[1]);
                //Parallel.For(0, width, i =>
                //{
                //    Parallel.For(0, height, j =>
                //    {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {



                        int result = mandelbrot.Calculate_mandelbrot(i, j, scale);
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
                        //    });


                        //});
                    }
                }
                stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Jpeg);
                images.Add(stream);
                    }

            return images;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public void InformAboutConnection()
        {
            Console.WriteLine("Client connected to server");
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
