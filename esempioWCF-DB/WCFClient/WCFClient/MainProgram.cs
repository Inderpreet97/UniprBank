using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyConsole;
using System.Configuration;

namespace WCFClient
{
    static class Globals 
    {
        public static DateTime defaultDate = new DateTime(1900, 1, 1);
    }
    static class LoggedUser
    {
        public static string username = "default";
        public static string privilegi = "admin";
        public static string nome = "nomeDiProva";
        public static List<ContoCorrente> contoCorrenti = new List<ContoCorrente>();
    }
    static class Funzioni 
    {
        public static void algoritmoModificaPersona(Persona persona) {
            
            //Lista delle properties dell' oggetto
            List<System.Reflection.PropertyInfo> personaProperties = persona.GetType().GetProperties().ToList();
            
            //Lista delle properties non modificabili
            List<string> BlackList = new List<string>() { "privilegi", "filiale" };

            string temp = string.Empty;
            
            //Itero tutte le properties dell'oggetto
            for (int index = 0; index < personaProperties.Count; index++) {

                //Se la property si può modificare (non è contenuta nella blacklist)
                if (!BlackList.Contains(personaProperties[index].Name)) {

                    temp = string.Empty;
                    temp = Input.ReadString("Nuovo " + personaProperties[index].Name);

                    if (!string.IsNullOrWhiteSpace(temp)) {
                        if (personaProperties[index].GetValue(persona).GetType() == typeof(int?)) { //Int
                            personaProperties[index].SetValue(persona, Convert.ToInt32(temp));
                        } else if (personaProperties[index].GetValue(persona).GetType() == typeof(DateTime?)) { //Date time
                            personaProperties[index].SetValue(persona, Convert.ToDateTime(temp));
                        } else if (personaProperties[index].GetValue(persona).GetType() == typeof(decimal)){
                            personaProperties[index].SetValue(persona, Convert.ToDecimal(temp));
                        } else { //string
                            personaProperties[index].SetValue(persona, temp);
                        }
                    } 
                }
            }
        }
        public static string digitaNuovoUsername() {

            /*Questa funzione viene richiamata ogni qualvolta bisogna registrare un NUOVO utente
            Una volta inserito, viene richiamata la funzione checkUsername() per controllare se l'username non è già stato utilizzato
            Se checkUsername() restituisce una persona vuota, allora l'username non è stato utilizzato e questa funzione restituisce il nuovo username.
            In caso contrario verrà chiesto di digitare nuovamente l'username
            */

            string username = Input.ReadString("Digitare l'username: ");
            while (Funzioni.checkUsername(username).username != string.Empty) {
                //La persona restituita deve essere vuota (non esiste = username disponibile)
                Output.WriteLine("Username già utilizzato, riprovare con un altro\n");
                username = Input.ReadString("Digitare l'username: ");
            }
            return username;
        }
        public static string digitaUsername() {
            //Questa funzione viene chiamata ogni volta che occorre digitare l'username e lo controlla
            //Attenzione! Questa funzione non viene richiamata per registrare una nuova persona, per questo utilizzare digitaNuoboUsername()

            string username = Input.ReadString("Digitare l'username: ");
            while(Funzioni.checkUsername(username).username == string.Empty) {
                //Utente non trovato
                Output.WriteLine("Username non trovato, riprovare\n");
                Output.WriteLine("Digitare l'username");
            }
            return username;


        }
        public static Persona checkUsername(string username) {
            //Restituisce una persona vuota se l'username non è presente, restituisce una persona popolata in caso contrario
            //Persona p = WCFCLient.CheckUsername(string username);
            Persona p = new Persona();
            return p;
        }
        public static bool checkEta(DateTime dataDiNascita) {
            //Controlla se l'utente è maggiorenne e se non ha più di limite max di età
            short limiteMaxEta = 100;
            if (DateTime.Compare(DateTime.Now, dataDiNascita) >= 18 && DateTime.Compare(DateTime.Now, dataDiNascita) < limiteMaxEta) { return true; }
            return false;
        }
        public static void aggiungiPersona(string privilegio) {

            string username = digitaNuovoUsername();

            string codiceFiscale= string.Empty;
            string nome = string.Empty;
            string cognome = string.Empty;
            string sesso = string.Empty;
            string indirizzo = string.Empty;
            string numeroDiTelefono = string.Empty;
            string filiale = string.Empty;
            string citta = string.Empty;
            string provincia = string.Empty;
            string stato = string.Empty;
            DateTime dataDiNascita = Globals.defaultDate;
            int? CAP = null;
            string temp = null;
            int? tempInt = null;

            //Tutti gli attributi devono essere valorizzati, altrimenti esegue il ciclo while
            while (codiceFiscale == string.Empty || nome == string.Empty || cognome == string.Empty ||
                sesso == string.Empty || indirizzo == string.Empty ||
                numeroDiTelefono == string.Empty || filiale == string.Empty ||
                citta == string.Empty || provincia == string.Empty || stato == string.Empty ||
                !CAP.HasValue || (!Funzioni.checkEta(dataDiNascita))){
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
                tempInt = (int?)Input.ReadInt("CAP: ", 0, 99999);
            }

            Persona persona = new Persona(username, privilegio, codiceFiscale, nome, cognome, dataDiNascita, sesso,
                indirizzo, CAP, citta, provincia, stato, numeroDiTelefono, filiale);

            bool risultato = false;

            //La password non viene registrata nella classe Persona
            string password1 = Input.ReadString("Password: ");
            string password2 = Input.ReadString("Conferma password: ");

            //Le password devono coincidere
            while(password1 != password2) {
                Output.WriteLine(ConsoleColor.Red,"Le password non coincidono, riprovare\n");
                password1 = Input.ReadString("Password: ");
                password2 = Input.ReadString("Conferma password: ");
            }
            Output.WriteLine("Le password coincidono...\n\n");

            //risultato = WCFCLient.AggiungiPersona(persona, password)
            if (risultato) { Output.WriteLine(privilegio + " aggiunto correttamente"); } else { Output.WriteLine("Errore"); }

        }
        public static void modificaPersona(string usernamePersona) {

            /*Questa funzione viene richiamata sia quando un impiegato o un direttore vogliono modificare un profilo di un cliente
            sia quando un cliente/impiegato/direttore vuole modificare il proprio profilo.
            (Con LoggedUser so con certezza chi richiama la funzione)
            Il parametro username se viene passato come stringa vuota, vuol dire che devo chiedere all'utente il profilo da modificare
            e tramite checkUsername controllo l'esistenza del profilo nel database,
            altrimenti se lo ho già, vuol dire che la funzione viene chiamata dal cliente/impiegato/direttore
            per modificare il proprio profilo*/

            bool risultato = false;
            Persona persona = new Persona();

            //persona.username = digitaUsername();
            //string currentUsername = persona.username;

            Console.Clear();
            persona.Stampa();

            Input.ReadString("PREMI INVIO PER NON MODIFICARE...");

            algoritmoModificaPersona(persona); //Occorre passare l'oggetto per riferimento o basta il valore?
            
            //WCFClient.ModificaPersona(currentUsername, p);

            if (risultato) { Output.WriteLine(persona.privilegi + " modificato correttamente"); } else { Output.WriteLine("Errore"); }

        }
        public static bool checkContoCorrente(int idContoCorrente) {
            bool risultato = false;
            //risultato = WCFClient.checkContoCorrente(idContoCorrente)
            return risultato;
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

        //Costruttori
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

        //Metodi
        public void StampaFiliale() {
            Output.WriteLine("Nome filiale: " + this.nome);
            Output.WriteLine("Indirizzo: " + this.indirizzo);
            Output.WriteLine("CAP: " + this.CAP);
            Output.WriteLine("Città: " + this.citta);
            Output.WriteLine("Provincia: " + this.provincia);
            Output.WriteLine("Stato: " + this.stato);
            Output.WriteLine("Numero di telefono: " + this.numeroDiTelefono);
            Output.WriteLine("Direttore: " + this.direttore);
        }

        //Attributi
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

        public string getNomeFiliale() { //Ottiene il nome della filiale con quell'ID Filiale
            //string nomeFiliale = WCFClient.getNomeFiliale(this.idFiliale);
            string nomeFiliale = string.Empty;
            return nomeFiliale;
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

        public void stampa() {
            Console.Clear();
            Output.WriteLine("IBAN", this.IBAN);
            Output.WriteLine("Saldo: ", this.saldo);
            Output.WriteLine("Filiale di appartenza", getNomeFiliale());
        }
    }

    public class Movimento {
        public Movimento() {
            this.idMovimento = null;
            this.IBANCommittente = string.Empty;
            this.tipo = string.Empty;
            this.importo = null;
            this.IBANBeneficiario = string.Empty;
            this.dataOra = null;
        }

        public Movimento(int? idMovimento, string IBANCommittente, string tipo, decimal? importo, string IBANBeneficiario, DateTime? dataOra) {
            this.idMovimento = idMovimento;
            this.IBANCommittente = IBANCommittente;
            this.tipo = tipo;
            this.importo = importo;
            this.IBANBeneficiario = IBANBeneficiario;
            this.dataOra = dataOra;
        }

        public void Stampa() {
            Console.Clear();

            Output.WriteLine("Id movimento: ", this.idMovimento);
            Output.WriteLine("Tipo: ", this.tipo);
            Output.WriteLine("Importo: ", this.importo);
            Output.WriteLine("Data ora: ", this.dataOra);
            if(this.tipo == "Bonifico" && importo > 0) { Output.WriteLine("Bonifico in entrata da: ", this.IBANCommittente); }
            if(this.tipo == "Bonifico" && importo < 0) { Output.WriteLine("Bonifico in uscita a: ", this.IBANBeneficiario); }

        }

        public int? idMovimento { get; set; }
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
