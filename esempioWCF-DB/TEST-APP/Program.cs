using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using TEST_APP.ServiceReferenceAccountPersona;
using TEST_APP.ServiceReferenceContoCorrente;
using TEST_APP.ServiceReferenceFiliale;
using TEST_APP.ServiceReferenceMovimenti;

namespace TEST_APP
{
    class Program
    {
        // Questo metodo contiene l'errore di cui parlo
        static void Errore (ServiceAccountPersonaClient serviceAccountPersonaClient){

            // L'errore è che Persona non contiene costruttori che accettano argomenti
            // anche se nella dichiarazione della classe esiste il costruttore con argomenti.
            // La dichiarazione della classe si trova nella parte Server nel file WCFServerDB, nel file IServiceAccountPersona
            Persona persona = new Persona("tempUser", "cliente", "FRNLVC80M55H889B", "Lodovica", "Fiorentino",
                                            new DateTime(1980, 8, 15), "femmina", "Via Giulio Camuzzoni, 69", "San Giorgio Morgeto",
                                            "RC", "Italia", "3853673585", "PR12FID001");

            // Il costruttore vuoto che posso usare non è quello dichiarato da me, ma quello generato di default
            Persona persona1 = new Persona();
        }
        static void TestLogin(ServiceAccountPersonaClient serviceAccountPersonaClient) {
            // >>>> -------------- TEST Login -------------- <<<<<<<<
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool Login(string username, string password) =--\n");
            Console.ResetColor();

            // ---- Valori Input CORRETTI, Risultato Corretto True
            string username = "indi97";
            string password = "indi123";
            bool risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valori di input Corretti, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tPassword: {1}", username, password);
            risultato = serviceAccountPersonaClient.Login(username, password);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Risultato Corretto False
            username = "indi97";
            password = "ndi123"; // Manca la i nella password
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Sbagliati, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tPassword: {1}", username, password);
            risultato = serviceAccountPersonaClient.Login(username, password);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Risultato Corretto False
            username = "indi97";
            password = "Indi123"; // La I deve essere minuscola
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Sbagliati, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tPassword: {1}", username, password);
            risultato = serviceAccountPersonaClient.Login(username, password);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Risultato Corretto False
            username = string.Empty; // username vuoto
            password = string.Empty; // password vuota
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Sbagliati, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tPassword: {1}", username, password);
            risultato = serviceAccountPersonaClient.Login(username, password);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestGetPrivilegi(ServiceAccountPersonaClient serviceAccountPersonaClient) {
            // >>>> -------------- TEST GetPrivilegi -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= string GetPrivilegi(string username) =--\n");
            Console.ResetColor();

            // ---- Valore Input Corretto, Risultato Corretto Admin
            string username = "indi97";
            string risultatoStr;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Corretto, risultato atteso Admin");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultatoStr = serviceAccountPersonaClient.GetPrivilegi(username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultatoStr);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input Sbagliato, Risultato Corretto string.Empty
            username = "inesistente";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Sbagliato, risultato atteso string.Empty");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultatoStr = serviceAccountPersonaClient.GetPrivilegi(username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultatoStr);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input Sbagliato, Risultato Corretto string.Empty
            username = string.Empty;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Sbagliato, risultato atteso string.Empty");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultatoStr = serviceAccountPersonaClient.GetPrivilegi(username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultatoStr);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestGetListaPersona(ServiceAccountPersonaClient serviceAccountPersonaClient) {
            // >>>> -------------- TEST GetListaPersone -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= List<Persona> GetListaPersone(string tipoAccount, string idFiliale) =--\n");
            Console.ResetColor();

            // ---- Valori Input Corretti, Risultato atteso è una lista di Clienti appartenenti alla filiale con id = PR12FID001
            string tipoAccount = "cliente";
            string idFiliale = "PR12FID001";
            List<Persona> resultList = new List<Persona>() { };
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Corretti, Risultato atteso è una lista di clienti appartenenti alla filiale con id = PR12FID001");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tTipo account: {0}\tidFiliale: {1}", tipoAccount, idFiliale);
            resultList = serviceAccountPersonaClient.GetListaPersone(tipoAccount, idFiliale).ToList();

            Console.WriteLine("OUTPUT:");

            var table = new ConsoleTable("Filiale", "Privilegi", "Codice Fiscale", "Nome", "Cognome", "Sesso",
                "Data di nascita", "Indirizzo", "CAP", "Città", "Provincia", "Stato", "Numero di Telefono");

            resultList.ForEach(persona => {
                // Prima di poter inserire la data di nascita nella tabella va sistemata in questo modo
                var tempDataNascita = persona.dataDiNascita.ToString().Remove(10, 9);

                table.AddRow(persona.filiale, persona.privilegi, persona.codiceFiscale, persona.nome,
                    persona.cognome, persona.sesso, tempDataNascita, persona.indirizzo, persona.CAP,
                    persona.citta, persona.provincia, persona.stato, persona.numeroDiTelefono);
            });

            table.Write();

            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input Corretti, Risultato atteso è una lista di Impiegati appartenenti alla filiale con id = CR12CRM001
            tipoAccount = "impiegato";
            idFiliale = "CR12CRM001";
            resultList = new List<Persona>() { };
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Corretti, Risultato atteso è una lista di Impiegati appartenenti alla filiale con id = CR12CRM001");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tTipo account: {0}\tidFiliale: {1}", tipoAccount, idFiliale);
            resultList = serviceAccountPersonaClient.GetListaPersone(tipoAccount, idFiliale).ToList();

