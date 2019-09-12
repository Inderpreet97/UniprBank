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

    static class Funzioni 
    {
        //Questa classe fornisce dei metodi globali che possono essere chiamati da tutta l'applicazione Client a fine di ottimizzazione del codice

        //Persona/Account
        public static void StampaPersona(Persona p) {
            Output.WriteLine("\n\t>> Dati Persona <<\n");
            Output.WriteLine("Tipo di utenza: " + p.privilegi);
            Output.WriteLine("\nUsername: " + p.username + "\nNome: " + p.nome + "\nCognome: " + p.cognome);
            Output.WriteLine("\nCodice Fiscale: " + p.codiceFiscale + "\nSesso: " + p.sesso + "\nData di nascita: " + p.dataDiNascita.ToString().Remove(10, 9));
            Output.WriteLine("\nCittà: " + p.citta + "\nProvincia: " + p.provincia + "\nStato: " + p.stato);
            Output.WriteLine("\nNumero di telefono: " + p.numeroDiTelefono);
        }
        public static void aggiungiPersona(string privilegio) {

            string username = digitaNuovoUsername();

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
            DateTime dataDiNascita = Globals.defaultDate;
            int? CAP = null;
            string temp;

            //Tutti gli attributi devono essere valorizzati, altrimenti esegue il ciclo while
            while (codiceFiscale == string.Empty || nome == string.Empty || cognome == string.Empty ||
                sesso == string.Empty || indirizzo == string.Empty ||
                numeroDiTelefono == string.Empty || filiale == string.Empty ||
                citta == string.Empty || provincia == string.Empty || stato == string.Empty ||
                !CAP.HasValue || (!Funzioni.checkEta(dataDiNascita))) {

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
            persona.citta = citta;
            persona.provincia = provincia;
            persona.stato = stato;
            persona.numeroDiTelefono = numeroDiTelefono;
            persona.filiale = filiale;

            bool risultato;

            //La password non viene registrata nella classe Persona
            string password1 = Input.ReadString("Password: ");
            string password2 = Input.ReadString("Conferma password: ");

            //Le password devono coincidere
            while (password1 != password2) {
                Output.WriteLine(ConsoleColor.Red, "Le password non coincidono, riprovare\n");
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


            //Lista delle properties dell' oggetto
            List<System.Reflection.PropertyInfo> personaProperties = persona.GetType().GetProperties().ToList();

            //Lista delle properties non modificabili
            List<string> BlackList = new List<string>() { "privilegi", "filiale", "ExtensionData" };

            string temp;

            //Itero tutte le properties dell'oggetto
            for (int index = 0; index < personaProperties.Count; index++) {

                //Se la property si può modificare (non è contenuta nella blacklist)
                if (!BlackList.Contains(personaProperties[index].Name)) {

                    temp = Input.ReadString("Nuovo " + personaProperties[index].Name + ": ");

                    if (!string.IsNullOrWhiteSpace(temp)) {
                        if (personaProperties[index].PropertyType == typeof(int?)) {
                            //Int
                            personaProperties[index].SetValue(persona, Convert.ToInt32(temp));

                        } else if (personaProperties[index].PropertyType == typeof(DateTime?)) {
                            //Date time
                            personaProperties[index].SetValue(persona, Convert.ToDateTime(temp));

                        } else if (personaProperties[index].PropertyType == typeof(decimal?)) {
                            //decimal
                            personaProperties[index].SetValue(persona, Convert.ToDecimal(temp));

                        } else {
                            //string
                            personaProperties[index].SetValue(persona, temp);
                        }
                    }
                }
            }

            bool risultato = Globals.wcfClient.ModificaPersona(usernamePersona, persona);

            if (risultato) { Output.WriteLine(persona.privilegi + " modificato correttamente"); } else { Output.WriteLine("Errore"); }

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
            while (Globals.wcfClient.CheckUsername(username).username == string.Empty) {
                //Utente non trovato
                Output.WriteLine("Username non trovato, riprovare\n");
                username = Input.ReadString("Digitare l'username: ");
            }

            return username;

        }
        public static bool checkEta(DateTime dataDiNascita) {
            //Controlla se l'utente è maggiorenne e se non ha più di limite max di età

            DateTime zeroTime = new DateTime(1, 1, 1);
            var eta = (zeroTime + (DateTime.Now - dataDiNascita)).Year - 1;

            if (eta >= 18 && eta < 100) { return true; }
            return false;
        }

        //Filiale
        public static void StampaFiliale(Filiale filiale) {
            Output.WriteLine("Nome filiale: " + filiale.nome);
            Output.WriteLine("Indirizzo: " + filiale.indirizzo);
            Output.WriteLine("CAP: " + filiale.CAP);
            Output.WriteLine("Città: " + filiale.citta);
            Output.WriteLine("Provincia: " + filiale.provincia);
            Output.WriteLine("Stato: " + filiale.stato);
            Output.WriteLine("Numero di telefono: " + filiale.numeroDiTelefono);
        }

        //Conto corrente
        public static void StampaContoCorrente(ContoCorrente contoCorrente) {
            Console.Clear();
            Output.WriteLine("IBAN", contoCorrente.IBAN);
            Output.WriteLine("Saldo: ", contoCorrente.saldo);
            Output.WriteLine("Filiale di appartenza", Globals.wcfClient.GetNameFiliale(contoCorrente.idFiliale));
        }
        public static UInt64 scegliIdContoCorrente(List<ContoCorrente> listaContiUser) {

            //Questa funzione restituisce l'id del conto corrente

            int sceltaConto = 0;        //Indice numero di conto da scegliere
            int index;                  //Contatore indici conto correnti

            do {
                var tableConti = new ConsoleTable(" # ", "Numero di conto", "IBAN", "Saldo");

                index = 1;

                listaContiUser.ForEach(conto => {
                    tableConti.AddRow(index, conto.idContoCorrente, conto.IBAN, conto.saldo);
                    index++;
                });

                tableConti.Write();
                sceltaConto = Convert.ToInt32(Input.ReadString("Digitare l'indice del conto corrente: "));
                Output.WriteLine("Indice scelto {0}", sceltaConto);

            } while (sceltaConto < 1 || sceltaConto >= index);

            Output.WriteLine("ContoCorrente selezionato: {0}", listaContiUser[sceltaConto - 1].idContoCorrente);

            return Convert.ToUInt64(listaContiUser[sceltaConto - 1].idContoCorrente);
        }
        public static List<ContoCorrente> getListaContiByPrivilege() {

            List<ContoCorrente> listaContiUser = new List<ContoCorrente>(); //Lista contenenti i conti di un utente

            //-----RICHIESTA USERNAME-------
            //---L'impiegato o il direttore devono inserire l'username dell'account della quale si vuole ottenere la lista movimenti
            //---Se invece il LoggedUser è cliente la lista dei conti viene caricata sia nella fase di login, sia qui per aggiornare eventuali modifiche

            //Impiegato e direttore
            if (LoggedUser.privilegi != "cliente") {
                string username = Funzioni.digitaUsername();
                listaContiUser = Globals.wcfClient.GetListaContoCorrente(username);
            } else {
                //Cliente
                LoggedUser.contoCorrenti = Globals.wcfClient.GetListaContoCorrente(LoggedUser.username);        //Ricalcola E aggionra la lista dei conti e i relativi saldi
                listaContiUser = LoggedUser.contoCorrenti;
            }

            return listaContiUser;
        }
        public static void printListaConti(List<ContoCorrente> listaConti) {
            var tableConti = new ConsoleTable(" # ", "Numero di conto", "IBAN", "Saldo");

            var index = 1;

            listaConti.ForEach(conto => {
                tableConti.AddRow(index, conto.idContoCorrente, conto.IBAN, conto.saldo);
                index++;
            });

            tableConti.Write();
        }

        //Movimenti
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
        public static decimal CheckImportoDisponibile(decimal importo, string IBANCommittente) {
            bool importoDisponibile = Globals.wcfClient.CheckImporto(importo, IBANCommittente);

            while (importo < 0 || !importoDisponibile) {

                if (importo < 0) {
                    Output.WriteLine("L'importo deve essere maggiore di 0.0");
                } else {
                    Output.WriteLine("Il saldo non ricopre l'importo");
                }

                importo = Convert.ToDecimal(Input.ReadString("Nuovo Importo: "));

                importoDisponibile = Globals.wcfClient.CheckImporto(importo, IBANCommittente);
            };

            return importo;
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
                    errorString = (checkDati) ? string.Empty : "Username o Password non corretti!";
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
