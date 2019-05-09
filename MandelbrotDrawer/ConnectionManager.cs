using MandelbrotDrawer.MandelbrotCalcServiceReference;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotDrawer
{
    class ConnectionManager
    {
        List<IMandelbrotCalc> Servers;

        public ConnectionManager()
        {
            Servers = new List<IMandelbrotCalc>();
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


        public void SetServers()
        {
            IMandelbrotCalc potentialServer;
            String name;
            for (int i = 1; i <= 6; i++)
            {
                name = "server" + i.ToString();
                ChannelFactory<IMandelbrotCalc> chF = new ChannelFactory<IMandelbrotCalc>(name);
                Console.WriteLine(name + " : " + chF.State);
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

        public int getNumberOfServers()
        {
            if (Servers != null)
            {
                return Servers.Count();
            }
            return 0;
        }

        public Bitmap getImageFromServer(int width, int height)
        {
            if (Servers != null)
            {
                Bitmap bitmap = Servers[0].DrawMandelbrot(width, height);
                return bitmap;
            }
            return null;
        }
    }
}