            Console.WriteLine("OUTPUT:");

            table = new ConsoleTable("Filiale", "Privilegi", "Codice Fiscale", "Nome", "Cognome", "Sesso",
                "Data di nascita", "Indirizzo", "CAP", "Città", "Provincia", "Stato", "Numero di Telefono");

            resultList.ForEach(persona => {
                // Prima di poter inserire la data di nascita nella tabella va sistemata in questo modo
                var tempDataNascita = persona.dataDiNascita.ToString().Remove(10, 9);

                table.AddRow(persona.filiale, persona.privilegi, persona.codiceFiscale, persona.nome,
                    persona.cognome, persona.sesso, tempDataNascita, persona.indirizzo, persona.CAP,
                    persona.citta, persona.provincia, persona.stato, persona.numeroDiTelefono);
            });

            table.Write();

            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input Sbagliati, Risultato atteso è una lista vuota di persone 
            tipoAccount = string.Empty;
            idFiliale = string.Empty;
            resultList = new List<Persona>() { };
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Sbagliati, Risultato atteso è una lista vuota di persone");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tTipo account: {0}\tidFiliale: {1}", tipoAccount, idFiliale);
            resultList = serviceAccountPersonaClient.GetListaPersone(tipoAccount, idFiliale).ToList();

            Console.WriteLine("OUTPUT:");

            table = new ConsoleTable("Filiale", "Privilegi", "Codice Fiscale", "Nome", "Cognome", "Sesso",
                "Data di nascita", "Indirizzo", "CAP", "Città", "Provincia", "Stato", "Numero di Telefono");

            resultList.ForEach(persona => {
                // Prima di poter inserire la data di nascita nella tabella va sistemata in questo modo
                var tempDataNascita = persona.dataDiNascita.ToString().Remove(10, 9);

                table.AddRow(persona.filiale, persona.privilegi, persona.codiceFiscale, persona.nome,
                    persona.cognome, persona.sesso, tempDataNascita, persona.indirizzo, persona.CAP,
                    persona.citta, persona.provincia, persona.stato, persona.numeroDiTelefono);
            });

