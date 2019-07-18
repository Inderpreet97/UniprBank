using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyConsole;
using System.Configuration;

namespace WCFClient
{
    static class LoggedUser // Questa è una classe static, non possiamo creare oggi di questa classe, 
    {                       // "esiste un unico oggetto" visibile in tutto il namespace WCFClient
        public static string username;
        public static string privilegi;
        public static string nome;
    }

    class MainProgram
    {
        static void Main(string[] args) {

            ServiceReference1.Service1Client wcfClient = new ServiceReference1.Service1Client();

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
                    checkDati = wcfClient.Login(LoggedUser.username, password);
                    errorString= (checkDati) ? string.Empty : "Username o Password non corretti!";
                }
            } while (!checkDati);

            Console.Clear();

            switch (wcfClient.GetPrivilegi(LoggedUser.username))
            {
                case 1:
                    LoggedUser.privilegi = "admin";
                    new DirettoreProgram().Run();
                    break;
                case 2:
                    LoggedUser.privilegi = "impiegato";
                    new ImpiegatoProgram().Run();
                    break;
                case 3:
                    LoggedUser.privilegi = "cliente";
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
