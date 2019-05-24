using MandelbrotDrawer.MandelbrotCalcServiceReference;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MandelbrotDrawer
{
    class ConnectionManager
    {
        private List<IMandelbrotCalc> Servers;
        private byte[] StateOfFrame;
        private Thread[] Threads;
        private List<double[]> Scales;
        int numberOfFramesPerServer;
        private int Iterations;

        public ConnectionManager()
        {
            SetServers();
        }


        public List<IMandelbrotCalc> getServers()
        {

            if (Servers.Count != 0)
            {
                return Servers;
            }
            else
            {
                return null;
            }
        }

        public void SetIterations(int iterations)
        {
            Iterations = iterations;
        }

        public void SetServers()
        {
            Servers = new List<IMandelbrotCalc>();
            IMandelbrotCalc potentialServer;
            string name;
            for (int i = 1; i <= 6; i++)
            {
                name = "server" + i.ToString();
                ChannelFactory<IMandelbrotCalc> chF = new ChannelFactory<IMandelbrotCalc>(name);
                try
                {
                    potentialServer = chF.CreateChannel();
                    potentialServer.InformAboutConnection();
                    Servers.Add(potentialServer);
                }
                catch (System.ServiceModel.EndpointNotFoundException)
                {
                    continue;
                }
            }
        }

        public void SetScales(List<double[]> scales)
        {
            this.Scales = scales;
            StateOfFrame = new byte[Scales.Count];
            //for(int i = 0; i <Scales.Count; i+=numberOfFrames)
            //{
            //    StateOfFrame[i] = 1;
            //}
        }

        public void SetNumberOfFramesPerServer(int numberOfFramesPerServer)
        {
            this.numberOfFramesPerServer = numberOfFramesPerServer;
        }

        private void ConnectWithServerThread(int number, int width, int height)
        {
            List <List<double>> scalesTmp = new List<List<double>>();
            List<MemoryStream> bitmaps = new List<MemoryStream>();
            List<int> frames;
            for (; ; )
            {
                frames = new List<int>();
                scalesTmp = new List<List<double>>();
                lock (StateOfFrame) {
                    for (int counter = 0; counter < Scales.Count; counter++ )
                    {
                        if (StateOfFrame[counter] == 0 && scalesTmp.Count < numberOfFramesPerServer)
                        {
                            scalesTmp.Add(Scales[counter].ToList());
                            StateOfFrame[counter] = 1;
                            frames.Add(counter);
                        }
                    }
                }
                if (scalesTmp.Count > 0)
                {
                    bitmaps = Servers[number].DrawMandelbrot(width, height, scalesTmp, Iterations);
                    Image image;
                    int numberOfFrame = 0;
                    foreach (MemoryStream bitmap in bitmaps)
                    {
                        image = Image.FromStream(bitmap);
                        image.Save("bitmap" + frames[numberOfFrame] + ".Jpeg", ImageFormat.Jpeg);
                        numberOfFrame++;

                    }
                }
                else
                {
                    break;
                }
                    
            }
        }

        public int getNumberOfServers()
        {
            if (Servers != null)
            {
                return Servers.Count();
            }
            return 0;
        }

        public void getImagesFromServers(int width, int height)
        {
            if (Servers != null)
            {
                Threads = new Thread[Servers.Count];
                for(int i=0; i<Servers.Count; i++)
                {
                    int serverNr = i;
                    Threads[serverNr] = new Thread(delegate () { ConnectWithServerThread(serverNr, width, height); })
                    {
                        IsBackground = true
                    };
                    Threads[serverNr].Start();
                }

                for(int i=0; i<Servers.Count; i++)
                {
                    Threads[i].Join();
                }

            }
        }


    }
}