            table.Write();

            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestEliminaAccount(ServiceAccountPersonaClient serviceAccountPersonaClient) {
            // >>>> -------------- TEST EliminaAccount -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool EliminaAccount(string username) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato Corretto True
            string username = "tempUser1";
            bool risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = serviceAccountPersonaClient.EliminaAccount(username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input CORRETTO, Risultato Corretto True
            username = "tempUser2";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = serviceAccountPersonaClient.EliminaAccount(username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato Corretto False
            username = "inesistente";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = serviceAccountPersonaClient.EliminaAccount(username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato Corretto False
            username = string.Empty;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = serviceAccountPersonaClient.EliminaAccount(username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestCheckUsername(ServiceAccountPersonaClient serviceAccountPersonaClient){
            // >>>> -------------- TEST CheckUsername -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= Persona CheckUsername(string username) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso un oggetto Persona con dati validi
            string username = "indi97";
            Persona risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso un oggetto Persona con dati validi");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = serviceAccountPersonaClient.CheckUsername(username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso un oggetto Persona con attributi settati a default
            username = "inesistente";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso un oggetto Persona con attributi settati a default");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = serviceAccountPersonaClient.CheckUsername(username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso un oggetto Persona con attributi settati a default
            username = string.Empty;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso un oggetto Persona con attributi settati a default");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            risultato = serviceAccountPersonaClient.CheckUsername(username);
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void StampaPersona(Persona p) {
            Console.WriteLine("------------->> DATI PERSONA <<-------------");
            Console.WriteLine("Tipo di utenza: {0}", p.privilegi);
            Console.WriteLine("Username: {0}\nNome: {1}\nCognome: {2}", p.username, p.nome, p.cognome);
            Console.WriteLine("Codice Fiscale: {0}\nSesso: {1}\nData di nascita: {2}", p.codiceFiscale, p.sesso , p.dataDiNascita);
            Console.WriteLine("Città: {0}\nProvincia: {1}\nStato: {2}", p.citta, p.provincia, p.stato);
            Console.WriteLine("Numero di telefono: {0}", p.numeroDiTelefono);
        }

        static void TestGetPersona(ServiceAccountPersonaClient serviceAccountPersonaClient) {
            // >>>> -------------- TEST GetPersona -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= Persona GetPersona(string codiceFiscale) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso un oggetto Persona con dati validi
            string codiceFiscale = "FRNLVC80M55H889B";
            Persona risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso un oggetto Persona con dati validi");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tCodice Fiscale: {0}", codiceFiscale);
            risultato = serviceAccountPersonaClient.GetPersona(codiceFiscale);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso un oggetto Persona con attributi settati a default
            codiceFiscale = "inesistente";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso un oggetto Persona con attributi settati a default");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tCodice Fiscale: {0}", codiceFiscale);
            risultato = serviceAccountPersonaClient.GetPersona(codiceFiscale);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso un oggetto Persona con attributi settati a default
            codiceFiscale = string.Empty;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso un oggetto Persona con attributi settati a default");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tCodice Fiscale: {0}", codiceFiscale);
            risultato = serviceAccountPersonaClient.GetPersona(codiceFiscale);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestAggiungiPersona(ServiceAccountPersonaClient serviceAccountPersonaClient) {
            // >>>> -------------- TEST AggiungiPersona -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool AggiungiPersona(Persona persona, string password) =--\n");
            Console.ResetColor();

            // ---- Valori Input CORRETTI, Risultato Corretto True
            Persona persona = new Persona();
            persona.username = "tempUser";
            persona.privilegi = "cliente";
            persona.codiceFiscale = "FRNLVC80M55H889B";
            persona.nome = "Lodovica";
            persona.cognome = "Fiorentino";
            persona.dataDiNascita = new DateTime(1980, 8, 15);
            persona.sesso = "femmina";
            persona.indirizzo = "Via Giulio Camuzzoni, 69";
            persona.CAP = 89017;
            persona.citta = "San Giorgio Morgeto";
            persona.provincia = "RC";
            persona.stato = "Italia";
            persona.numeroDiTelefono = "3853673585";
            persona.filiale = "PR12FID001";
            string password = "tempUser123";
            bool risultato;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valori di input Corretti, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tPassword: {0}", password);
            StampaPersona(persona);
            risultato = serviceAccountPersonaClient.AggiungiPersona(persona, password);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Persona già registrata, Risultato Corretto False
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori Input SBAGLIATI, Persona già registrata, Risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tPassword: {0}", password);
            StampaPersona(persona);
            risultato = serviceAccountPersonaClient.AggiungiPersona(persona, password);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input Corretti in parte, Persona Registrata ma Account nuovo, Risultato True
            persona.username = "tempUser1";
            persona.privilegi = "impiegato";
            persona.codiceFiscale = "FRNLVC80M55H889B";
            persona.nome = "Lodovica";
            persona.cognome = "Fiorentino";
            persona.dataDiNascita = new DateTime(1980, 8, 15);
            persona.sesso = "femmina";
            persona.indirizzo = "Via Giulio Camuzzoni, 69";
            persona.CAP = 89017;
            persona.citta = "San Giorgio Morgeto";
            persona.provincia = "RC";
            persona.stato = "Italia";
            persona.numeroDiTelefono = "3853673585";
            persona.filiale = "PR12FID001";
            password = "tempUser123";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valori di input Corretti, Persona già registrata ma Account nuovo, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tPassword: {0}", password);
            StampaPersona(persona);
            risultato = serviceAccountPersonaClient.AggiungiPersona(persona, password);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Mancano dei dati, Risultato Corretto False
            persona.username = "tempUser3";
            persona.privilegi = "cliente";
            persona.codiceFiscale = "GRSBDT04T57F839E";
            persona.nome = "Benedetta";
            persona.cognome = "Grossi";
            persona.dataDiNascita = null;   // <<<<<  Campo vuoto
            persona.sesso = string.Empty;   // <<<<<  Campo vuoto
            persona.indirizzo = "Via dei mille, 154";
            persona.CAP = 80121;
            persona.citta = "Napoli";
            persona.provincia = "NA";
            persona.stato = "Italia";
            persona.numeroDiTelefono = "3274115990";
            persona.filiale = "PR12FID001";
            password = "tempUser123";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori Input SBAGLIATI, Mancano dei dati, Risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tPassword: {0}", password);
            StampaPersona(persona);
            risultato = serviceAccountPersonaClient.AggiungiPersona(persona, password);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestModificaPersona(ServiceAccountPersonaClient serviceAccountPersonaClient) {
            // >>>> -------------- TEST ModificaPersona -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool ModificaPersona(string username, Persona persona) =--\n");
            Console.ResetColor();

            // ---- Valori Input CORRETTI, Modifico dati relativi all'indirizzo, Risultato Corretto True
            Persona persona = new Persona();
            persona.username = string.Empty;
            persona.privilegi = string.Empty;
            persona.codiceFiscale = string.Empty;
            persona.nome = string.Empty;
            persona.cognome = string.Empty;
            persona.dataDiNascita = null;
            persona.sesso = string.Empty;
            persona.indirizzo = "Via Dordone, 12";
            persona.CAP = 43014;
            persona.citta = "Felegara";
            persona.provincia = "PR";
            persona.stato = "Italia";
            persona.numeroDiTelefono = string.Empty;
            persona.filiale = string.Empty;
            string username = "tempUser";
            bool risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valori di input Corretti, Modifico dati relativi all'indirizzo, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            StampaPersona(persona);
            risultato = serviceAccountPersonaClient.ModificaPersona(username, persona);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Nessun dato da aggiornare, Risultato Corretto False
            persona.indirizzo = string.Empty;
            persona.CAP = null;
            persona.citta = string.Empty;
            persona.provincia = string.Empty;
            persona.stato = string.Empty;
            username = "tempUser";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori Input SBAGLIATI, Nessun dato da aggiornare, Risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            StampaPersona(persona);
            risultato = serviceAccountPersonaClient.ModificaPersona(username, persona);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input CORRETTI, Cambio username e privilegi account, Risultato True
            persona.username = "tempUser2";
            persona.privilegi = "impiegato";

            username = "tempUser";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Corretti, Cambio username e privilegi account, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            StampaPersona(persona);
            risultato = serviceAccountPersonaClient.ModificaPersona(username, persona);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input CORRETTI, Cambio Filiale, Risultato True
            persona.username = string.Empty;
            persona.privilegi = string.Empty;
            persona.filiale =  "CR12CRM001";
            username = "tempUser2";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Corretti, Cambio Filiale, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            StampaPersona(persona);
            risultato = serviceAccountPersonaClient.ModificaPersona(username, persona);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        // DA QUI IN POI I TEST SONO ANCORA DA FARE
        static void TestSelectContoCorrente(ServiceContoCorrenteClient serviceContoCorrenteClient) {
            // >>>> -------------- TEST SelectContoCorrente -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= ContoCorrente SelectContoCorrente(int idContoCorrente); =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso un oggetto ContoCorrente con dati validi
            int? idContoCorrente = 000000;
            ContoCorrente risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso un oggetto ContoCorrente con dati validi");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tCodice Fiscale: {0}", idContoCorrente);
            risultato = serviceAccountPersonaClient.GetPersona(idContoCorrente);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso un oggetto ContoCorrente con attributi settati a default
            idContoCorrente = 1120;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso un oggetto ContoCorrente con attributi settati a default");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tCodice Fiscale: {0}", idContoCorrente);
            risultato = serviceAccountPersonaClient.GetPersona(idContoCorrente);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso un oggetto ContoCorrente con attributi settati a default
            idContoCorrente = null;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso un oggetto Persona con attributi settati a default");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tCodice Fiscale: {0}", idContoCorrente);
            risultato = serviceAccountPersonaClient.GetPersona(idContoCorrente);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>");
            StampaPersona(risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestAggiungiContoCorrente(ServiceContoCorrenteClient serviceContoCorrenteClient) {
            //bool AggiungiContoCorrente(string username, string idFiliale, decimal? saldo)
        }

        static void TestCheckIBAN(ServiceContoCorrenteClient serviceContoCorrenteClient) {
            //bool CheckIBAN(string IBAN); //true se l'iban esiste, false in caso contrario
        }

        static void TestCheckIDConto(ServiceContoCorrenteClient serviceContoCorrenteClient) {
            //bool CheckIDConto(int idContoCorrente); // controlla se esiste un conto Corrente con quell'ID
        }

        static void TestGetFiliale(ServiceFilialeClient serviceFilialeClient) {
            //Filiale GetFiliale(string username);
        }

        static void TestModificaDatiFiliale(ServiceFilialeClient serviceFilialeClient) {
            //bool ModificaDatiFiliale(string idFiliale, Filiale nuovaFiliale);
        }

        static void TestGetNameFiliale(ServiceFilialeClient serviceFilialeClient) {
            //string GetNameFiliale(string idFiliale); //restituisce il nome della filiale che ha quel determinato IDFiliale
        }

        static void TestGetListaMovimenti(ServiceMovimentiClient serviceMovimentiClient) {
            //List<Movimento> GetListaMovimenti(int idContoCorrente);
        }

        static void TestCheckImporto(ServiceMovimentiClient serviceMovimentiClient) {
            //bool CheckImporto(decimal importo, string IBANCommittente); // controlla se il saldo ricopre l'importo del bonifico
        }

        static void TestEseguiBonifico(ServiceMovimentiClient serviceMovimentiClient) {
            //bool EseguiBonifico(string IBANCommittente, string IBANBeneficiario, decimal importo); // true (bonifico avvenuto) false (caso contrario)
        }

        static void TestEseguiPrelievoDenaro(ServiceMovimentiClient serviceMovimentiClient) {
            //bool EseguiPrelievoDenaro(int IBANCommittente, decimal importo); // true (avvenuto), false (non avvenuto)
        }

        static void TestEseguiDeposito(ServiceMovimentiClient serviceMovimentiClient) {
            //bool EseguiDeposito(int IBANCommittente, decimal importo);  // true (avvenuto), false (non avvenuto)
        }

        static void Main(string[] args) {

            ServiceAccountPersonaClient serviceAccountPersonaClient = new ServiceAccountPersonaClient();
            ServiceContoCorrenteClient serviceContoCorrenteClient = new ServiceContoCorrenteClient();
            ServiceMovimentiClient serviceMovimentiClient = new ServiceMovimentiClient();
            ServiceFilialeClient serviceFilialeClient = new ServiceFilialeClient();

            TestLogin(serviceAccountPersonaClient);
            TestGetPrivilegi(serviceAccountPersonaClient);
            TestGetListaPersona(serviceAccountPersonaClient);
            TestAggiungiPersona(serviceAccountPersonaClient);
            TestModificaPersona(serviceAccountPersonaClient);
            TestGetPersona(serviceAccountPersonaClient);
            TestEliminaAccount(serviceAccountPersonaClient);
            TestCheckUsername(serviceAccountPersonaClient);
        }
    }
}
