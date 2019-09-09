using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyConsole;
using System.Configuration;
using WCFClient.ServiceReference1;

namespace WCFClient
{
    static class Globals 
    {
        public static DateTime defaultDate = new DateTime(1900, 1, 1);
        public static Service1Client wcfClient = new Service1Client();
    }
    static class LoggedUser
    {
        public static string username = "default";
        public static string privilegi = "admin";
        public static string nome = "nomeDiProva";
        public static string idFiliale = "";
        public static List<ContoCorrente> contoCorrenti = new List<ContoCorrente>();
    }

    static class Funzioni 
    {
        public static void StampaPersona(Persona p) {
            Output.WriteLine("#########################################");
            Output.WriteLine("Tipo di utenza: " + p.privilegi);
            Output.WriteLine("\nUsername: " + p.username + "\nNome: " + p.nome + "\nCognome: " + p.cognome);
            Output.WriteLine("\nCodice Fiscale: " + p.codiceFiscale + "\nSesso: " + p.sesso + "\nData di nascita" + p.dataDiNascita);
            Output.WriteLine("\nCittà: " + p.citta + "\nProvincia: " + p.provincia + "\nStato: " + p.stato);
            Output.WriteLine("\nNumero di telefono: ", p.numeroDiTelefono);
        }

        public static void StampaFiliale(Filiale filiale) {
            Output.WriteLine("Nome filiale: " + filiale.nome);
            Output.WriteLine("Indirizzo: " + filiale.indirizzo);
            Output.WriteLine("CAP: " + filiale.CAP);
            Output.WriteLine("Città: " + filiale.citta);
            Output.WriteLine("Provincia: " + filiale.provincia);
            Output.WriteLine("Stato: " + filiale.stato);
            Output.WriteLine("Numero di telefono: " + filiale.numeroDiTelefono);
        }

        public static void StampaContoCorrente(ContoCorrente contoCorrente) {
            Console.Clear();
            Output.WriteLine("IBAN", contoCorrente.IBAN);
            Output.WriteLine("Saldo: ", contoCorrente.saldo);
            Output.WriteLine("Filiale di appartenza", Globals.wcfClient.GetNameFiliale(contoCorrente.idFiliale));
        }

        public static void StampaMovimento(Movimento movimento) {
            Console.Clear();

            Output.WriteLine("Id movimento: ", movimento.idMovimenti);
            Output.WriteLine("Tipo: ", movimento.tipo);
            Output.WriteLine("Importo: ", movimento.importo);
            Output.WriteLine("Data ora: ", movimento.dataOra);

            if (movimento.tipo == "Bonifico") {
                Output.WriteLine("Committente: ", movimento.IBANCommittente);
                Output.WriteLine("Beneficiario: ", movimento.IBANBeneficiario);
            }

        }

        public static void algoritmoModificaPersona(Persona persona) {
            
           
        }

        public static string digitaNuovoUsername() {

            /*Questa funzione viene richiamata ogni qualvolta bisogna registrare un NUOVO utente
            Una volta inserito, viene richiamata la funzione checkUsername() per controllare se l'username non è già stato utilizzato
            Se checkUsername() restituisce una persona vuota, allora l'username non è stato utilizzato e questa funzione restituisce il nuovo username.
            In caso contrario verrà chiesto di digitare nuovamente l'username
            */

            string username = Input.ReadString("Digitare l'username: ");
            while (Globals.wcfClient.CheckUsername(username).username != string.Empty) {
                //La persona restituita deve essere vuota (non esiste = username disponibile)
                Output.WriteLine("Username già utilizzato, riprovare con un altro\n");
                username = Input.ReadString("Digitare l'username: ");
            }
            return username;
        }

