using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyConsole;
using System.Configuration;
using WCFClient.ServiceReference1;
using ConsoleTables;

namespace WCFClient
{
    static class Globals 
    {
        public static DateTime defaultDate = new DateTime(1900, 1, 1);
        public static Service1Client wcfClient = new Service1Client();
        //public static Service2Client wcfClient = new Service2Client();

    }
    static class LoggedUser
    {
        public static string username = "default";
        public static string privilegi = "admin";
        public static string nome = "nomeDiProva";
        public static string idFiliale = "";
        public static List<ContoCorrente> contoCorrenti = new List<ContoCorrente>();
    }

    class MainProgram
    {
        static void Main(string[] args) {

            string logo = ConfigurationManager.AppSettings["logo"].Replace("\\n", "\n");

            //Login
            string password, errorString= string.Empty;
            bool checkDati;

            do {

                Console.Clear();

                Output.WriteLine(ConsoleColor.Yellow, logo.ToString());   // Output è una classe astratta di EasyConsole
                Output.WriteLine(ConsoleColor.Cyan, "------------== LOGIN ==------------");

                Output.WriteLine(ConsoleColor.DarkRed, errorString);

                LoggedUser.username = Input.ReadString("Username: ");  // Input è una classe astratta di EasyConsole
                password = Input.ReadString("Password: ");  // Input offre diversi metodi tra cui ReadString


                if (string.IsNullOrWhiteSpace(LoggedUser.username) || string.IsNullOrWhiteSpace(password))
                {
                    checkDati = false;
                    errorString= "Username o Password non inseriti!";
                }
                else
                {
                    checkDati = Globals.wcfClient.Login(LoggedUser.username, password);
                    errorString = (checkDati) ? string.Empty : "Username o Password non corretti!";
                }
            } while (!checkDati);

            LoggedUser.idFiliale = Globals.wcfClient.GetIdFilialeByUsername(LoggedUser.username);

            //Login eseguito correttamente
            Console.Clear();

            switch (Globals.wcfClient.GetPrivilegi(LoggedUser.username).ToLower())
            {
                case "admin":
                    LoggedUser.privilegi = "admin";
                    new DirettoreProgram().Run();
                    break;
                case "impiegato":
                    LoggedUser.privilegi = "impiegato";
                    new ImpiegatoProgram().Run();
                    break;
                case "cliente":
                    LoggedUser.privilegi = "cliente";
                    LoggedUser.contoCorrenti = Globals.wcfClient.GetListaContoCorrente(LoggedUser.username);
                    new ClienteProgram().Run();
                    break;
                default:
                    errorString = "\nSi è verificato un errore, riavviare il programma...";
                    Output.WriteLine(ConsoleColor.DarkRed, errorString);
                    Console.ReadLine();
                    break;
            }
        }
    }
}
