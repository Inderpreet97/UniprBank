using System;
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

                ServiceHost svcHostAccountPersona = new ServiceHost(typeof(ServiceAccountPersona));
                ServiceHost svcHostContoCorrente = new ServiceHost(typeof(ServiceContoCorrente));
                ServiceHost svcHostFiliale = new ServiceHost(typeof(ServiceFiliale));
                ServiceHost svcHostMovimenti = new ServiceHost(typeof(ServiceMovimenti));

                svcHostAccountPersona.Open();
                svcHostContoCorrente.Open();
                svcHostFiliale.Open();
                svcHostMovimenti.Open();

                Console.WriteLine("Servizio WCF online --- premere un tasto per interrompere...");

                Console.ReadLine();
                Console.WriteLine("Servizio WCF interrotto");

                svcHostAccountPersona.Close();
                svcHostContoCorrente.Close();
                svcHostFiliale.Close();
                svcHostMovimenti.Close();

            }
            catch (Exception ex) {
                Console.WriteLine("Errore: " + ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
