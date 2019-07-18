﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFServerDB {
    class Program {
        static void Main(string[] args) {

            try {
                var t = ConfigurationManager.AppSettings["connectionString"];

                ServiceHost svcHost = new ServiceHost(typeof(Service1));
                svcHost.Open();
                Console.WriteLine("Servizio WCF online --- premere un tasto per interrompere...");

                Console.ReadLine();
                Console.WriteLine("Servizio WCF interrotto");

            } catch (Exception ex) {
                Console.WriteLine("Errore: " + ex.ToString());

            }
        }
    }
}
