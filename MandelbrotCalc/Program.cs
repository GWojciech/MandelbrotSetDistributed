using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MandelbrotWcfServiceLibrary;
using System.ServiceModel.Description;


namespace CalculatorHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Tworzymy adres gdzie pod którym bedzie dostepna usługa
            Uri baseAddress = new Uri("http://localhost:8000/MandelbrotCalcService/");

            // Tworzymy obiekt klasy CalculatorService
            ServiceHost selfHost = new ServiceHost(typeof(MandelbrotCalcService), baseAddress);

            try
            {
                // Dodajemy Endopoint usługi
                selfHost.AddServiceEndpoint(typeof(IMandelbrotCalc), new BasicHttpBinding(), "MandelbrotCalcService");

                // Umozliwiamy wymiane metadanych
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                // Startujemy serwis
                selfHost.Open();
                Console.WriteLine("Serwis działa....");
                Console.WriteLine("Nacisnij <ENTER> by zakonczyc.");
                Console.WriteLine();
                Console.ReadLine();

                // zamykamy serwis
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("Przechwyciłem wyjatek: {0}", ce.Message);
                selfHost.Abort();
                Console.ReadLine();
            }
        }
    }
}