using System;
using System.ServiceModel;


namespace WCFServerDB {

    static class Globals {
        public static bool debugMode = true;
    }

    class Program {
        static void Main(string[] args) {

            try {
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
