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
    class Program {
        static void TestLogin(ServiceAccountPersonaClient serviceAccountPersonaClient) {
            // >>>> -------------- TEST Login -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool Login(string username, string password) =--\n");
            Console.ResetColor();

            // ---- Valori Input CORRETTI, Risultato Corretto True
            string username = "tempUser";
            string password = "tempUser123";
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
            username = "tempUser";
            password = "empUser123"; // Manca la t nella password
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
            username = "tempUser";
            password = "TempUser123"; // La T deve essere minuscola
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

            // ---- Valore Input Corretto, Risultato Corretto impiegato
            string username = "tempUser2";
            string risultatoStr;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Corretto, risultato atteso impiegato");
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

            // ---- Valori Input Corretti, Risultato atteso è una lista di Clienti appartenenti alla filiale con id = PR00TST000
            string tipoAccount = "cliente";
            string idFiliale = "PR00TST000";
            List<Persona> resultList = new List<Persona>() { };
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Corretti, Risultato atteso è una lista di clienti appartenenti alla filiale con id = PR00TST000");
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

            // ---- Valori Input Corretti, Risultato atteso è una lista di Impiegati appartenenti alla filiale con id = PR00TST001
            tipoAccount = "impiegato";
            idFiliale = "PR00TST001";
            resultList = new List<Persona>() { };
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Corretti, Risultato atteso è una lista di Impiegati appartenenti alla filiale con id = PR00TST001");
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

        static void TestCheckUsername(ServiceAccountPersonaClient serviceAccountPersonaClient) {
            // >>>> -------------- TEST CheckUsername -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= Persona CheckUsername(string username) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso un oggetto Persona con dati validi
            string username = "tempUser1";
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

        static void TestGetIdFilialeByUsername(ServiceAccountPersonaClient serviceAccountPersonaClient) {
            // >>>> -------------- TEST GetIdFilialeByUsername -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= string GetIdFilialeByUsername(string username) =--\n");
            Console.ResetColor();

            // ---- Valore Input Corretto, Risultato Corretto CR12CRM001
            string username = "tempUser2";
            string risultatoStr;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Corretto, risultato atteso CR12CRM001");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultatoStr = serviceAccountPersonaClient.GetIdFilialeByUsername(username);

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
            risultatoStr = serviceAccountPersonaClient.GetIdFilialeByUsername(username);

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
            risultatoStr = serviceAccountPersonaClient.GetIdFilialeByUsername(username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultatoStr);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void StampaPersona(Persona p) {
            Console.WriteLine("------------->> DATI PERSONA <<-------------");
            Console.WriteLine("Tipo di utenza: {0}", p.privilegi);
            Console.WriteLine("Username: {0}\nNome: {1}\nCognome: {2}", p.username, p.nome, p.cognome);
            Console.WriteLine("Codice Fiscale: {0}\nSesso: {1}\nData di nascita: {2}", p.codiceFiscale, p.sesso , p.dataDiNascita);
            Console.WriteLine("Indirizzo: {0}", p.indirizzo);
            Console.WriteLine("Città: {0}\nProvincia: {1}\nStato: {2}", p.citta, p.provincia, p.stato);
            Console.WriteLine("Numero di telefono: {0}", p.numeroDiTelefono);
        }

        static void StampaFiliale(Filiale f) {
            Console.WriteLine("------------->> DATI Filiale <<-------------");
            Console.WriteLine("idFiliale: {0}\nNome: {1}", f.idFiliale, f.nome);
            Console.WriteLine("Indirizzo: {0}", f.indirizzo);
            Console.WriteLine("Città: {0}\nProvincia: {1}\nStato: {2}", f.citta, f.provincia, f.stato);
            Console.WriteLine("Numero di telefono: {0}", f.numeroDiTelefono); 
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
            persona.filiale = "PR00TST000";
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
            persona.filiale = "PR00TST000";
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
            persona.filiale = "PR00TST001";
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

        static void TestGetListaContoCorrente(ServiceContoCorrenteClient serviceContoCorrenteClient) {
            // >>>> -------------- TEST GetListaContoCorrente -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= List<ContoCorrente> GetListaContoCorrente(string username) =--\n");
            Console.ResetColor();

            // ---- Valore Input Corretto, Risultato atteso è una lista di Conti appartenenti all'account con username = tempUser1
            string username = "tempUser1";

            List<ContoCorrente> resultList = new List<ContoCorrente>() { };
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Corretto, Risultato atteso è una lista di Conti appartenenti all'account con username = tempUser1");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            resultList = serviceContoCorrenteClient.GetListaContoCorrente(username).ToList();

            Console.WriteLine("OUTPUT:");

            var table = new ConsoleTable("idContoCorrente", "IBAN", "Username", "Saldo", "Filiale");

            resultList.ForEach(ContoCorrente => {
                table.AddRow(ContoCorrente.idContoCorrente, ContoCorrente.IBAN, ContoCorrente.username, ContoCorrente.saldo, ContoCorrente.idFiliale);
            });

            table.Write();

            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input Corretto, Risultato atteso è una lista di Conti appartenenti all'account con username = tempuser2
            username = "tempuser2";
            resultList = new List<ContoCorrente>() { };
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Corretto, Risultato atteso è una lista di Conti appartenenti all'account con username = tempuser2");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            resultList = serviceContoCorrenteClient.GetListaContoCorrente(username).ToList();

            Console.WriteLine("OUTPUT:");

            table = new ConsoleTable("idContoCorrente", "IBAN", "Username", "Saldo", "Filiale");

            resultList.ForEach(ContoCorrente => {
                table.AddRow(ContoCorrente.idContoCorrente, ContoCorrente.IBAN, ContoCorrente.username, ContoCorrente.saldo, ContoCorrente.idFiliale);
            });

            table.Write();

            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore di input Sbagliato, Risultato atteso è una lista vuota di Conti 
            username = string.Empty;
            resultList = new List<ContoCorrente>() { };
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Sbagliato, Risultato atteso è una lista vuota di Conti");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            resultList = serviceContoCorrenteClient.GetListaContoCorrente(username).ToList();

            Console.WriteLine("OUTPUT:");

            table = new ConsoleTable("idContoCorrente", "IBAN", "Username", "Saldo", "Filiale");

            resultList.ForEach(ContoCorrente => {
                table.AddRow(ContoCorrente.idContoCorrente, ContoCorrente.IBAN, ContoCorrente.username, ContoCorrente.saldo, ContoCorrente.idFiliale);
            });

            table.Write();

            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestSelectContoCorrente(ServiceContoCorrenteClient serviceContoCorrenteClient) {
            // >>>> -------------- TEST SelectContoCorrente -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= ContoCorrente SelectContoCorrente(int idContoCorrente); =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso un oggetto ContoCorrente con dati validi
            int idContoCorrente = (int)serviceContoCorrenteClient.GetListaContoCorrente("tempUser1").First().idContoCorrente;
            ContoCorrente risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso un oggetto ContoCorrente con dati validi");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tCodice Fiscale: {0}", idContoCorrente);
            risultato = serviceContoCorrenteClient.SelectContoCorrente(idContoCorrente);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> idContoCorrente: {0}\tIBAN: {1}\tUsername: {2}\tSaldo: {3}\tFiliale: {4}", risultato.idContoCorrente, risultato.IBAN, risultato.username, risultato.saldo, risultato.idFiliale);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso un oggetto ContoCorrente con attributi settati a default
            idContoCorrente = 0;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso un oggetto ContoCorrente con attributi settati a default");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tCodice Fiscale: {0}", idContoCorrente);
            risultato = serviceContoCorrenteClient.SelectContoCorrente(idContoCorrente);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> idContoCorrente: {0}\tIBAN: {1}\tUsername: {2}\tSaldo: {3}\tFiliale: {4}", risultato.idContoCorrente, risultato.IBAN, risultato.username, risultato.saldo, risultato.idFiliale);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }
        
        static void TestAggiungiContoCorrente(ServiceContoCorrenteClient serviceContoCorrenteClient) {
            // >>>> -------------- TEST AggiungiContoCorrente -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool AggiungiContoCorrente(string username, string idFiliale, decimal? saldo) =--\n");
            Console.ResetColor();

            // ---- Valori Input CORRETTI, Risultato Corretto True
            string username = "tempUser1";
            decimal saldo = 1000;
            string idFiliale = "PR00TST001";
            bool risultato;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valori di input Corretti, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tFiliale: {1}\tSaldo: {2}", username, idFiliale, saldo);
            risultato = serviceContoCorrenteClient.AggiungiContoCorrente(username, idFiliale, saldo);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input CORRETTI, Risultato Corretto True
            username = "tempUser1";
            saldo = 2000;
            idFiliale = "PR00TST001";

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valori di input Corretti, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tFiliale: {1}\tSaldo: {2}", username, idFiliale, saldo);
            risultato = serviceContoCorrenteClient.AggiungiContoCorrente(username, idFiliale, saldo);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input Sbagliati, Risultato Corretto False
            username = "tempUser"; // Errore
            saldo = 2000;
            idFiliale = "PR00TST001";

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valori di input Sbagliati, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tFiliale: {1}\tSaldo: {2}", username, idFiliale, saldo);
            risultato = serviceContoCorrenteClient.AggiungiContoCorrente(username, idFiliale, saldo);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input Sbagliati, Risultato Corretto False
            username = string.Empty; // Errore
            decimal? nullableSaldo = null;
            idFiliale = string.Empty; // Errore

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valori di input Sbagliati, risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}\tFiliale: {1}\tSaldo: {2}", username, idFiliale, nullableSaldo);
            risultato = serviceContoCorrenteClient.AggiungiContoCorrente(username, idFiliale, nullableSaldo);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestCheckIBAN(ServiceContoCorrenteClient serviceContoCorrenteClient) {
            // >>>> -------------- TEST CheckIBAN -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool CheckIBAN(string IBAN) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso True
            string IBAN = serviceContoCorrenteClient.GetListaContoCorrente("tempUser1").First().IBAN;
            bool risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tIBAN: {0}", IBAN);
            risultato = serviceContoCorrenteClient.CheckIBAN(IBAN);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso False
            IBAN = "inesistente";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tIBAN: {0}", IBAN);
            risultato = serviceContoCorrenteClient.CheckIBAN(IBAN);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso False
            IBAN = string.Empty;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tIBAN: {0}", IBAN);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            risultato = serviceContoCorrenteClient.CheckIBAN(IBAN);
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestCheckIDConto(ServiceContoCorrenteClient serviceContoCorrenteClient) {
            // >>>> -------------- TEST CheckIDConto -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool CheckIDConto(int idContoCorrente) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso True
            int idContoCorrente = (int)serviceContoCorrenteClient.GetListaContoCorrente("tempUser1").First().idContoCorrente;
            bool risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tidContoCorrente: {0}", idContoCorrente);
            risultato = serviceContoCorrenteClient.CheckIDConto(idContoCorrente);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso False
            idContoCorrente = 0;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tidContoCorrente: {0}", idContoCorrente);
            risultato = serviceContoCorrenteClient.CheckIDConto(idContoCorrente);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        /*  Filiali TEST presenti nel DB sono:
        *      Filiale 1:
        *          idFiliale = "PR00TST000"
        *          nome = "UniPR Bank Test1"
        *          indirizzo = "Via Dordone, 12"
        *          CAP = 43036
        *          citta = "Fidenza"
        *          provincia = "PR"
        *          stato = "Italia"
        *          numeroDiTelefono = "0524000001"
        *          
        *      Filiale 2:
        *          idFiliale = "PR00TST001"
        *          nome = "UniPR Bank Test2"
        *          indirizzo = "Via Gramsci, 12"
        *          CAP = 43100
        *          citta = "Parma"
        *          provincia = "PR"
        *          stato = "Italia"
        *          numeroDiTelefono = "0524000000"        
        */
        static void TestGetFiliale(ServiceFilialeClient serviceFilialeClient) {
            // >>>> -------------- TEST GetFiliale -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= Filiale GetFiliale(string username) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso un oggetto Filiale con dati validi
            string username = "tempuser1";
            Filiale risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso un oggetto Filiale con dati validi");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = serviceFilialeClient.GetFiliale(username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>");
            StampaFiliale(risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso un oggetto Filiale con attributi settati a default
            username = "inesistente";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso un oggetto Filiale con attributi settati a default");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);
            risultato = serviceFilialeClient.GetFiliale(username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>");
            StampaFiliale(risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso un oggetto Filiale con attributi settati a default
            username = string.Empty;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso un oggetto Filiale con attributi settati a default");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tUsername: {0}", username);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            risultato = serviceFilialeClient.GetFiliale(username);
            Console.WriteLine("OUTPUT>");
            StampaFiliale(risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestModificaDatiFiliale(ServiceFilialeClient serviceFilialeClient) {
            // >>>> -------------- TEST ModificaDatiFiliale -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool ModificaDatiFiliale(string idFiliale, Filiale nuovaFiliale) =--\n");
            Console.ResetColor();

            // ---- Valori Input CORRETTI, Modifico dati relativi all'indirizzo, Risultato Corretto True
            Filiale filiale = new Filiale();
            filiale.idFiliale = string.Empty;
            filiale.nome = string.Empty;
            filiale.indirizzo = "Via I maggio, 24";
            filiale.CAP = null;
            filiale.citta = string.Empty;
            filiale.provincia = string.Empty;
            filiale.stato = string.Empty;
            filiale.numeroDiTelefono = string.Empty;
            string idFiliale = "PR00TST000";
            bool risultato;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valori di input Corretti, Modifico dati relativi all'indirizzo, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tidFiliale: {0}", idFiliale);
            StampaFiliale(filiale);
            risultato = serviceFilialeClient.ModificaDatiFiliale(idFiliale, filiale);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input SBAGLIATI, Nessun dato da aggiornare, Risultato Corretto False
            filiale.indirizzo = string.Empty;
            filiale.CAP = null;
            filiale.citta = string.Empty;
            filiale.provincia = string.Empty;
            filiale.stato = string.Empty;
            idFiliale = "PR00TST000";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori Input SBAGLIATI, Nessun dato da aggiornare, Risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tidFiliale: {0}", idFiliale);
            StampaFiliale(filiale);
            risultato = serviceFilialeClient.ModificaDatiFiliale(idFiliale, filiale);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valori Input CORRETTI, Cambio idFiliale, Risultato True
            filiale.idFiliale = "PR10TST000";

            idFiliale = "PR00TST000";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValori di input Corretti, Cambio idFiliale, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tidFiliale: {0}", idFiliale);
            StampaFiliale(filiale);
            risultato = serviceFilialeClient.ModificaDatiFiliale(idFiliale, filiale);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestGetNameFiliale(ServiceFilialeClient serviceFilialeClient) {
            // >>>> -------------- TEST GetNameFiliale -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= string GetNameFiliale(string idFiliale) =--\n");
            Console.ResetColor();

            // ---- Valore Input Corretto, Risultato Corretto UniPR Bank Test1
            string idFiliale = "PR00TST000";
            string risultatoStr;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Corretto, risultato atteso UniPR Bank Test1");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tidFiliale: {0}", idFiliale);
            risultatoStr = serviceFilialeClient.GetNameFiliale(idFiliale);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultatoStr);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input Sbagliato, Risultato Corretto string.Empty
            idFiliale = "inesistente";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Sbagliato, risultato atteso string.Empty");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tidFiliale: {0}", idFiliale);
            risultatoStr = serviceFilialeClient.GetNameFiliale(idFiliale);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultatoStr);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input Sbagliato, Risultato Corretto string.Empty
            idFiliale = string.Empty;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Sbagliato, risultato atteso string.Empty");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tidFiliale: {0}", idFiliale);
            risultatoStr = serviceFilialeClient.GetNameFiliale(idFiliale);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultatoStr);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestGetListaMovimenti(ServiceMovimentiClient serviceMovimentiClient, ServiceContoCorrenteClient serviceContoCorrenteClient) {
            // >>>> -------------- TEST GetListaMovimenti -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= List<Movimento> GetListaMovimenti(int idContoCorrente) =--\n");
            Console.ResetColor();

            // ---- Valore Input Corretto, Risultato atteso è una lista di Movimenti appartenenti al conto corrente con id = tempuser
            int idContoCorrente = (int)serviceContoCorrenteClient.GetListaContoCorrente("tempUser1").First().idContoCorrente;

            List<Movimento> resultList = new List<Movimento>() { };
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Corretto, Risultato atteso è una lista di Movimenti appartenenti al conto corrente con id = tempuser");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tidContoCorrente: {0}", idContoCorrente);
            resultList = serviceMovimentiClient.GetListaMovimenti(idContoCorrente).ToList();

            Console.WriteLine("OUTPUT:");

            var table = new ConsoleTable("idMovimenti", "IBANCommittente", "tipo", "importo", "IBANBeneficiario", "dataOra");

            resultList.ForEach(movimento => {
                table.AddRow(movimento.idMovimenti, movimento.IBANCommittente, movimento.tipo, movimento.importo, movimento.IBANBeneficiario, movimento.dataOra);
            });

            table.Write();

            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore di input Sbagliato, Risultato atteso è una lista vuota di Movimenti 
            idContoCorrente = 0;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nValore di input Sbagliato, Risultato atteso è una lista vuota di Movimenti");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tidContoCorrente: {0}", idContoCorrente);
            resultList = serviceMovimentiClient.GetListaMovimenti(idContoCorrente).ToList();

            Console.WriteLine("OUTPUT:");

            table = new ConsoleTable("idMovimenti", "IBANCommittente", "tipo", "importo", "IBANBeneficiario", "dataOra");

            resultList.ForEach(movimento => {
                table.AddRow(movimento.idMovimenti, movimento.IBANCommittente, movimento.tipo, movimento.importo, movimento.IBANBeneficiario, movimento.dataOra);
            });

            table.Write();

            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestCheckImporto(ServiceMovimentiClient serviceMovimentiClient, ServiceContoCorrenteClient serviceContoCorrenteClient) {
            // >>>> -------------- TEST CheckImporto -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool CheckImporto(decimal importo, string IBANCommittente) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso True
            string IBAN = serviceContoCorrenteClient.GetListaContoCorrente("tempUser1").First().IBAN;
            decimal importo = 850;
            bool risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tImporto: {0}\tIBAN: {1}", importo, IBAN);
            risultato = serviceMovimentiClient.CheckImporto(importo, IBAN);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input CORRETTO, Risultato atteso False, Importo supera il saldo
            importo = 950;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso False, Importo supera il saldo");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tImporto: {0}\tIBAN: {1}", importo, IBAN);
            risultato = serviceMovimentiClient.CheckImporto(importo, IBAN);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso False
            IBAN = string.Empty;
            importo = 0;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tImporto: {0}\tIBAN: {1}", importo, IBAN);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            risultato = serviceMovimentiClient.CheckImporto(importo, IBAN);
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestEseguiBonifico(ServiceMovimentiClient serviceMovimentiClient, ServiceContoCorrenteClient serviceContoCorrenteClient) {
            // >>>> -------------- TEST EseguiBonifico -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool EseguiBonifico(string IBANCommittente, string IBANBeneficiario, decimal importo) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso True
            string IBANCommittente = serviceContoCorrenteClient.GetListaContoCorrente("tempUser1").First().IBAN;
            string IBANBeneficiario = serviceContoCorrenteClient.GetListaContoCorrente("tempUser1").Last().IBAN;
            decimal importo = 100;

            bool risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tIBANCommittente: {0}\tIBANBeneficiario: {1}\tImporto: {2}", IBANCommittente, IBANBeneficiario, importo);
            risultato = serviceMovimentiClient.EseguiBonifico(IBANCommittente, IBANBeneficiario, importo);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso False
            IBANCommittente = string.Empty;
            IBANBeneficiario = string.Empty;
            importo = 0;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tIBANCommittente: {0}\tIBANBeneficiario: {1}\tImporto: {2}", IBANCommittente, IBANBeneficiario, importo);
            risultato = serviceMovimentiClient.EseguiBonifico(IBANCommittente, IBANBeneficiario, importo);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestEseguiPrelievoDenaro(ServiceMovimentiClient serviceMovimentiClient, ServiceContoCorrenteClient serviceContoCorrenteClient) {
            // >>>> -------------- TEST EseguiPrelievoDenaro -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool EseguiPrelievoDenaro(int IBANCommittente, decimal importo) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso True
            string IBANCommittente = serviceContoCorrenteClient.GetListaContoCorrente("tempUser1").First().IBAN;
            decimal importo = 100;

            bool risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tIBANCommittente: {0}\tImporto: {1}", IBANCommittente, importo);
            risultato = serviceMovimentiClient.EseguiPrelievoDenaro(IBANCommittente, importo);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso False
            IBANCommittente = string.Empty;
            importo = 0;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tIBANCommittente: {0}\tImporto: {1}", IBANCommittente, importo);
            risultato = serviceMovimentiClient.EseguiPrelievoDenaro(IBANCommittente, importo);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void TestEseguiDeposito(ServiceMovimentiClient serviceMovimentiClient, ServiceContoCorrenteClient serviceContoCorrenteClient) {
            // >>>> -------------- TEST EseguiDeposito -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= bool EseguiDeposito(int IBANCommittente, decimal importo) =--\n");
            Console.ResetColor();

            // ---- Valore Input CORRETTO, Risultato atteso True
            string IBANCommittente = serviceContoCorrenteClient.GetListaContoCorrente("tempUser1").First().IBAN;
            decimal importo = 100;

            bool risultato;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Corretto, Risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tIBANCommittente: {0}\tImporto: {1}", IBANCommittente, importo);
            risultato = serviceMovimentiClient.EseguiDeposito(IBANCommittente, importo);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            // ---- Valore Input SBAGLIATO, Risultato atteso False
            IBANCommittente = string.Empty;
            importo = 0;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valore di input Sbagliato, Risultato atteso False");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tIBANCommittente: {0}\tImporto: {1}", IBANCommittente, importo);
            risultato = serviceMovimentiClient.EseguiDeposito(IBANCommittente, importo);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT> {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();
        }

        static void Main(string[] args) {

            ServiceAccountPersonaClient serviceAccountPersonaClient = new ServiceAccountPersonaClient();
            ServiceContoCorrenteClient serviceContoCorrenteClient = new ServiceContoCorrenteClient();
            ServiceMovimentiClient serviceMovimentiClient = new ServiceMovimentiClient();
            ServiceFilialeClient serviceFilialeClient = new ServiceFilialeClient();

            TestAggiungiPersona(serviceAccountPersonaClient);
            TestLogin(serviceAccountPersonaClient);
            TestGetListaPersona(serviceAccountPersonaClient);
            TestModificaPersona(serviceAccountPersonaClient);
            TestGetIdFilialeByUsername(serviceAccountPersonaClient);
            TestGetPrivilegi(serviceAccountPersonaClient);
            TestGetPersona(serviceAccountPersonaClient);
           
            TestGetNameFiliale(serviceFilialeClient);
            TestModificaDatiFiliale(serviceFilialeClient);
            TestGetFiliale(serviceFilialeClient);

            /*  Account TEST presenti nel DB sono:
            *      ACCOUNT 1:
            *          username = "tempUser1"
            *          password = "tempUser123"
            *          privilegi = "cliente"
            *          codiceFiscale = "FRNLVC80M55H889B"
            *          nome = "Lodovica"
            *          cognome = "Fiorentino"
            *          dataDiNascita = 1980-8-15
            *          sesso = "femmina"
            *          indirizzo = "Via Dordone, 12"
            *          CAP = 43014
            *          citta = "Felegara"
            *          provincia = "PR"
            *          stato = "Italia"
            *          numeroDiTelefono = "3853673585"
            *          filiale = "PR12FID001"
            *          
            *      ACCOUNT 2:
            *          username = "tempUser2"
            *          password = "tempUser123"
            *          privilegi = "impiegato"
            *          filiale = "CR12CRM001"
            *          
            *          + stessi dati anagrafici di account 1
            */

            TestAggiungiContoCorrente(serviceContoCorrenteClient);
            TestGetListaContoCorrente(serviceContoCorrenteClient);
            TestSelectContoCorrente(serviceContoCorrenteClient);
            TestCheckIBAN(serviceContoCorrenteClient);
            TestCheckIDConto(serviceContoCorrenteClient);
            
            /*  Saldo di partenza: 1000
            *      Bonifico di 100 ad un altro conto
            *      Prelievo di 100
            *      Deposito di 100
            *      
            *  Saldo finale 900
            */
            TestEseguiBonifico(serviceMovimentiClient, serviceContoCorrenteClient);
            TestEseguiPrelievoDenaro(serviceMovimentiClient, serviceContoCorrenteClient);
            TestEseguiDeposito(serviceMovimentiClient, serviceContoCorrenteClient);
            TestCheckImporto(serviceMovimentiClient, serviceContoCorrenteClient);
            TestGetListaMovimenti(serviceMovimentiClient, serviceContoCorrenteClient);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ATTENZIONE!!!  --  ATTENZIONE!!! -- ATTENZIONE!!!  -- ATTENZIONE!!!");
            Console.ResetColor();
            Console.WriteLine("\nPrima di proseguire eliminare manualmente dal DB i movimenti e i ContiCorrenti usati per i test.\n");
            Console.WriteLine("Premere un tasto per continuare...");
            Console.ReadLine();
            Console.WriteLine("\nSEI SICURO DI AVER ELIMINATO I MOVIMENTI E I CONTICORRENTI ???\n");
            Console.WriteLine("Premere un tasto per continuare...");
            Console.ReadLine();

            TestCheckUsername(serviceAccountPersonaClient);
            TestEliminaAccount(serviceAccountPersonaClient);

            // >>>> -------------- RESET DEI DATI DELLA FILIALE DI TEST -------------- <<<<<<<<
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --= RESET FILIALE DI TEST =--\n");
            Console.ResetColor();

            // ---- Valori Input CORRETTI, Modifico dati relativi all'indirizzo, Risultato Corretto True
            Filiale filiale = new Filiale();
            filiale.idFiliale = "PR00TST000";
            filiale.nome = string.Empty;
            filiale.indirizzo = "Via Dordone, 12";
            filiale.CAP = null;
            filiale.citta = string.Empty;
            filiale.provincia = string.Empty;
            filiale.stato = string.Empty;
            filiale.numeroDiTelefono = string.Empty;
            string idFiliale = "PR10TST000";
            bool risultato;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Valori di input Corretti, Modifico dati relativi all'indirizzo, risultato atteso True");
            Console.ResetColor();
            Console.WriteLine("INPUT >\tidFiliale: {0}", idFiliale);
            StampaFiliale(filiale);
            risultato = serviceFilialeClient.ModificaDatiFiliale(idFiliale, filiale);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("OUTPUT>\tRisultato: {0}", risultato);
            Console.ResetColor();
            Console.Write("\nPremere un tasto per continuare...");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine("TEST CONCLUSO.\n\nPremere un tasto per chiudere la finestra...");
            Console.ReadLine();
        }
    }
}
