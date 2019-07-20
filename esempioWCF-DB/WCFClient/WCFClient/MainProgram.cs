using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyConsole;
using System.Configuration;

namespace WCFClient
{
    // Messaggio di prova 
    // Messaggio di prova 2
    static class LoggedUser // Questa è una classe static, non possiamo creare oggi di questa classe, 
    {                       // "esiste un unico oggetto" visibile in tutto il namespace WCFClient
        public static string username = "default";
        public static string privilegi = "admin";
        public static string nome;
    }

    //##################################### CLASSI TEMPORANEE ##############################################

    public class Persona 
    {
        public Persona() {
        }

        public Persona(string username, string privilegi, string codiceFiscale,
            string nome, string cognome, DateTime dataDiNascita, string sesso, string indirizzo, string CAP,
            string citta, string provincia, string stato, string numeroDiTelefono, string filiale) {
            this.username = username;
            this.privilegi = privilegi;
            this.codiceFiscale = codiceFiscale;
            this.nome = nome;
            this.cognome = cognome;
            this.dataDiNascita = dataDiNascita;
            this.sesso = sesso;
            this.indirizzo = indirizzo;
            this.CAP = CAP;
            this.citta = citta;
            this.provincia = provincia;
            this.stato = stato;
            this.numeroDiTelefono = numeroDiTelefono;
            this.filiale = filiale;
        }

        public void Stampa() {
            Output.WriteLine("#########################################");
            Output.WriteLine("Tipo di utenza: ", this.privilegi);
            Output.WriteLine("\nUsername: ", this.username + "\nNome: ", this.nome + "\nCognome: ", this.cognome);
            Output.WriteLine("\nCodice Fiscale: ", this.codiceFiscale + "\nSesso: ", this.sesso + "\nData di nascita", this.dataDiNascita);
            Output.WriteLine("\nCittà: ", this.citta + "\nProvincia: ", this.provincia + "\nStato: ", this.stato);
            Output.WriteLine("\nNumero di telefono: ", numeroDiTelefono);
        }

        public string username { get; set; }
        public string privilegi { get; set; }
        public string codiceFiscale { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public DateTime dataDiNascita { get; set; }
        public string sesso { get; set; }
        public string indirizzo { get; set; }
        public string CAP { get; set; }
        public string citta { get; set; }
        public string provincia { get; set; }
        public string stato { get; set; }
        public string numeroDiTelefono { get; set; }
        public string filiale { get; set; }
    }

    public class Filiale {
        public Filiale() {
        }

        public Filiale(string idFiliale, string nome, string indirizzo,
            int CAP, string citta, string provincia, string stato, string numeroDiTelefono, string direttore) {
            this.idFiliale = idFiliale;
            this.nome = nome;
            this.indirizzo = indirizzo;
            this.CAP = CAP;
            this.citta = citta;
            this.provincia = provincia;
            this.stato = stato;
            this.numeroDiTelefono = numeroDiTelefono;
            this.direttore = direttore;
        }

        public string idFiliale { get; set; }
        public string nome { get; set; }
        public string indirizzo { get; set; }
        public int CAP { get; set; }
        public string citta { get; set; }
        public string provincia { get; set; }
        public string stato { get; set; }
        public string numeroDiTelefono { get; set; }
        public string direttore { get; set; }
    }

    public class ContoCorrente {
        public ContoCorrente() {
        }

        public ContoCorrente(int idContoCorrente, string IBAN, string username, decimal saldo, string idFiliale) {
            this.idContoCorrente = idContoCorrente;
            this.IBAN = IBAN;
            this.username = username;
            this.saldo = saldo;
            this.idFiliale = idFiliale;
        }

        public int idContoCorrente { get; set; }
        public string IBAN { get; set; }
        public string username { get; set; }
        public decimal saldo { get; set; }
        public string idFiliale { get; set; }
    }

    public class Movimenti {
        public Movimenti() {
        }

        public Movimenti(int idMovimenti, string IBANCommittente, string tipo, decimal importo, string IBANBeneficiario, DateTime dataOra) {
            this.idMovimenti = idMovimenti;
            this.IBANCommittente = IBANCommittente;
            this.tipo = tipo;
            this.importo = importo;
            this.IBANBeneficiario = IBANBeneficiario;
            this.dataOra = dataOra;
        }

        public int idMovimenti { get; set; }
        public string IBANCommittente { get; set; }
        public string tipo { get; set; }
        public decimal importo { get; set; }
        public string IBANBeneficiario { get; set; }
        public DateTime dataOra { get; set; }
    }

    //################################################################################################


    class MainProgram
    {
        static void Main(string[] args) {

            /*ServiceReference1.Service1Client wcfClient = new ServiceReference1.Service1Client();

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

            //Login eseguito correttamente
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
            }*/
            DirettoreProgram program = new DirettoreProgram();
            program.Run();
        }
    }
}
