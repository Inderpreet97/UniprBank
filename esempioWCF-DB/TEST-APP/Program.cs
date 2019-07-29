using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_APP
{
    class Program
    {
        static void Main(string[] args) {
            ServiceReferenceAccountPersona.ServiceAccountPersonaClient ServiceAccountPersonaClient = new ServiceReferenceAccountPersona.ServiceAccountPersonaClient();

            // -------------- TEST Login --------------
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool Login(string username, string password) =--\n");
            Console.ResetColor();
            // ---- Valori Input CORRETTI, Risultato Corretto True
            string username = "indi";
            string password = "prova";
            bool risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valori di input Corretti, risultato attesto True");
            Console.ResetColor();
            Console.WriteLine("\tUsername: {0}\tPassword: {1}", username, password);
            risultato = ServiceAccountPersonaClient.Login(username, password);
            Console.WriteLine("\tRisultato: {0}", risultato);
            Console.WriteLine("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Risultato Corretto False
            username = "indi";
            password = "rova"; // Manca la p nella password
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valori di input Sbagliati, risultato attesto False");
            Console.ResetColor();
            Console.WriteLine("\tUsername: {0}\tPassword: {1}", username, password);
            risultato = ServiceAccountPersonaClient.Login(username, password);
            Console.WriteLine("\tRisultato: {0}", risultato);
            Console.WriteLine("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // -------------- TEST GetPrivilegi --------------
        }
    }
}