        public static string digitaUsername() {
            //Questa funzione viene chiamata ogni volta che occorre digitare l'username e lo controlla
            //Attenzione! Questa funzione non viene richiamata per registrare una nuova persona, per questo utilizzare digitaNuoboUsername()
            //Restituisce l'username come stringa

            string username = Input.ReadString("Digitare l'username: ");
            while(Globals.wcfClient.CheckUsername(username).username == string.Empty) {
                //Utente non trovato
                Output.WriteLine("Username non trovato, riprovare\n");
                Output.WriteLine("Digitare l'username");
            }
            return username;


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
            string temp;
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
                
                //Cognome
                temp = Input.ReadString("Cognome: ");
                if (!string.IsNullOrWhiteSpace(temp)) { cognome = temp; }
                
                //Data di nascita
                temp = Input.ReadString("Data di nascita: ");
                if (!string.IsNullOrWhiteSpace(temp)) { DateTime.TryParse(temp, out dataDiNascita); }
                
                //Codice fiscale
                temp = Input.ReadString("Codice fiscale: ");
                if (!string.IsNullOrWhiteSpace(temp)) { codiceFiscale = temp; }
                
                //Sesso
                temp = Input.ReadString("Sesso: ");
                if (!string.IsNullOrWhiteSpace(temp)) { sesso = temp; }
                
                //Indirizzo
                temp = Input.ReadString("Indirizzo: ");
                if (!string.IsNullOrWhiteSpace(temp)) { indirizzo = temp; }
                
                //Citta
                temp = Input.ReadString("Città: ");
                if (!string.IsNullOrWhiteSpace(temp)) { citta = temp; }
                
                //Provincia
                temp = Input.ReadString("Provincia: ");
                if (!string.IsNullOrWhiteSpace(temp)) { provincia = temp; }
                
                //Stato
                temp = Input.ReadString("Stato: ");
                if (!string.IsNullOrWhiteSpace(temp)) { stato = temp; }
                
                //Numero di telefono
                temp = Input.ReadString("Numero di telefono: ");
                if (!string.IsNullOrWhiteSpace(temp)) { numeroDiTelefono = temp; }
                
                //Filiale
                temp = Input.ReadString("Filiale: ");
                if (!string.IsNullOrWhiteSpace(temp)) { filiale = temp; }
                
                //CAP
                CAP = (int?)Input.ReadInt("CAP: ", 0, 99999);
            }

            //Persona persona = new Persona(username, privilegio, codiceFiscale, nome, cognome, dataDiNascita, sesso,
            //    indirizzo, CAP, citta, provincia, stato, numeroDiTelefono, filiale);
            Persona persona = new Persona();
            persona.username = username;
            persona.privilegi = privilegio;
            persona.codiceFiscale = codiceFiscale;
            persona.nome = nome;
            persona.cognome = cognome;
            persona.dataDiNascita = dataDiNascita;
            persona.sesso = sesso;
            persona.indirizzo = indirizzo;
            persona.CAP = CAP;
            persona.provincia = provincia;
            persona.stato = stato;
            persona.numeroDiTelefono = numeroDiTelefono;
            persona.filiale = filiale;

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

            risultato = Globals.wcfClient.AggiungiPersona(persona, password1);
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


            if (usernamePersona == string.Empty) { //Username vuoto
                usernamePersona = digitaUsername();
            }

            Persona persona = Globals.wcfClient.CheckUsername(usernamePersona);

            StampaPersona(persona);

            persona = new Persona();

            /*
                persona.username = string.Empty;
                persona.privilegi = string.Empty;
                persona.codiceFiscale = string.Empty;
                persona.nome = string.Empty;
                persona.cognome = string.Empty;
                persona.dataDiNascita = null;
                persona.sesso = string.Empty;
                persona.indirizzo = string.Empty;
                persona.CAP = null;
                persona.citta = string.Empty;
                persona.provincia = string.Empty;
                persona.stato = string.Empty;
                persona.numeroDiTelefono = string.Empty;
                persona.filiale = string.Empty;
             */

            //Lista delle properties dell' oggetto
            List<System.Reflection.PropertyInfo> personaProperties = persona.GetType().GetProperties().ToList();

            //Lista delle properties non modificabili
            List<string> BlackList = new List<string>() { "privilegi", "filiale" };

            string temp;

            //Itero tutte le properties dell'oggetto
            for (int index = 0; index < personaProperties.Count; index++) {

                //Se la property si può modificare (non è contenuta nella blacklist)
                if (!BlackList.Contains(personaProperties[index].Name)) {

                    temp = Input.ReadString("Nuovo " + personaProperties[index].Name);

                    if (!string.IsNullOrWhiteSpace(temp)) {
                        if (personaProperties[index].GetValue(persona).GetType() == typeof(int?)) { //Int
                            personaProperties[index].SetValue(persona, Convert.ToInt32(temp));
                        } else if (personaProperties[index].GetValue(persona).GetType() == typeof(DateTime?)) { //Date time
                            personaProperties[index].SetValue(persona, Convert.ToDateTime(temp));
                        } else if (personaProperties[index].GetValue(persona).GetType() == typeof(decimal)) {
                            personaProperties[index].SetValue(persona, Convert.ToDecimal(temp));
                        } else { //string
                            personaProperties[index].SetValue(persona, temp);
                        }
                    }
                }
            }

            bool risultato = Globals.wcfClient.ModificaPersona(usernamePersona, persona);

            if (risultato) { Output.WriteLine(persona.privilegi + " modificato correttamente"); } else { Output.WriteLine("Errore"); }

        }

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
                    errorString= (checkDati) ? string.Empty : "Username o Password non corretti!";
                }
            } while (!checkDati);

            LoggedUser.idFiliale = Globals.wcfClient.GetIdFilialeByUsername(LoggedUser.username);

            //Login eseguito correttamente
            Console.Clear();

            switch (Globals.wcfClient.GetPrivilegi(LoggedUser.username))
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
