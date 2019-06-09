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
            List<TimeSpan> timeInClient = new List<TimeSpan>();
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
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    bitmaps = Servers[number].DrawMandelbrot(width, height, scalesTmp, Iterations);
                    watch.Stop();
                    timeInClient.Add(watch.Elapsed);
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
                    TimeSpan timeTotal = TimeSpan.Zero;
                    Console.WriteLine("Results in client about Server" + number);
                    timeInClient.ForEach(item => {
                        Console.WriteLine("From client about server" + number+ ", " + item);
                        timeTotal += item;
                    });
                    Console.WriteLine("TOTAL Results in client about Server" + number + ", " + timeTotal);

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
                TimeSpan time;
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
                    time = TimeSpan.Zero;
                    Threads[i].Join();
                    Console.WriteLine("Results from server" + i );
                    Servers[i].GetTimes().ForEach(item => {
                        Console.WriteLine("From Server" + i +", " + item + " ");
                        time += item;
                    });
                    Console.WriteLine("Total results from server" + i + ", " + time);
                }

            }
        }


    }
}
