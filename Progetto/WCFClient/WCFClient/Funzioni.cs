using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyConsole;
using System.Configuration;
using WCFClient.ServiceReference1;
using ConsoleTables;
using System.Text.RegularExpressions;

namespace WCFClient
{
    public static class Funzioni
    {
        //Questa classe fornisce dei metodi globali che possono essere chiamati da tutta l'applicazione Client a fine di ottimizzazione del codice

        //Persona/Account
        public static void StampaPersona(Persona p) {
            Output.WriteLine("\n\t>> Dati Persona <<\n");
            Output.WriteLine("Tipo di utenza: " + p.privilegi);
            Output.WriteLine("\nUsername: " + p.username + "\nNome: " + p.nome + "\nCognome: " + p.cognome);
            Output.WriteLine("\nCodice Fiscale: " + p.codiceFiscale + "\nSesso: " + p.sesso + "\nData di nascita: " + p.dataDiNascita.ToString().Remove(10, 9));
            Output.WriteLine("\nIndirizzo: " + p.indirizzo + "\nCittà: " + p.citta + "\nCAP: " + p.CAP + "\nProvincia: " + p.provincia + "\nStato: " + p.stato);
            Output.WriteLine("\nNumero di telefono: " + p.numeroDiTelefono);
        }

        public static string InputPassword() {
            string password = string.Empty;
            do {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter) {
                    password += key.KeyChar;
                    Console.Write("*");
                } else {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0) {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    } else if (key.Key == ConsoleKey.Enter) {
                        break;
                    }
                }
            } while (true);

