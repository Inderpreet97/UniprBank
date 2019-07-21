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

    static class Funzioni 
    {
        public static string digitaUsername() {
            string username = Input.ReadString("Digitare l'username: ");
            while (!Funzioni.checkUsername(username)) {
                Output.WriteLine("Username già utilizzato, riprovare con un altro\n");
                username = Input.ReadString("Digitare l'username: ");
            }
            return username;
        }
        public static bool checkUsername(string username) {
            //return WCFCLient.checkUsername(string username);
            return false;
        }
        public static void aggiungiPersona(string privilegio) {

            DateTime defaultDate = new DateTime(1900, 1, 1);

            string username = digitaUsername();

            string codiceFiscale = string.Empty;
            string nome = string.Empty;
            string cognome = string.Empty;
            string sesso = string.Empty;
            string indirizzo = string.Empty;
            string numeroDiTelefono = string.Empty;
            string filiale = string.Empty;
            string citta = string.Empty;
            string provincia = string.Empty;
            string stato = string.Empty;
            DateTime dataDiNascita = defaultDate;
            int CAP = 0;
            string temp = null;

            while (codiceFiscale == string.Empty && nome == string.Empty && cognome == string.Empty &&
                sesso == string.Empty && indirizzo == string.Empty &&
                numeroDiTelefono == string.Empty && filiale == string.Empty &&
                citta == string.Empty && provincia == string.Empty && stato == string.Empty &&
                CAP == 0 && DateTime.Compare(dataDiNascita, defaultDate) <= 0){
                //Nome
                temp = Input.ReadString("Nome: ");
                if (!string.IsNullOrWhiteSpace(temp)) { nome = temp; }
                temp = string.Empty;

                //Cognome
                temp = Input.ReadString("Cognome: ");
                if (!string.IsNullOrWhiteSpace(temp)) { cognome = temp; }
                temp = string.Empty;

                //Data di nascita
                temp = Input.ReadString("Data di nascita: ");
                if (!string.IsNullOrWhiteSpace(temp)) { DateTime.TryParse(temp, out dataDiNascita); }
                temp = string.Empty;

                //Codice fiscale
                temp = Input.ReadString("Codice fiscale: ");
                if (!string.IsNullOrWhiteSpace(temp)) { codiceFiscale = temp; }
                temp = string.Empty;

                //Sesso
                temp = Input.ReadString("Sesso: ");
                if (!string.IsNullOrWhiteSpace(temp)) { sesso = temp; }
                temp = string.Empty;

                //Indirizzo
                temp = Input.ReadString("Indirizzo: ");
                if (!string.IsNullOrWhiteSpace(temp)) { indirizzo = temp; }
                temp = string.Empty;

                //Citta
                temp = Input.ReadString("Città: ");
                if (!string.IsNullOrWhiteSpace(temp)) { citta = temp; }
                temp = string.Empty;

                //Provincia
                temp = Input.ReadString("Provincia: ");
                if (!string.IsNullOrWhiteSpace(temp)) { provincia = temp; }
                temp = string.Empty;

                //Stato
                temp = Input.ReadString("Stato: ");
                if (!string.IsNullOrWhiteSpace(temp)) { stato = temp; }
                temp = string.Empty;

                //Numero di telefono
                temp = Input.ReadString("Numero di telefono: ");
                if (!string.IsNullOrWhiteSpace(temp)) { numeroDiTelefono = temp; }
                temp = string.Empty;

                //Filiale
                temp = Input.ReadString("Filiale: ");
                if (!string.IsNullOrWhiteSpace(temp)) { filiale = temp; }
                temp = string.Empty;

                //CAP
                temp = Input.ReadString("CAP: ");
                if (!string.IsNullOrWhiteSpace(temp)) {
                    int.TryParse(temp, out CAP); }
                temp = string.Empty;
            }

            Persona persona = new Persona(username, privilegio, codiceFiscale, nome, cognome, dataDiNascita, sesso,
                indirizzo, CAP, citta, provincia, stato, numeroDiTelefono, filiale);

            bool risultato = false;

            string password1 = Input.ReadString("Password: ");
            string password2 = Input.ReadString("Conferma password: ");


            while(password1 != password2) {
                Input.ReadString("Le password non coincidono, riprovare\n");
                password1 = Input.ReadString("Password: ");
                password2 = Input.ReadString("Conferma password: ");
            }

            Output.WriteLine("Le password coincidono...\n\n");

            //risultato = WCFCLient.AggiungiPersona(persona, password)
            if (risultato) { Output.WriteLine(privilegio + " aggiunto correttamente"); } else { Output.WriteLine("Errore"); }

        }
        
    }

    //##################################### CLASSI TEMPORANEE ##############################################

    public class Persona 
    {
        public Persona() {
            this.username = string.Empty;
            this.privilegi = string.Empty;
            this.codiceFiscale = string.Empty;
            this.nome = string.Empty;
            this.cognome = string.Empty;
            this.dataDiNascita = null;
            this.sesso = string.Empty;
            this.indirizzo = string.Empty;
            this.CAP = null;
            this.citta = string.Empty;
            this.provincia = string.Empty;
            this.stato = string.Empty;
            this.numeroDiTelefono = string.Empty;
            this.filiale = string.Empty;
        }

        public Persona(string username, string privilegi, string codiceFiscale,
            string nome, string cognome, DateTime? dataDiNascita, string sesso, string indirizzo, int? CAP,
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
        public DateTime? dataDiNascita { get; set; }
        public string sesso { get; set; }
        public string indirizzo { get; set; }
        public int? CAP { get; set; }
        public string citta { get; set; }
        public string provincia { get; set; }
        public string stato { get; set; }
        public string numeroDiTelefono { get; set; }
        public string filiale { get; set; }
    }

    public class Filiale {
        public Filiale() {
            this.idFiliale = string.Empty;
            this.direttore = string.Empty;
            this.nome = string.Empty;
            this.indirizzo = string.Empty;
            this.CAP = null;
            this.citta = string.Empty;
            this.provincia = string.Empty;
            this.stato = string.Empty;
            this.numeroDiTelefono = string.Empty;
        }

        public Filiale(string idFiliale, string nome, string indirizzo,
            int? CAP, string citta, string provincia, string stato, string numeroDiTelefono, string direttore) {
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
        public int? CAP { get; set; }
        public string citta { get; set; }
        public string provincia { get; set; }
        public string stato { get; set; }
        public string numeroDiTelefono { get; set; }
        public string direttore { get; set; }
    }

    public class ContoCorrente {
        public ContoCorrente() {
            this.idContoCorrente = null;
            this.IBAN = string.Empty;
            this.username = string.Empty;
            this.saldo = null;
            this.idFiliale = string.Empty;
        }

        public ContoCorrente(int? idContoCorrente, string IBAN, string username, decimal? saldo, string idFiliale) {
            this.idContoCorrente = idContoCorrente;
            this.IBAN = IBAN;
            this.username = username;
            this.saldo = saldo;
            this.idFiliale = idFiliale;
        }

        public int? idContoCorrente { get; set; }
        public string IBAN { get; set; }
        public string username { get; set; }
        public decimal? saldo { get; set; }
        public string idFiliale { get; set; }
    }

    public class Movimento {
        public Movimento() {
            this.idMovimenti = null;
            this.IBANCommittente = string.Empty;
            this.tipo = string.Empty;
            this.importo = null;
            this.IBANBeneficiario = string.Empty;
            this.dataOra = null;
        }

        public Movimento(int? idMovimenti, string IBANCommittente, string tipo, decimal? importo, string IBANBeneficiario, DateTime? dataOra) {
            this.idMovimenti = idMovimenti;
            this.IBANCommittente = IBANCommittente;
            this.tipo = tipo;
            this.importo = importo;
            this.IBANBeneficiario = IBANBeneficiario;
            this.dataOra = dataOra;
        }

        public int? idMovimenti { get; set; }
        public string IBANCommittente { get; set; }
        public string tipo { get; set; }
        public decimal? importo { get; set; }
        public string IBANBeneficiario { get; set; }
        public DateTime? dataOra { get; set; }
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
