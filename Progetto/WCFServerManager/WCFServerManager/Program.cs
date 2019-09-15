﻿using System;
using System.ServiceModel;

namespace WCFServerManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost svcHost = new ServiceHost(typeof(Service1));
                svcHost.Open();

                Console.WriteLine("Servizio WCF online, premere un tasto per interrompere...");

                Console.ReadLine();
                svcHost.Close();
                Console.WriteLine("Servizio WCF interrotto");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore: " + ex.ToString());
            }
        }
    }
}