            return password;
        }

        public static void aggiungiPersona(string privilegio) {

            string username = digitaNuovoUsername();

            if (username != string.Empty) {

                string codiceFiscale = string.Empty;
                string nome = string.Empty;
                string cognome = string.Empty;
                string sesso = string.Empty;
                string indirizzo = string.Empty;
                string numeroDiTelefono = string.Empty;
                string filiale = LoggedUser.idFiliale;
                string citta = string.Empty;
                string provincia = string.Empty;
                string stato = string.Empty;
                DateTime dataDiNascita = Globals.defaultDate;
                int? CAP = null;
                string temp;
                bool maggiorenne = false;

                int scelta = 2;

                //Tutti gli attributi devono essere valorizzati, altrimenti esegue il ciclo while
                while (scelta != 1) {
                    Console.WriteLine("");
                    try {
                        //Nome
                        if (nome == string.Empty) {
                            temp = Input.ReadString("Nome: ");
                            if (!string.IsNullOrWhiteSpace(temp)) { nome = temp; }
                        }

                        //Cognome
                        if (cognome == string.Empty) {
                            temp = Input.ReadString("Cognome: ");
                            if (!string.IsNullOrWhiteSpace(temp)) { cognome = temp; }
                        }

                        //Data di nascita
                        if (!maggiorenne) {
                            temp = Input.ReadString("Data di nascita: ");
                            if (!string.IsNullOrWhiteSpace(temp)) {
                                DateTime.TryParse(temp, out dataDiNascita);
                                maggiorenne = Funzioni.checkEta(dataDiNascita);
                            }
                        }

                        //Codice fiscale
                        if (codiceFiscale == string.Empty) {
                            temp = Input.ReadString("Codice fiscale: ");
                            if (!string.IsNullOrWhiteSpace(temp)) {
                                Match match = Regex.Match(temp, "^[A-Z]{6}[\\d]{2}[A-Z][\\d]{2}[A-Z][\\d]{3}[A-Z]$");

                                if (match != Match.Empty) {
                                    codiceFiscale = temp;
                                } else {
                                    throw new Exception("Codice Fiscale non valido");
                                }
                            }
                        }

                        //Sesso
                        if (sesso == string.Empty) {
                            temp = Input.ReadString("Sesso: ");
                            if (!string.IsNullOrWhiteSpace(temp)) { sesso = temp; }
                        }

                        //Indirizzo
                        if (indirizzo == string.Empty) {
                            temp = Input.ReadString("Indirizzo: ");
                            if (!string.IsNullOrWhiteSpace(temp)) { indirizzo = temp; }
                        }

                        //Citta
                        if (citta == string.Empty) {
                            temp = Input.ReadString("Città: ");
                            if (!string.IsNullOrWhiteSpace(temp)) { citta = temp; }
                        }

                        //CAP
                        if (CAP == null) {
                            CAP = Input.ReadInt("CAP: ", 0, 99999);
                        }

                        //Provincia
                        if (provincia == string.Empty) {
                            temp = Input.ReadString("Provincia: ");
                            if (!string.IsNullOrWhiteSpace(temp)) { provincia = temp; }
                        }

                        //Stato
                        if (stato == string.Empty) {
                            temp = Input.ReadString("Stato: ");
                            if (!string.IsNullOrWhiteSpace(temp)) { stato = temp; }
                        }

                        //Numero di telefono
                        if (numeroDiTelefono == string.Empty) {
                            temp = Input.ReadString("Numero di telefono: ");
                            if (!string.IsNullOrWhiteSpace(temp)) { numeroDiTelefono = temp; }
                        }

                        //Filiale
                        Output.WriteLine("Filiale di appartenenza: " + filiale + " - " + Globals.wcfClient.GetNameFiliale(filiale));

                        if (codiceFiscale != string.Empty && nome != string.Empty && cognome != string.Empty &&
                                sesso != string.Empty && indirizzo != string.Empty &&
                                numeroDiTelefono != string.Empty && filiale != string.Empty &&
                                citta != string.Empty && provincia != string.Empty && stato != string.Empty &&
                                CAP.HasValue && maggiorenne) 
                        {
                            scelta = 1;
                        }


                    }
                    catch (Exception ex) {
                        Output.WriteLine(ConsoleColor.Red, ex.Message);
                        scelta = Input.ReadInt("Vuoi annullare l'operazione?\n1) Si\n2) No\nScelta: ", 1, 2);
                    }
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

                //La password non viene registrata nella classe Persona
                Console.Write("Password: ");
                string password1 = Funzioni.InputPassword();
                Console.Write("Conferma Password: ");
                string password2 = Funzioni.InputPassword();

                //Le password devono coincidere
                while (password1 != password2) {
                    Output.WriteLine(ConsoleColor.Red, "Le password non coincidono, riprovare\n");
                    Console.Write("Password: ");
                    password1 = Funzioni.InputPassword();
                    Console.Write("Conferma Password: ");
                    password2 = Funzioni.InputPassword();
                }
                Output.WriteLine("Le password coincidono...\n\n");

                bool risultato = Globals.wcfClient.AggiungiPersona(persona, password1);

                if (risultato) {
                    Output.WriteLine(ConsoleColor.DarkGreen, "\n" + privilegio + " aggiunto correttamente\n");
                } else {
                    Output.WriteLine(ConsoleColor.DarkRed, "\nErrore, persona non aggiunta\n");
                }
            }
        }

        public static void modificaPersona(string usernamePersona) {

            /*Questa funzione viene richiamata sia quando un impiegato o un direttore vogliono modificare un profilo di un cliente
            sia quando un cliente/impiegato/direttore vuole modificare il proprio profilo.
            (Con LoggedUser so con certezza chi richiama la funzione)
            Il parametro username se viene passato come stringa vuota, vuol dire che devo chiedere all'utente il profilo da modificare
            e tramite checkUsername controllo l'esistenza del profilo nel database,
            altrimenti se lo ho già, vuol dire che la funzione viene chiamata dal cliente/impiegato/direttore
            per modificare il proprio profilo*/


            if (usernamePersona == string.Empty) { usernamePersona = digitaUsername(); }    //Username vuoto

            if (usernamePersona != string.Empty) {
                Persona persona = Globals.wcfClient.CheckUsername(usernamePersona);
                int scelta = 1;
                do {
                    StampaPersona(persona);

                    Output.WriteLine(ConsoleColor.Yellow, "\n\tInserire dati da aggiornare");
                    Output.WriteLine(ConsoleColor.DarkYellow, "Premere invio per i campi che non si intende modificare\n");

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

                    try {
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
                                        if (personaProperties[index].Name == "codiceFiscale") {

                                            Match match = Regex.Match(temp, "^[A-Z]{6}[\\d]{2}[A-Z][\\d]{2}[A-Z][\\d]{3}[A-Z]$");
                                            if (match == Match.Empty) {
                                                throw new Exception("Il codice fiscale non è corretto");
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        bool risultato = Globals.wcfClient.ModificaPersona(usernamePersona, persona);

                        if (risultato) {
                            Output.WriteLine(ConsoleColor.DarkGreen, "\n" + persona.privilegi + " modificato correttamente\n");
                        } else {
                            Output.WriteLine(ConsoleColor.DarkRed, "\nLa persona NON ha subito modifiche\n");
                        }

                        scelta = 1;

                    }
                    catch (Exception ex) {
                        Output.WriteLine(ConsoleColor.Red, ex.Message);
                        scelta = Input.ReadInt("Vuoi annullare l'operazione?\n1) Si\n2) No\nScelta: ", 1, 2);
                    }
                } while (scelta != 1);
            }
        }

        public static string digitaNuovoUsername() {

            /*Questa funzione viene richiamata ogni qualvolta bisogna registrare un NUOVO utente
            Una volta inserito, viene richiamata la funzione checkUsername() per controllare se l'username non è già stato utilizzato
            Se checkUsername() restituisce una persona vuota, allora l'username non è stato utilizzato e questa funzione restituisce il nuovo username.
            In caso contrario verrà chiesto di digitare nuovamente l'username
            */
            int scelta;
            string username;

            do {
                username = Input.ReadString("Digitare l'username: ");

                if (!string.IsNullOrEmpty(username)) {
                    if (Globals.wcfClient.CheckUsername(username).username != string.Empty) {
                        //La persona restituita deve essere vuota (non esiste = username disponibile)
                        Output.WriteLine(ConsoleColor.Red, "Username già utilizzato\n");
                        username = string.Empty;
                        scelta = Input.ReadInt("Vuoi annullare l'operazione?\n1) Si\n2) No\nScelta: ", 1, 2);
                    } else {
                        return username;
                    }
                } else {
                    Output.WriteLine(ConsoleColor.Red, "\nUsername non inserito");
                    username = string.Empty;
                    scelta = Input.ReadInt("Vuoi annullare l'operazione?\n1) Si\n2) No\nScelta: ", 1, 2);
                }
            } while (scelta != 1);

            return username;
        }

        public static string digitaUsername() {
            //Questa funzione viene chiamata ogni volta che occorre digitare l'username e lo controlla
            //Attenzione! Questa funzione non viene richiamata per registrare una nuova persona, per questo utilizzare digitaNuoboUsername()
            //Restituisce l'username come stringa

            int scelta;
            string username;

            do {
                username = Input.ReadString("Digitare l'username: ");

                if (!string.IsNullOrEmpty(username)) {
                    if (Globals.wcfClient.CheckUsername(username).username == string.Empty) {
                        Output.WriteLine(ConsoleColor.Red, "\nUsername non trovato");
                        username = string.Empty;
                        scelta = Input.ReadInt("Vuoi annullare l'operazione?\n1) Si\n2) No\nScelta: ", 1, 2);
                    } else {
                        return username;
                    }
                } else {
                    Output.WriteLine(ConsoleColor.Red, "\nUsername non inserito");
                    username = string.Empty;
                    scelta = Input.ReadInt("Vuoi annullare l'operazione?\n1) Si\n2) No\nScelta: ", 1, 2);
                }
            } while (scelta != 1);

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

            /*Questa funzione restituisce l'id del conto corrente -> viene utilizzata quando occorre selezionare un conto corrente
              da una lista di conti correnti di un utente*/

            int sceltaConto = 0;        //Indice numero di conto da scegliere
            int index = 1;              //Contatore indici conto correnti

            var tableConti = new ConsoleTable(" # ", "Numero di conto", "IBAN", "Saldo");

            listaContiUser.ForEach(conto => {
                tableConti.AddRow(index, conto.idContoCorrente, conto.IBAN, conto.saldo);
                index++;
            });

            tableConti.Write();

            sceltaConto = Input.ReadInt("Digitare l'indice del conto corrente: ", 1, --index);

            Output.WriteLine("\nIndice scelto {0}", sceltaConto);

            Output.WriteLine("ContoCorrente selezionato: {0}", listaContiUser[sceltaConto - 1].idContoCorrente);

            return Convert.ToUInt64(listaContiUser[sceltaConto - 1].idContoCorrente);
        }

        public static List<ContoCorrente> getListaContiByPrivilege() {
            //-----RICHIESTA USERNAME-------
            //---L'impiegato o il direttore devono inserire l'username dell'account della quale si vuole ottenere la lista movimenti
            //---Se invece il LoggedUser è cliente la lista dei conti viene caricata sia nella fase di login, sia qui per aggiornare eventuali modifiche

            //Impiegato e direttore
            if (LoggedUser.privilegi != "cliente") {
                string username = digitaUsername();
                if (username != string.Empty) {
                    return Globals.wcfClient.GetListaContoCorrente(username);
                } else { // Utente ha annullato l'operazione in digitaUsername
                    return new List<ContoCorrente>();
                }
            } else {
                //Cliente
                LoggedUser.contoCorrenti = Globals.wcfClient.GetListaContoCorrente(LoggedUser.username);        //Ricalcola E aggiorna la lista dei conti e i relativi saldi
                return LoggedUser.contoCorrenti;
            }
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
}
